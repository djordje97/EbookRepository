using EBookStore.Configuration;
using EBookStore.Dto;
using EBookStore.Lucene.analyzer;
using EBookStore.Lucene.Model;
using EBookStore.Model;
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

        public List<Ebook> Search(SearchModel searchModel)
        {
            List<Ebook> ebooks = new List<Ebook>();
            if(searchModel.Type == "simple")
            {
                ebooks=SimpleSearch(searchModel);
            }
            return ebooks;
        }

        public List<Ebook> SimpleSearch(SearchModel searchModel)
        {
            QueryParser parser = new QueryParser(Version.LUCENE_48, searchModel.FirstField, analyzer);
            Query query = null;
            Term term = new Term(searchModel.FirstField, searchModel.Input);
            query = new TermQuery(term);
           query=parser.Parse(query.ToString(searchModel.FirstField));
            try
            {
                Directory indexDir = new SimpleFSDirectory(ConfigurationManager.IndexDir);
                DirectoryReader reader = DirectoryReader.Open(indexDir);
                IndexSearcher isr = new IndexSearcher(reader);
                TopScoreDocCollector collector = TopScoreDocCollector.Create(
                        10,true);

                List<Ebook> results = new List<Ebook>();

            isr.Search(query, collector);
                ScoreDoc[] hits = collector.GetTopDocs().ScoreDocs;

                Ebook ebook;
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
                    int publicationYear = Int32.Parse(doc.Get("filedate"));
                    string highlight = "";

                    ebook = new Ebook()
                    {
                        Title=title,
                        Filename=location,
                        Keywords=keywords,
                        MIME="application/json",
                        CategoryId=category,
                        Author=author,
                        LanguageId=language,
                        PublicationYear=publicationYear,
                        UserId=1
                          
                    };
                    results.Add(ebook);
                }
                reader.Dispose();
                return results;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Source);
                return new List<Ebook>();
            }
        }
    }
}
