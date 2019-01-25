using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UES.EbookRepository.BLL.Contract.Contracts;
using UES.EbookRepository.BLL.Contract.DTOs;
using UES.EbookRepository.BLL.Extensions;
using UES.EbookRepository.BLL.Lucene;
using UES.EbookRepository.DAL.Contract.Contracts;
using Lucene.Net.Documents;
using Version = Lucene.Net.Util.LuceneVersion;
using UES.EbookRepository.DAL.Contract.Entities;

namespace UES.EbookRepository.BLL.Managers
{
   public class EbookManager:IEbookManager
    {
        private readonly IEbookProvider _ebookProvider;
        private readonly ILanguageManager _languageManager;
        private readonly IIndexManager _indexManager;
        private static SerbianAnalyzer analyzer = new SerbianAnalyzer();

        public EbookManager(IEbookProvider ebookProvider,ILanguageManager languageManager,IIndexManager indexManager)
        {
            _ebookProvider = ebookProvider;
            _languageManager = languageManager;
            _indexManager = indexManager;

        }


        public IEnumerable<EbookDTO> GetAll()
        {
            return ConverterExtension.ToDTOs(_ebookProvider.GetAll());
        }

        public EbookDTO GetById(int id)
        {
            return ConverterExtension.ToDTO(_ebookProvider.GetById(id));
        }


        public EbookDTO Create(IndexUnit unit)
        {
            _indexManager.Index(unit);
            var keywords = string.Empty;
            foreach (var item in unit.Keywords)
            {
                keywords += item + " ";

            }
            keywords=keywords.Trim();
            var language = _languageManager.GetByName(unit.Language);
            var ebook = new Ebook()
            {
                Author = unit.Author,
                CategoryId = unit.Category,
                Filename = unit.Filename,
                Keywords = keywords,
                MIME = "application/pdf",
                PublicationYear = int.Parse(unit.FileDate),
                Title=unit.Title,
                UserId=1,
                LanguageId=language.LanguageId
            };
            int newEbookId = 0;
            newEbookId = _ebookProvider.Create(ebook);
            return ConverterExtension.ToDTO(_ebookProvider.GetById(newEbookId));
        }

        public EbookDTO Update(IndexUnit indexUnit)
        {
            var ebookFromDb = _ebookProvider.GetAll().Where(book => book.Filename == indexUnit.Filename).SingleOrDefault();
            ebookFromDb.Title = indexUnit.Title;
            ebookFromDb.Author = indexUnit.Author;
            var keyword = string.Empty;
            foreach (var item in indexUnit.Keywords)
            {
                keyword += item + " ";

            }
            if (keyword.Length >= 120)
            {
                keyword = keyword.Substring(0, 110);
            }
            ebookFromDb.Keywords = keyword;
            ebookFromDb.LanguageId = _languageManager.GetByName(indexUnit.Language).LanguageId;
            ebookFromDb.CategoryId = indexUnit.Category;
            indexUnit.FileDate = ebookFromDb.PublicationYear.ToString();

            _indexManager.Delete(indexUnit.Filename);
            indexUnit.Text = GetDocumentText(indexUnit.Filename);
            _indexManager.Index(indexUnit);
            int ebookId;
            ebookId = _ebookProvider.Update(ebookFromDb);
            return ConverterExtension.ToDTO(_ebookProvider.GetById(ebookId));
        }

        public void Delete(int id)
        {
            var book = _ebookProvider.GetById(id);
            var isDeleted=_indexManager.Delete(book.Filename);
            if (isDeleted)
                _ebookProvider.Delete(id);
            else
                return;
        }

        public IEnumerable<EbookDTO> GetByCategory(int categoryId)
        {
            var books = _ebookProvider.GetAll();
            var booksForReturn = books.Where(book => book.CategoryId == categoryId).ToList();
            return ConverterExtension.ToDTOs(booksForReturn);
        }

        public List<ResultData> Search(SearchModel searchModel)
        {
            List<RequiredHighlight> highlights = new List<RequiredHighlight>();
            highlights.Add(new RequiredHighlight() { FieldName = searchModel.FirstField, Value = searchModel.Input });
            if (searchModel.Type == "boolean")
                highlights.Add(new RequiredHighlight() { FieldName = searchModel.SecondField, Value = searchModel.Input });
            Query query = QueryBuilder(searchModel);
            List<ResultData> results = GetResults(query, highlights);
            return results;
        }

