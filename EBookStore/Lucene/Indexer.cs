using com.sun.tools.doclets.@internal.toolkit;
using EBookStore.Configuration;
using EBookStore.Lucene.analyzer;
using EBookStore.Lucene.Model;
using java.io;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Microsoft.Extensions.Configuration;
using org.apache.pdfbox.pdfparser;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version = Lucene.Net.Util.LuceneVersion;

namespace EBookStore.Lucene
{
    public class Indexer
    {
       
        private static Directory indexDirectory = FSDirectory.Open(ConfigurationManager.AppSetting["indexDir"]);
        private static Analyzer analyzer = new SerbianAnalyzer();
        private static IndexWriterConfig config = new IndexWriterConfig(Version.LUCENE_48, analyzer);
        protected static IndexWriter indexWriter = new IndexWriter(indexDirectory, config);

        public static void index(IndexUnit unit,string path)
        {
            
            Document document = new Document();

            document.Add(new TextField("title",unit.Title,Field.Store.YES));

            foreach (var keyword in unit.Keywords)
            {
                document.Add(new TextField("keyword", keyword, Field.Store.YES));
            }

            document.Add(new StringField("filename",unit.Filename, Field.Store.YES));
            document.Add(new TextField("text", unit.Text, Field.Store.NO));
            document.Add(new TextField("filedate", unit.FileDate, Field.Store.YES));
            indexWriter.AddDocument(document);
            indexWriter.Commit();

        }

        public static IndexUnit GetIndexUnit(File file)
        {
            IndexUnit unit=new IndexUnit();
            try
            {
                PDFParser parser = new PDFParser(new FileInputStream(file));
                parser.parse();
                PDDocument pdf = parser.getPDDocument();
                PDDocumentInformation info = pdf.getDocumentInformation();

                string title = "" + info.getTitle();
                unit.Title = title;
                string keywords = "" + info.getKeywords();
                string[] splittedKeywords = keywords.Split(" ");
                unit.Keywords = new List<string>();
                foreach (var item in splittedKeywords)
                {
                    unit.Keywords.Add(item);

                }
                PDFTextStripper textStripper = new PDFTextStripper("utf-8");
                string text = textStripper.getText(parser.getPDDocument());
                unit.Text = text;
                pdf.close();

                unit.FileDate = DateTools.DateToString(new DateTime(file.lastModified()), DateTools.Resolution.DAY);
            }
            catch (Exception){ System.Console.Out.WriteLine("Greska prilikom obrade PDF fajla"); }

            return unit;
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
            catch (IOException e)
            {
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
                TopScoreDocCollector collector = TopScoreDocCollector.Create(10,true);

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
                        catch (IOException e)
                        {
                            throw new Exception("Greska: " + e.getMessage()+" na ovom mestu "+e.getStackTrace());
                        }
                    }
                }

                return false;

            }
            catch (IOException e)
            {
                throw new Exception("Greska: " + e.getMessage() + " na ovom mestu " + e.getStackTrace());
            }
        }
    }
   
}
