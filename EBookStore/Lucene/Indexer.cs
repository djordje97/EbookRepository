using EBookStore.Configuration;
using EBookStore.Lucene.analyzer;
using EBookStore.Lucene.Model;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Directory = Lucene.Net.Store.Directory;
using TextField = Lucene.Net.Documents.TextField;
using Version = Lucene.Net.Util.LuceneVersion;

namespace EBookStore.Lucene
{
    public class Indexer
    {

        private static Directory indexDirectory = FSDirectory.Open(ConfigurationManager.IndexDir);
        private static Analyzer analyzer = new SerbianAnalyzer();
        private static IndexWriterConfig config = new IndexWriterConfig(Version.LUCENE_48, analyzer);
        protected static IndexWriter indexWriter = new IndexWriter(indexDirectory, config);

        public static void index(IndexUnit unit)
        {

            Document document = new Document();

            document.Add(new TextField("title", unit.Title, Field.Store.YES));

            foreach (var keyword in unit.Keywords)
            {
                document.Add(new TextField("keyword", keyword, Field.Store.YES));
            }

            document.Add(new StringField("filename", unit.Filename, Field.Store.YES));
            document.Add(new TextField("text", unit.Text, Field.Store.NO));
            document.Add(new TextField("filedate", unit.FileDate, Field.Store.YES));
            document.Add(new TextField("author", unit.Author, Field.Store.YES));
            document.Add(new TextField("category", unit.Category.ToString(), Field.Store.YES));
            document.Add(new TextField("language", unit.Language.ToString(), Field.Store.YES));
            indexWriter.AddDocument(document);
            indexWriter.Commit();

        }

        public static IndexUnit GetIndexUnit(string fileName)
        {
            IndexUnit unit = new IndexUnit();
            try
            {
                var filePath = ConfigurationManager.FileDir + fileName;
                Console.Out.WriteLine(filePath);
                PdfReader reader = new PdfReader(filePath);
                Dictionary<string, string> dict = reader.Info;

                string author = string.Empty;
                if (dict.Keys.Contains("Author"))
                {
                    author += reader.Info["Author"];
                }
                unit.Author = author;
                string title = string.Empty;
                if (dict.Keys.Contains("Title"))
                {
                    title+= reader.Info["Title"];
                }
                unit.Title = title;
                unit.Keywords = new List<string>();
                if (dict.Keys.Contains("Keywords"))
                {
                    string keywords = "" + reader.Info["Keywords"];
                    string[] splittedKeywords = keywords.Split(" ");

                    foreach (var item in splittedKeywords)
                    {
                        unit.Keywords.Add(item);

                    }
                }

                unit.FileDate = DateTools.DateToString(new FileInfo(fileName).LastAccessTime, DateTools.Resolution.DAY);
                var text = string.Empty;
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    text += PdfTextExtractor.GetTextFromPage(reader, i, strategy);
                }
                unit.Text = text.Replace('\n',' ').Trim();
    
                return unit;
            }
            catch (Exception e) { Console.Out.WriteLine(e.Message); return null; }


        }


        public static bool Delete(string filename)
        {
            Term delTerm = new Term("filename", filename);
            try
            {
                indexWriter.DeleteDocuments(delTerm);
                indexWriter.Commit();
                return true;
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Greska: " + e.Message + " na ovom mestu " + e.StackTrace);
                return false;
            }
        }


        public static bool UpdateDocument(string filename, List<IIndexableField> fields)
        {
            try
            {
                DirectoryReader reader = DirectoryReader.Open(indexDirectory);
                IndexSearcher indexSearcher = new IndexSearcher(reader);
                Query query = new TermQuery(new Term("filename", filename));
                TopScoreDocCollector collector = TopScoreDocCollector.Create(10, true);

                indexSearcher.Search(query, collector);

                ScoreDoc[] scoreDocs = collector.GetTopDocs().ScoreDocs;
                if (scoreDocs.Length > 0)
                {
                    int docID = scoreDocs[0].Doc;
                    Document doc = indexSearcher.Doc(docID);
                    if (doc != null)
                    {
                        foreach (IIndexableField field in fields)
                        {
                            doc.RemoveFields(field.Name);
                        }
                        foreach (IIndexableField field in fields)
                        {
                            doc.Add(field);
                        }
                        try
                        {

                            indexWriter.UpdateDocument(new Term("filename", filename), doc);
                            indexWriter.Commit();
                            return true;

                        }
                        catch (Exception e)
                        {
                            System.Console.Out.WriteLine("Greska: " + e.Message + " na ovom mestu " + e.StackTrace);
                            return false;
                        }
                    }
                }


                return false;
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Greska: " + e.Message + " na ovom mestu " + e.StackTrace);
                return false;
            }
        }
    }

}
