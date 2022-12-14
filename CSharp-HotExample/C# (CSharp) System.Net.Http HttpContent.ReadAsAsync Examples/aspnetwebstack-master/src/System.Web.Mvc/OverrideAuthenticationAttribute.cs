// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Web.Mvc.Filters;

namespace System.Web.Mvc
{
    /// <summary>
    /// Represents a filter attribute that overrides authentication filters defined at a higher level.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class OverrideAuthenticationAttribute : FilterAttribute, IOverrideFilter
    {
        /// <inheritdoc />
        public Type FiltersToOverride
        {
            get { return typeof(IAuthenticationFilter); }
        }
    }
}
