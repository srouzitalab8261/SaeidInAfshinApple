// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;

namespace System.Web.Mvc
{
    public class ActionExecutedContext : ControllerContext
    {
        private ActionResult _result;

        // parameterless constructor used for mocking
        public ActionExecutedContext()
        {
        }

        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "The virtual property setters are only to support mocking frameworks, in which case this constructor shouldn't be called anyway.")]
        public ActionExecutedContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor, bool canceled, Exception exception)
            : base(controllerContext)
        {
            if (actionDescriptor == null)
            {
                throw new ArgumentNullException("actionDescriptor");
            }

            ActionDescriptor = actionDescriptor;
            Canceled = canceled;
            Exception = exception;
        }

        public virtual ActionDescriptor ActionDescriptor { get; set; }

        public virtual bool Canceled { get; set; }

        public virtual Exception Exception { get; set; }

        public bool ExceptionHandled { get; set; }

        public ActionResult Result
        {
            get { return _result ?? EmptyResult.Instance; }
            set { _result = value; }
        }
    }
}
