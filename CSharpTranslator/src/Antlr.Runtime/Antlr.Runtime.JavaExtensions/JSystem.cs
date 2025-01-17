﻿/*
 * [The "BSD licence"]
 * Copyright (c) 2005-2008 Terence Parr
 * All rights reserved.
 *
 * Conversion to C#:
 * Copyright (c) 2008 Sam Harwell, Pixel Mine, Inc.
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

#if !DEBUG
namespace Antlr.Runtime.JavaExtensions
{
    using System;
    using System.IO;

    public static class JSystem
    {
        [Obsolete]
        public static TextWriter err
        {
            get
            {
                return Console.Error;
            }
        }

        [Obsolete]
        public static TextWriter @out
        {
            get
            {
                return Console.Out;
            }
        }

        [Obsolete]
        public static void arraycopy<T>( T[] sourceArray, int sourceIndex, T[] destinationArray, int destinationIndex, int length )
        {
            Array.Copy( sourceArray, sourceIndex, destinationArray, destinationIndex, length );
        }

        [Obsolete]
        public static string getProperty( string name )
        {
            switch ( name )
            {
            case "file.encoding":
                return System.Text.Encoding.Default.WebName;

            case "line.separator":
                return Environment.NewLine;

            case "java.io.tmpdir":
                return System.IO.Path.GetTempPath();

            case "user.home":
                return Environment.GetFolderPath( Environment.SpecialFolder.Personal );

            default:
                throw new ArgumentException( string.Format( "Unknown system property: '{0}'", name ) );
            }
        }

    }
}
#endif
