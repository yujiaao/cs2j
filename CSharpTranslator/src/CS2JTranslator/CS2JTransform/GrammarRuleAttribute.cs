// $ANTLR 3.3 Nov 30, 2010 12:46:29 src/test/resources/trans/JavaMaker.g 2022-07-29 18:45:34

using System;

namespace Twiglet.CS2J.Translator.Transform
{
    internal class GrammarRuleAttribute : Attribute
    {
        private string v;

        public GrammarRuleAttribute(string v)
        {
            this.v = v;
        }
    }
}