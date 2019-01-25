using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;

namespace UES.EbookRepository.BLL.Lucene
{
    public class CyrilicToLatinFilter : TokenFilter
    {
        private CharTermAttribute termAttribute;

        public CyrilicToLatinFilter(TokenStream input) : base(input)
        {

            termAttribute = (CharTermAttribute)input.AddAttribute<ICharTermAttribute>();

        }

        public sealed override bool IncrementToken()
        {
            if (m_input.IncrementToken())
            {
                string text = termAttribute.ToString();
                termAttribute.SetEmpty();
                termAttribute.Append(CyrillicLatinConverter.cir2lat(text));
                return true;
            }
            return false;
        }

    }
}