// Python Tools for Visual Studio
// Copyright(c) Microsoft Corporation
// All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the License); you may not use
// this file except in compliance with the License. You may obtain a copy of the
// License at http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS
// OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY
// IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
// MERCHANTABILITY OR NON-INFRINGEMENT.
//
// See the Apache Version 2.0 License for specific language governing
// permissions and limitations under the License.

using System.Collections.Generic;
using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {

    public class DelStatement : Statement {
        private readonly Expression[] _expressions;

        public DelStatement(Expression[] expressions) {
            _expressions = expressions;
        }

        public IList<Expression> Expressions {
            get { return _expressions; }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_expressions != null) {
                    foreach (Expression expression in _expressions) {
                        expression.Walk(walker);
                    }
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeStringStmt(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            ListExpression.AppendItems(res, ast, format, "del", "", this, Expressions);
        }
    }
}
