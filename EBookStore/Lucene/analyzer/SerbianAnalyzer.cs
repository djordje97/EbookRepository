
using System.IO;
using EBookStore.Lucene.filters;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Standard;
using Version = Lucene.Net.Util.LuceneVersion;
namespace EBookStore.Lucene.analyzer
{
    public class SerbianAnalyzer:Analyzer
    {
        public static  string[] STOP_WORDS =
    {
        "i","a","ili","ali","pa","te","da","u","po","na"
    };
        public SerbianAnalyzer()
        {

        }

        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            Tokenizer source = new StandardTokenizer(Version.LUCENE_48, reader);
            TokenStream result=new CyrilicToLatinFilter(source);
            result = new LowerCaseFilter(Version.LUCENE_48, result);
            result = new StopFilter(Version.LUCENE_48, result, StopFilter.MakeStopSet(Version.LUCENE_48, STOP_WORDS));
            return new TokenStreamComponents(source, result);
        }

    }
}
