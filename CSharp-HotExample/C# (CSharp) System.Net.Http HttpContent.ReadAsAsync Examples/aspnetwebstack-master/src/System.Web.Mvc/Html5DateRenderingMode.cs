// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Web.Mvc
{
    /// <summary>
    /// Controls the value-rendering method For HTML5 input elements of types such as date, time, datetime and datetime-local.
    /// </summary>
    public enum Html5DateRenderingMode
    {
        /// <summary>
        /// Render date and time values according to the current culture's ToString behavior.
        /// </summary>
        CurrentCulture = 0,

        /// <summary>
        /// Render date and time values as Rfc3339 compliant strings to support HTML5 date and time types of input elements.
        /// </summary>
        Rfc3339,
    }
}