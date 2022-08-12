using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Antlr.Runtime
{
 
        public interface IRuleReturnScope<TLabel>
        {
            TLabel Start { get; }

            TLabel Stop { get; }
        }
    

}
