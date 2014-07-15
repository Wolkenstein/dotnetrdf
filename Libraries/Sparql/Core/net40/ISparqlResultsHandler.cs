/*
dotNetRDF is free and open source software licensed under the MIT License

-----------------------------------------------------------------------------

Copyright (c) 2009-2012 dotNetRDF Project (dotnetrdf-developer@lists.sf.net)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is furnished
to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using VDS.RDF.Nodes;
using VDS.RDF.Query;
using VDS.RDF.Query.Results;

namespace VDS.RDF
{
    /// <summary>
    /// Interface for Handlers which handle the SPARQL Results produced by parsers
    /// </summary>
    public interface ISparqlResultsHandler : INodeFactory
    {
        /// <summary>
        /// Starts the Handling of Results
        /// </summary>
        void StartResults();

        /// <summary>
        /// Ends the Handling of Results
        /// </summary>
        /// <param name="ok">Indicates whether parsing completed without error</param>
        void EndResults(bool ok);

        /// <summary>
        /// Handles a Boolean Result
        /// </summary>
        /// <param name="result">Result</param>
        void HandleBooleanResult(bool result);

        /// <summary>
        /// Handles a Variable Declaration
        /// </summary>
        /// <param name="var">Variable Name</param>
        /// <returns></returns>
        bool HandleVariable(String var);

        /// <summary>
        /// Handles a SPARQL Result
        /// </summary>
        /// <param name="result">Result</param>
        /// <returns></returns>
        bool HandleResult(IResultRow result);
    }
}
