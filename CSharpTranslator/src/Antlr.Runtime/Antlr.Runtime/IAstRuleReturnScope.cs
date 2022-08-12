using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Antlr.Runtime
{
    public interface IAstRuleReturnScope<TAstLabel>
    {
        TAstLabel Tree { get; }
    }
}
