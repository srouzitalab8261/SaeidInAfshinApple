// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Web.Mvc
{
    public abstract class ValueProviderFactory
    {
        public abstract IValueProvider GetValueProvider(ControllerContext controllerContext);
    }
}
