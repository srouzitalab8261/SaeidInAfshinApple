// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Web.Mvc
{
    public class ModelValidationResult
    {
        private string _memberName;
        private string _message;

        public string MemberName
        {
            get { return _memberName ?? String.Empty; }
            set { _memberName = value; }
        }

        public string Message
        {
            get { return _message ?? String.Empty; }
            set { _message = value; }
        }
    }
}
