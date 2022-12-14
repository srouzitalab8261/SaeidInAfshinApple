// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
    [AttributeUsage(ValidTargets, AllowMultiple = false, Inherited = false)]
    public sealed class ModelBinderAttribute : CustomModelBinderAttribute
    {
        public ModelBinderAttribute(Type binderType)
        {
            if (binderType == null)
            {
                throw new ArgumentNullException("binderType");
            }
            if (!typeof(IModelBinder).IsAssignableFrom(binderType))
            {
                string message = String.Format(CultureInfo.CurrentCulture,
                                               MvcResources.ModelBinderAttribute_TypeNotIModelBinder, binderType.FullName);
                throw new ArgumentException(message, "binderType");
            }

            BinderType = binderType;
        }

        public Type BinderType { get; private set; }

        public override IModelBinder GetBinder()
        {
            try
            {
                return (IModelBinder)Activator.CreateInstance(BinderType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        MvcResources.ModelBinderAttribute_ErrorCreatingModelBinder,
                        BinderType.FullName),
                    ex);
            }
        }
    }
}
