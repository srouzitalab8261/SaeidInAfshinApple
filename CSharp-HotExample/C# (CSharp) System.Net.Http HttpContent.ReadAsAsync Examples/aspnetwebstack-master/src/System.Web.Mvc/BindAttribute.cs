// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class BindAttribute : Attribute
    {
        private string _exclude;
        private string[] _excludeSplit = new string[0];
        private string _include;
        private string[] _includeSplit = new string[0];

        public string Exclude
        {
            get { return _exclude ?? String.Empty; }
            set
            {
                _exclude = value;
                _excludeSplit = AuthorizeAttribute.SplitString(value);
            }
        }

        public string Include
        {
            get { return _include ?? String.Empty; }
            set
            {
                _include = value;
                _includeSplit = AuthorizeAttribute.SplitString(value);
            }
        }

        public string Prefix { get; set; }

        internal static bool IsPropertyAllowed(string propertyName, ICollection<string> includeProperties, ICollection<string> excludeProperties)
        {
            // We allow a property to be bound if its both in the include list AND not in the exclude list.
            // An empty include list implies all properties are allowed.
            // An empty exclude list implies no properties are disallowed.
            bool includeProperty = (includeProperties == null) || (includeProperties.Count == 0) || includeProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
            bool excludeProperty = (excludeProperties != null) && excludeProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
            return includeProperty && !excludeProperty;
        }

        public bool IsPropertyAllowed(string propertyName)
        {
            return IsPropertyAllowed(propertyName, _includeSplit, _excludeSplit);
        }
    }
}
