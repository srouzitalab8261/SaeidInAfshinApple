// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if ASPNETWEBAPI
namespace System.Web.Http.Routing
#else
namespace System.Web.Mvc.Routing
#endif
{
    // Represents a literal subsegment of a ContentPathSegment
    internal sealed class PathLiteralSubsegment : PathSubsegment
    {
        public PathLiteralSubsegment(string literal)
        {
            Literal = literal;
        }

        public string Literal { get; private set; }

#if ROUTE_DEBUGGING
        public override string LiteralText
        {
            get
            {
                return Literal;
            }
        }

        public override string ToString()
        {
            return "\"" + Literal + "\"";
        }
#endif
    }
}
