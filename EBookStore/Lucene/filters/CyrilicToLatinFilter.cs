
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookStore.Lucene.filters
{
    public class CyrilicToLatinFilter : TokenFilter
    {
        private CharTermAttribute termAttribute;

        public CyrilicToLatinFilter(TokenStream input):base(input)
        {

            termAttribute = (CharTermAttribute)input.AddAttribute<CharTermAttribute>();

        }

        public override bool IncrementToken()
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
