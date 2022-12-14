// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc.Html;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.WebPages;
using System.Web.WebPages.Scope;

namespace System.Web.Mvc
{
    public class HtmlHelper
    {
        public static readonly string ValidationInputCssClassName = "input-validation-error";
        public static readonly string ValidationInputValidCssClassName = "input-validation-valid";
        public static readonly string ValidationMessageCssClassName = "field-validation-error";
        public static readonly string ValidationMessageValidCssClassName = "field-validation-valid";
        public static readonly string ValidationSummaryCssClassName = "validation-summary-errors";
        public static readonly string ValidationSummaryValidCssClassName = "validation-summary-valid";

        private static readonly object _html5InputsModeKey = new object();

        private DynamicViewDataDictionary _dynamicViewDataDictionary;

        public HtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
        }

        public HtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }
            if (viewDataContainer == null)
            {
                throw new ArgumentNullException("viewDataContainer");
            }
            if (routeCollection == null)
            {
                throw new ArgumentNullException("routeCollection");
            }

            ViewContext = viewContext;
            ViewDataContainer = viewDataContainer;
            RouteCollection = routeCollection;
            ClientValidationRuleFactory = (name, metadata) => ModelValidatorProviders.Providers.GetValidators(metadata ?? ModelMetadata.FromStringExpression(name, ViewData), ViewContext).SelectMany(v => v.GetClientValidationRules());
        }

        public static bool ClientValidationEnabled
        {
            get { return ViewContext.GetClientValidationEnabled(); }
            set { ViewContext.SetClientValidationEnabled(value); }
        }

        public static string IdAttributeDotReplacement
        {
            get { return WebPages.Html.HtmlHelper.IdAttributeDotReplacement; }
            set { WebPages.Html.HtmlHelper.IdAttributeDotReplacement = value; }
        }

        internal Func<string, ModelMetadata, IEnumerable<ModelClientValidationRule>> ClientValidationRuleFactory { get; set; }

        public RouteCollection RouteCollection { get; private set; }

        public static bool UnobtrusiveJavaScriptEnabled
        {
            get { return ViewContext.GetUnobtrusiveJavaScriptEnabled(); }
            set { ViewContext.SetUnobtrusiveJavaScriptEnabled(value); }
        }

        /// <summary>
        /// Element name used to wrap a top-level message in
        /// <see cref="ValidationExtensions.ValidationSummary(HtmlHelper)"/> and other overloads.
        /// </summary>
        public static string ValidationSummaryMessageElement
        {
            get { return ViewContext.GetValidationSummaryMessageElement(); }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw Error.ParameterCannotBeNullOrEmpty("value");
                }

                ViewContext.SetValidationSummaryMessageElement(value);
            }
        }

        /// <summary>
        /// Element name used to wrap the validation message generated by
        /// <see cref="ValidationExtensions.ValidationMessage(HtmlHelper, String)"/> and other overloads.
        /// </summary>
        public static string ValidationMessageElement
        {
            get { return ViewContext.GetValidationMessageElement(); }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw Error.ParameterCannotBeNullOrEmpty("value");
                }

                ViewContext.SetValidationMessageElement(value);
            }
        }

        public dynamic ViewBag
        {
            get
            {
                if (_dynamicViewDataDictionary == null)
                {
                    _dynamicViewDataDictionary = new DynamicViewDataDictionary(() => ViewData);
                }
                return _dynamicViewDataDictionary;
            }
        }

        public ViewContext ViewContext { get; private set; }

        public ViewDataDictionary ViewData
        {
            get { return ViewDataContainer.ViewData; }
        }

        public IViewDataContainer ViewDataContainer { get; internal set; }

        /// <summary>
        /// Creates a dictionary of HTML attributes from the input object, 
        /// translating underscores to dashes.
        /// </summary>
        /// <example>
        /// <c>new { data_name="value" }</c> will translate to the entry <c>{ "data-name" , "value" }</c>
        /// in the resulting dictionary.
        /// </example>
        /// <param name="htmlAttributes">Anonymous object describing HTML attributes.</param>
        /// <returns>A dictionary that represents HTML attributes.</returns>
        public static RouteValueDictionary AnonymousObjectToHtmlAttributes(object htmlAttributes)
        {
            return System.Web.WebPages.Html.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "For consistency, all helpers are instance methods.")]
        public MvcHtmlString AntiForgeryToken()
        {
            return new MvcHtmlString(AntiForgery.GetHtml().ToString());
        }

        /// <summary>
        /// Set this property to <see cref="Mvc.Html5DateRenderingMode.Rfc3339"/> to have templated helpers such as Html.EditorFor render date and time
        /// values as Rfc3339 compliant strings.
        /// </summary>
        /// <remarks>
        /// The scope of this setting is for the current view alone. Sub views and parent views
        /// will default to <see cref="Mvc.Html5DateRenderingMode.CurrentCulture"/> unless explicitly set otherwise.
        /// </remarks>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The usage of the property is as an instance property of the helper.")]
        public Html5DateRenderingMode Html5DateRenderingMode
        {
            get
            {
                object value;
                if (ScopeStorage.CurrentScope.TryGetValue(_html5InputsModeKey, out value))
                {
                    return (Html5DateRenderingMode)value;
                }
                return default(Html5DateRenderingMode);
            }
            set
            {
                ScopeStorage.CurrentScope[_html5InputsModeKey] = value;
            }
        }

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AdditionalDataProvider", Justification = "API name.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AntiForgeryConfig", Justification = "API name.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AntiForgeryToken", Justification = "API name.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "httpCookies", Justification = "API name.")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Method is obsolete.")]
        [Obsolete("This method is deprecated. Use the AntiForgeryToken() method instead. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.", error: true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public MvcHtmlString AntiForgeryToken(string salt)
        {
            if (!String.IsNullOrEmpty(salt))
            {
                throw new NotSupportedException("This method is deprecated. Use the AntiForgeryToken() method instead. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.");
            }

            return AntiForgeryToken();
        }

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AdditionalDataProvider", Justification = "API name.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AntiForgeryConfig", Justification = "API name.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AntiForgeryToken", Justification = "API name.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "httpCookies", Justification = "API name.")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Method is obsolete.")]
        [Obsolete("This method is deprecated. Use the AntiForgeryToken() method instead. To specify a custom domain for the generated cookie, use the <httpCookies> configuration element. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.", error: true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public MvcHtmlString AntiForgeryToken(string salt, string domain, string path)
        {
            if (!String.IsNullOrEmpty(salt) || !String.IsNullOrEmpty(domain) || !String.IsNullOrEmpty(path))
            {
                throw new NotSupportedException("This method is deprecated. Use the AntiForgeryToken() method instead. To specify a custom domain for the generated cookie, use the <httpCookies> configuration element. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.");
            }

            return AntiForgeryToken();
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "For consistency, all helpers are instance methods.")]
        public string AttributeEncode(string value)
        {
            return (!String.IsNullOrEmpty(value)) ? HttpUtility.HtmlAttributeEncode(value) : String.Empty;
        }

        public string AttributeEncode(object value)
        {
            return AttributeEncode(Convert.ToString(value, CultureInfo.InvariantCulture));
        }

        public void EnableClientValidation()
        {
            EnableClientValidation(enabled: true);
        }

        public void EnableClientValidation(bool enabled)
        {
            ViewContext.ClientValidationEnabled = enabled;
        }

        public void EnableUnobtrusiveJavaScript()
        {
            EnableUnobtrusiveJavaScript(enabled: true);
        }

        public void EnableUnobtrusiveJavaScript(bool enabled)
        {
            ViewContext.UnobtrusiveJavaScriptEnabled = enabled;
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "For consistency, all helpers are instance methods.")]
        public string Encode(string value)
        {
            return (!String.IsNullOrEmpty(value)) ? HttpUtility.HtmlEncode(value) : String.Empty;
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "For consistency, all helpers are instance methods.")]
        public string Encode(object value)
        {
            return value != null ? HttpUtility.HtmlEncode(value) : String.Empty;
        }

        internal string EvalString(string key)
        {
            return Convert.ToString(ViewData.Eval(key), CultureInfo.CurrentCulture);
        }

        internal string EvalString(string key, string format)
        {
            return Convert.ToString(ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "For consistency, all helpers are instance methods.")]
        public string FormatValue(object value, string format)
        {
            return ViewDataDictionary.FormatValueInternal(value, format);
        }

        internal bool EvalBoolean(string key)
        {
            return Convert.ToBoolean(ViewData.Eval(key), CultureInfo.InvariantCulture);
        }

        internal static IView FindPartialView(ViewContext viewContext, string partialViewName, ViewEngineCollection viewEngineCollection)
        {
            ViewEngineResult result = viewEngineCollection.FindPartialView(viewContext, partialViewName);
            if (result.View != null)
            {
                return result.View;
            }

            StringBuilder locationsText = new StringBuilder();
            foreach (string location in result.SearchedLocations)
            {
                locationsText.AppendLine();
                locationsText.Append(location);
            }

            throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                                                              MvcResources.Common_PartialViewNotFound, partialViewName, locationsText));
        }

        public static string GenerateIdFromName(string name)
        {
            return GenerateIdFromName(name, IdAttributeDotReplacement);
        }

        public static string GenerateIdFromName(string name, string idAttributeDotReplacement)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (idAttributeDotReplacement == null)
            {
                throw new ArgumentNullException("idAttributeDotReplacement");
            }

            // TagBuilder.CreateSanitizedId returns null for empty strings, return String.Empty instead to avoid breaking change
            if (name.Length == 0)
            {
                return String.Empty;
            }

            return TagBuilder.CreateSanitizedId(name, idAttributeDotReplacement);
        }

        public static string GenerateLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return GenerateLink(requestContext, routeCollection, linkText, routeName, actionName, controllerName, null /* protocol */, null /* hostName */, null /* fragment */, routeValues, htmlAttributes);
        }

        public static string GenerateLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return GenerateLinkInternal(requestContext, routeCollection, linkText, routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes, true /* includeImplicitMvcValues */);
        }

        private static string GenerateLinkInternal(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool includeImplicitMvcValues)
        {
            string url = UrlHelper.GenerateUrl(routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues);
            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = (!String.IsNullOrEmpty(linkText)) ? HttpUtility.HtmlEncode(linkText) : String.Empty
            };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("href", url);
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        public static string GenerateRouteLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return GenerateRouteLink(requestContext, routeCollection, linkText, routeName, null /* protocol */, null /* hostName */, null /* fragment */, routeValues, htmlAttributes);
        }

        public static string GenerateRouteLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return GenerateLinkInternal(requestContext, routeCollection, linkText, routeName, null /* actionName */, null /* controllerName */, protocol, hostName, fragment, routeValues, htmlAttributes, false /* includeImplicitMvcValues */);
        }

        public static string GetFormMethodString(FormMethod method)
        {
            switch (method)
            {
                case FormMethod.Get:
                    return "get";
                case FormMethod.Post:
                    return "post";
                default:
                    return "post";
            }
        }

        public static string GetInputTypeString(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.CheckBox:
                    return "checkbox";
                case InputType.Hidden:
                    return "hidden";
                case InputType.Password:
                    return "password";
                case InputType.Radio:
                    return "radio";
                case InputType.Text:
                    return "text";
                default:
                    return "text";
            }
        }

        internal object GetModelStateValue(string key, Type destinationType)
        {
            ModelState modelState;
            if (ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null /* culture */);
                }
            }
            return null;
        }

        public IDictionary<string, object> GetUnobtrusiveValidationAttributes(string name)
        {
            return GetUnobtrusiveValidationAttributes(name, metadata: null);
        }

        // Only render attributes if unobtrusive client-side validation is enabled, and then only if we've
        // never rendered validation for a field with this name in this form. Also, if there's no form context,
        // then we can't render the attributes (we'd have no <form> to attach them to).
        public IDictionary<string, object> GetUnobtrusiveValidationAttributes(string name, ModelMetadata metadata)
        {
            Dictionary<string, object> results = new Dictionary<string, object>();

            // The ordering of these 3 checks (and the early exits) is for performance reasons.
            if (!ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                return results;
            }

            FormContext formContext = ViewContext.GetFormContextForClientValidation();
            if (formContext == null)
            {
                return results;
            }

            string fullName = ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (formContext.RenderedField(fullName))
            {
                return results;
            }

            formContext.RenderedField(fullName, true);

            IEnumerable<ModelClientValidationRule> clientRules = ClientValidationRuleFactory(name, metadata);
            UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(clientRules, results);

            return results;
        }

        public MvcHtmlString HttpMethodOverride(HttpVerbs httpVerb)
        {
            string httpMethod;
            switch (httpVerb)
            {
                case HttpVerbs.Delete:
                    httpMethod = "DELETE";
                    break;
                case HttpVerbs.Head:
                    httpMethod = "HEAD";
                    break;
                case HttpVerbs.Put:
                    httpMethod = "PUT";
                    break;
                case HttpVerbs.Patch:
                    httpMethod = "PATCH";
                    break;
                case HttpVerbs.Options:
                    httpMethod = "OPTIONS";
                    break;
                default:
                    throw new ArgumentException(MvcResources.HtmlHelper_InvalidHttpVerb, "httpVerb");
            }

            return HttpMethodOverride(httpMethod);
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "For consistency, all helpers are instance methods.")]
        public MvcHtmlString HttpMethodOverride(string httpMethod)
        {
            if (String.IsNullOrEmpty(httpMethod))
            {
                throw new ArgumentException(MvcResources.Common_NullOrEmpty, "httpMethod");
            }
            if (String.Equals(httpMethod, "GET", StringComparison.OrdinalIgnoreCase) ||
                String.Equals(httpMethod, "POST", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(MvcResources.HtmlHelper_InvalidHttpMethod, "httpMethod");
            }

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes["type"] = "hidden";
            tagBuilder.Attributes["name"] = HttpRequestExtensions.XHttpMethodOverrideKey;
            tagBuilder.Attributes["value"] = httpMethod;

            return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
        }

        /// <summary>
        /// Wraps HTML markup in an IHtmlString, which will enable HTML markup to be
        /// rendered to the output without getting HTML encoded.
        /// </summary>
        /// <param name="value">HTML markup string.</param>
        /// <returns>An IHtmlString that represents HTML markup.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "For consistency, all helpers are instance methods.")]
        public IHtmlString Raw(string value)
        {
            return new HtmlString(value);
        }

        /// <summary>
        /// Wraps HTML markup from the string representation of an object in an IHtmlString,
        /// which will enable HTML markup to be rendered to the output without getting HTML encoded.
        /// </summary>
        /// <param name="value">object with string representation as HTML markup</param>
        /// <returns>An IHtmlString that represents HTML markup.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "For consistency, all helpers are instance methods.")]
        public IHtmlString Raw(object value)
        {
            return new HtmlString(value == null ? null : value.ToString());
        }

        internal virtual void RenderPartialInternal(string partialViewName, ViewDataDictionary viewData, object model, TextWriter writer, ViewEngineCollection viewEngineCollection)
        {
            if (String.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException(MvcResources.Common_NullOrEmpty, "partialViewName");
            }

            ViewDataDictionary newViewData = null;

            if (model == null)
            {
                if (viewData == null)
                {
                    newViewData = new ViewDataDictionary(ViewData);
                }
                else
                {
                    newViewData = new ViewDataDictionary(viewData);
                }
            }
            else
            {
                if (viewData == null)
                {
                    newViewData = new ViewDataDictionary(model);
                }
                else
                {
                    newViewData = new ViewDataDictionary(viewData) { Model = model };
                }
            }

            ViewContext newViewContext = new ViewContext(ViewContext, ViewContext.View, newViewData, ViewContext.TempData, writer);
            IView view = FindPartialView(newViewContext, partialViewName, viewEngineCollection);
            view.Render(newViewContext, writer);
        }

        /// <summary>
        /// Set element name used to wrap a top-level message in
        /// <see cref="ValidationExtensions.ValidationSummary(HtmlHelper)"/> and other overloads.
        /// </summary>
        public void SetValidationSummaryMessageElement(string elementName)
        {
            if (String.IsNullOrEmpty(elementName))
            {
                throw Error.ParameterCannotBeNullOrEmpty("elementName");
            }

            ViewContext.ValidationSummaryMessageElement = elementName;
        }

        /// <summary>
        /// Set element name used to wrap the validation message generated by
        /// <see cref="ValidationExtensions.ValidationMessage(HtmlHelper, String)"/> and other overloads.
        /// </summary>
        public void SetValidationMessageElement(string elementName)
        {
            if (String.IsNullOrEmpty(elementName))
            {
                throw Error.ParameterCannotBeNullOrEmpty("elementName");
            }

            ViewContext.ValidationMessageElement = elementName;
        }

        /// <summary>
        /// Creates a dictionary from an object, by adding each public instance property as a key with its associated 
        /// value to the dictionary. It will expose public properties from derived types as well. This is typically used
        /// with objects of an anonymous type.
        /// </summary>
        /// <example>
        /// <c>new { property_name = "value" }</c> will translate to the entry <c>{ "property_name" , "value" }</c>
        /// in the resulting dictionary.
        /// </example>
        /// <param name="value">The object to be converted.</param>
        /// <returns>The created dictionary of property names and property values.</returns>
        public static IDictionary<string, object> ObjectToDictionary(object value)
        {
            return TypeHelper.ObjectToDictionary(value);
        }
    }
}
