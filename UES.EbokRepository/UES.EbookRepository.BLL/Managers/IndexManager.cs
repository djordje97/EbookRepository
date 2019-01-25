using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UES.EbookRepository.BLL.Contract.Contracts;
using UES.EbookRepository.BLL.Contract.DTOs;
using UES.EbookRepository.BLL.Extensions;
using UES.EbookRepository.BLL.Lucene;
using TextField = Lucene.Net.Documents.TextField;
using Version = Lucene.Net.Util.LuceneVersion;

namespace UES.EbookRepository.BLL.Managers
{
    public class IndexManager:IIndexManager
    {
        private static Directory indexDirectory = FSDirectory.Open(ConfigurationManager.IndexDir);
        private static Analyzer analyzer = new SerbianAnalyzer();
        private static IndexWriterConfig config = new IndexWriterConfig(Version.LUCENE_48, analyzer);
        protected static IndexWriter indexWriter = new IndexWriter(indexDirectory, config);


        public  void Index(IndexUnit unit)
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
            document.Add(new TextField("language", unit.Language, Field.Store.YES));
            indexWriter.AddDocument(document);
            indexWriter.Commit();

        }


        public IndexUnit GetIndexUnit(string fileName)
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
                    title += reader.Info["Title"];
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

                unit.FileDate = DateTools.DateToString(new System.IO.FileInfo(fileName).LastAccessTime, DateTools.Resolution.DAY);
                var text = string.Empty;
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    text += PdfTextExtractor.GetTextFromPage(reader, i, strategy);
                }
                unit.Text = text.Replace('\n', ' ').Trim();

                return unit;
            }
            catch (Exception e) { Console.Out.WriteLine(e.Message); return null; }

        }


        public  bool Delete(string filename)
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


    }


}
