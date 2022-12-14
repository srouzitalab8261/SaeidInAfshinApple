// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ActionNameAttribute : ActionNameSelectorAttribute
    {
        public ActionNameAttribute(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException(MvcResources.Common_NullOrEmpty, "name");
            }

            Name = name;
        }

        public string Name { get; private set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            return String.Equals(actionName, Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
