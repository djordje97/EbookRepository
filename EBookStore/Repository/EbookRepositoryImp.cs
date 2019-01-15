using EBookStore.Configuration;
using EBookStore.Dto;
using EBookStore.Lucene.analyzer;
using EBookStore.Lucene.Model;
using EBookStore.Model;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version = Lucene.Net.Util.LuceneVersion;

namespace EBookStore.Repository
{
    public class EbookRepositoryImp:Repository<Ebook>,EbookRepository
    {
        private AppDbContext context;
        private static SerbianAnalyzer analyzer = new SerbianAnalyzer();
        public EbookRepositoryImp(AppDbContext dbContext):base(dbContext)
        {
            context = dbContext;
        }

        public List<Ebook> GetEbooksByCategory(int categoryId)
        {
            return context.Ebooks.Where(x => x.CategoryId == categoryId).ToList();
        }

        public List<ResultData> Search(SearchModel searchModel)
        {
            List<RequiredHighlight> highlights = new List<RequiredHighlight>();
            highlights.Add(new RequiredHighlight() { FieldName = searchModel.FirstField, Value = searchModel.Input });
            if(searchModel.Type=="boolean")
                highlights.Add(new RequiredHighlight() { FieldName = searchModel.SecondField, Value = searchModel.Input });
            Query query = QueryBuilder(searchModel);
            List<ResultData> results = GetResults(query, highlights);
            return results;
        }

        public Query QueryBuilder(SearchModel searchModel)
        {
            QueryParser parser = new QueryParser(Version.LUCENE_48, searchModel.FirstField, analyzer);
            Query query = null;
            if(searchModel.Type == "simple")
            {
                Term term = new Term(searchModel.FirstField, searchModel.Input);
                query = new TermQuery(term);
            }else if(searchModel.Type == "fuzzy")
            {
                Term term = new Term(searchModel.FirstField, searchModel.Input);
                query = new FuzzyQuery(term,1);
            }else if(searchModel.Type == "phrase")
            {
                string[] values = searchModel.Input.Split(" ");
                PhraseQuery phrase = new PhraseQuery();
                foreach ( string word in values)
                {

                    Term term = new Term(searchModel.FirstField, word);
                    phrase.Add(term);
                }
                query = phrase;
            }
            else
            {
                Term firstTerm = new Term(searchModel.FirstField, searchModel.Input);
                Term secondTerm = new Term(searchModel.SecondField, searchModel.Input);
                Query firstQuery = new TermQuery(firstTerm);
                Query secondQuery = new TermQuery(secondTerm);
                BooleanQuery booleanClauses = new BooleanQuery();
                if(searchModel.Operation == "and")
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

        public List<ResultData> GetResults(Query query,List<RequiredHighlight> requiredHighlights)
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
                    int language = Int32.Parse(doc.Get("language"));
                    string highlight = "";
                    string text = GetDocumentText(location);
                    foreach (var item in requiredHighlights)
                    {
                        hi = new Highlighter(new QueryScorer(query, reader, item.FieldName));
                        try
                        {
                            highlight += hi.GetBestFragment(analyzer, item.FieldName,text );
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message+"  on"+e.StackTrace);
                        }

                    }


                    re = new ResultData()
                    {
                        Title = title,
                        Filename = location,
                        Keywords = keywords,
                        CategoryId = category,
                        Author = author,
                        Highlight=highlight
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

        public  string GetDocumentText(string fileName)
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

        public Ebook GetEbookByFilename(string fileName)
        {
            return context.Ebooks.Where(x => x.Filename == fileName).SingleOrDefault();
        }
    }
}
