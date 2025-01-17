/*
 * [The "BSD licence"]
 * Copyright (c) 2005-2008 Terence Parr
 * All rights reserved.
 *
 * Conversion to C#:
 * Copyright (c) 2008-2009 Sam Harwell, Pixel Mine, Inc.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 * 3. The name of the author may not be used to endorse or promote products
 *    derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

namespace Antlr.Runtime {
    using System.Collections.Generic;
    using CLSCompliant = System.CLSCompliantAttribute;
    using ArgumentNullException = System.ArgumentNullException;

    /** <summary>
     *  The set of fields needed by an abstract recognizer to recognize input
     *  and recover from errors etc...  As a separate state object, it can be
     *  shared among multiple grammars; e.g., when one grammar imports another.
     *  </summary>
     *
     *  <remarks>
     *  These fields are publically visible but the actual state pointer per
     *  parser is protected.
     *  </remarks>
     */
    public class RecognizerSharedState {
        /** <summary>
         *  Track the set of token types that can follow any rule invocation.
         *  Stack grows upwards.  When it hits the max, it grows 2x in size
         *  and keeps going.
         *  </summary>
         */
        //public List<BitSet> following;
        public BitSet[] following;
        public int _fsp;

        /** <summary>
         *  This is true when we see an error and before having successfully
         *  matched a token.  Prevents generation of more than one error message
         *  per error.
         *  </summary>
         */
        public bool errorRecovery;

        /** <summary>
         *  The index into the input stream where the last error occurred.
         * 	This is used to prevent infinite loops where an error is found
         *  but no token is consumed during recovery...another error is found,
         *  ad naseum.  This is a failsafe mechanism to guarantee that at least
         *  one token/tree node is consumed for two errors.
         *  </summary>
         */
        public int lastErrorIndex;

        /** <summary>
         *  In lieu of a return value, this indicates that a rule or token
         *  has failed to match.  Reset to false upon valid token match.
         *  </summary>
         */
        public bool failed;

        /** <summary>Did the recognizer encounter a syntax error?  Track how many.</summary> */
        public int syntaxErrors;

        /** <summary>
         *  If 0, no backtracking is going on.  Safe to exec actions etc...
         *  If >0 then it's the level of backtracking.
         *  </summary>
         */
        public int backtracking;

        /** <summary>
         *  An array[size num rules] of Map<Integer,Integer> that tracks
         *  the stop token index for each rule.  ruleMemo[ruleIndex] is
         *  the memoization table for ruleIndex.  For key ruleStartIndex, you
         *  get back the stop token for associated rule or MEMO_RULE_FAILED.
         *  </summary>
         *
         *  <remarks>This is only used if rule memoization is on (which it is by default).</remarks>
         */
        public IDictionary<int, int>[] ruleMemo;


        // LEXER FIELDS (must be in same state object to avoid casting
        //               constantly in generated code and Lexer object) :(


        /** <summary>
         *  The goal of all lexer rules/methods is to create a token object.
         *  This is an instance variable as multiple rules may collaborate to
         *  create a single token.  nextToken will return this object after
         *  matching lexer rule(s).  If you subclass to allow multiple token
         *  emissions, then set this to the last token to be matched or
         *  something nonnull so that the auto token emit mechanism will not
         *  emit another token.
         *  </summary>
         */
        public IToken token;

        /** <summary>
         *  What character index in the stream did the current token start at?
         *  Needed, for example, to get the text for current token.  Set at
         *  the start of nextToken.
         *  </summary>
         */
        public int tokenStartCharIndex;

        /** <summary>The line on which the first character of the token resides</summary> */
        public int tokenStartLine;

        /** <summary>The character position of first character within the line</summary> */
        public int tokenStartCharPositionInLine;

        /** <summary>The channel number for the current token</summary> */
        public int channel;

        /** <summary>The token type for the current token</summary> */
        public int type;

        /** <summary>
         *  You can set the text for the current token to override what is in
         *  the input char buffer.  Use setText() or can set this instance var.
         *  </summary>
         */
        public string text;

        public RecognizerSharedState() {
            //following = new List<BitSet>( BaseRecognizer.InitialFollowStackSize );
            following = new BitSet[BaseRecognizer.InitialFollowStackSize];
            _fsp = -1;
            lastErrorIndex = -1;
            tokenStartCharIndex = -1;
        }

        public RecognizerSharedState(RecognizerSharedState state) {
            if (state == null)
                throw new ArgumentNullException("state");

            following = (BitSet[])state.following.Clone();
            _fsp = state._fsp;
            errorRecovery = state.errorRecovery;
            lastErrorIndex = state.lastErrorIndex;
            failed = state.failed;
            syntaxErrors = state.syntaxErrors;
            backtracking = state.backtracking;

            if (state.ruleMemo != null)
                ruleMemo = (IDictionary<int, int>[])state.ruleMemo.Clone();

            token = state.token;
            tokenStartCharIndex = state.tokenStartCharIndex;
            tokenStartCharPositionInLine = state.tokenStartCharPositionInLine;
            channel = state.channel;
            type = state.type;
            text = state.text;
        }
    }
}