        public Query QueryBuilder(SearchModel searchModel)
        {
            QueryParser parser = new QueryParser(Version.LUCENE_48, searchModel.FirstField, analyzer);
            Query query = null;
            if (searchModel.Type == "simple")
            {
                Term term = new Term(searchModel.FirstField, searchModel.Input);
                query = new TermQuery(term);
            }
            else if (searchModel.Type == "fuzzy")
            {
                Term term = new Term(searchModel.FirstField, searchModel.Input);
                query = new FuzzyQuery(term, 1);
            }
            else if (searchModel.Type == "phrase")
            {
                string[] values = searchModel.Input.Split(" ");
                PhraseQuery phrase = new PhraseQuery();
                foreach (string word in values)
                {

                    Term term = new Term(searchModel.FirstField, word);
                    phrase.Add(term);
                }
                query = phrase;
            }
            else
            {
                Term firstTerm = new Term(searchModel.FirstField, searchModel.Input);
                Term secondTerm = new Term(searchModel.SecondField, searchModel.SecondInput);
                Query firstQuery = new TermQuery(firstTerm);
                Query secondQuery = new TermQuery(secondTerm);
                BooleanQuery booleanClauses = new BooleanQuery();
                if (searchModel.Operation == "and")
                {
                    booleanClauses.Add(firstQuery, Occur.MUST);
                    booleanClauses.Add(secondQuery, Occur.MUST);
                }
                else
                {
                    booleanClauses.Add(firstQuery, Occur.SHOULD);
                    booleanClauses.Add(secondQuery, Occur.SHOULD);
                }
                query = booleanClauses;
            }

            return parser.Parse(query.ToString(searchModel.FirstField));

        }

        public List<ResultData> GetResults(Query query, List<RequiredHighlight> requiredHighlights)
        {
            try
            {
                Directory indexDir = new SimpleFSDirectory(ConfigurationManager.IndexDir);
                DirectoryReader reader = DirectoryReader.Open(indexDir);
                IndexSearcher isr = new IndexSearcher(reader);
                TopScoreDocCollector collector = TopScoreDocCollector.Create(
                        10, true);

                List<ResultData> results = new List<ResultData>();

                isr.Search(query, collector);
                ScoreDoc[] hits = collector.GetTopDocs().ScoreDocs;

                ResultData re;
                Document doc;
                Highlighter hi;

                foreach (ScoreDoc sd in hits)
                {
                    doc = isr.Doc(sd.Doc);
                    string[] allKeywords = doc.GetValues("keyword");
                    string keywords = "";
                    foreach (string keyword in allKeywords)
                    {
                        keywords += keyword.Trim() + " ";
                    }
                    keywords = keywords.Trim();
                    string title = doc.Get("title");
                    string location = doc.Get("filename");
                    string author = doc.Get("author");
                    int category = Int32.Parse(doc.Get("category"));
                    string language = doc.Get("language");
                    string highlight = "";
                    string text = GetDocumentText(location);
                    foreach (var item in requiredHighlights)
                    {
                        hi = new Highlighter(new QueryScorer(query, reader, item.FieldName));
                        try
                        {
                            highlight += hi.GetBestFragment(analyzer, item.FieldName, text);
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message + "  on" + e.StackTrace);
                        }

                    }


                    re = new ResultData()
                    {
                        Title = title,
                        Filename = location,
                        Keywords = keywords,
                        CategoryId = category,
                        Author = author,
                        Highlight = highlight,

                    };
                    results.Add(re);
                }
                reader.Dispose();
                return results;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Source);
                return new List<ResultData>();
            }
        }

        public string GetDocumentText(string fileName)
        {
            try
            {
                var filePath = ConfigurationManager.FileDir + fileName;
                PdfReader reader = new PdfReader(filePath);
                var text = string.Empty;
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    text += PdfTextExtractor.GetTextFromPage(reader, i, strategy);
                }
                reader.Close();
                return text;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return string.Empty;
            }
        }

        public EbookDTO GetByFilename(string filename)
        {
            var books = _ebookProvider.GetAll();
            return ConverterExtension.ToDTO(books.Where(book => book.Filename == filename).SingleOrDefault());
        }
    }
}
