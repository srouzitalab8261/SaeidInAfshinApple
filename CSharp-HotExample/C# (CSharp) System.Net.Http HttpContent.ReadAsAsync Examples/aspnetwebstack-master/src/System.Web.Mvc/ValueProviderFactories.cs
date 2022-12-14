// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Web.Mvc
{
    public static class ValueProviderFactories
    {
        private static readonly ValueProviderFactoryCollection _factories = new ValueProviderFactoryCollection()
        {
            new ChildActionValueProviderFactory(),
            new FormValueProviderFactory(),
            new JsonValueProviderFactory(),
            new RouteDataValueProviderFactory(),
            new QueryStringValueProviderFactory(),
            new HttpFileCollectionValueProviderFactory(),
            new JQueryFormValueProviderFactory()
        };

        public static ValueProviderFactoryCollection Factories
        {
            get { return _factories; }
        }
    }
}
