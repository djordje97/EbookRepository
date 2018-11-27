using com.sun.tools.doclets.@internal.toolkit;
using EBookStore.Lucene.analyzer;
using java.io;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
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
         readonly IConfiguration _configuration;

        public Indexer(IConfiguration configuration)
        {

            _configuration = configuration;

        }

        public static void index(File file,string path)
        {
            Directory directory = FSDirectory.Open(path);
            Analyzer analyzer = new SerbianAnalyzer();
            IndexWriterConfig config = new IndexWriterConfig(Version.LUCENE_48, analyzer);
            IndexWriter indexWriter = new IndexWriter(directory, config);

            Document document = new Document();
            PDFParser parser = new PDFParser(new FileInputStream(file));
            parser.parse();
            PDDocument pdf = parser.getPDDocument();
            PDDocumentInformation info = pdf.getDocumentInformation();

            string title = "" + info.getTitle();
            document.Add(new TextField("title",title,Field.Store.YES));

            string keywords = "" + info.getKeywords();
            string[] splittedKeywords = keywords.Split(" ");
      
            foreach (var keyword in splittedKeywords)
            {
                document.Add(new TextField("keyword", keyword, Field.Store.YES));
            }

            document.Add(new StringField("filename", file.getCanonicalPath(), Field.Store.YES));
            PDFTextStripper textStripper = new PDFTextStripper("utf-8");
            string text = textStripper.getText(parser.getPDDocument());
            document.Add(new TextField("text", text, Field.Store.NO));
            pdf.close();

            indexWriter.Dispose();

        }
    }
}
