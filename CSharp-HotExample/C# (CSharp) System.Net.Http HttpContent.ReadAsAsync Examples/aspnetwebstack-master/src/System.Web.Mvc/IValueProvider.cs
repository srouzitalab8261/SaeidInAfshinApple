// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Web.Mvc
{
    /// <summary>
    /// Defines the methods that are required for a value provider in ASP.NET MVC.
    /// </summary>
    public interface IValueProvider
    {
        /// <summary>
        /// Determines whether the collection contains the specified prefix.
        /// </summary>
        /// <param name="prefix">The prefix to search for.</param>
        /// <returns>true if the collection contains the specified prefix; otherwise, false.</returns>
        bool ContainsPrefix(string prefix);

        /// <summary>
        /// Retrieves a value object using the specified key.
        /// </summary>
        /// <param name="key">The key of the value object to retrieve.</param>
        /// <returns>The value object for the specified key. If the exact key is not found, null.</returns>
        ValueProviderResult GetValue(string key);
    }
}
