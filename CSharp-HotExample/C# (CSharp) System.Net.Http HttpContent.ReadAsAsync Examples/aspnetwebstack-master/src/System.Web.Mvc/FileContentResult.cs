// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;

namespace System.Web.Mvc
{
    public class FileContentResult : FileResult
    {
        public FileContentResult(byte[] fileContents, string contentType)
            : base(contentType)
        {
            if (fileContents == null)
            {
                throw new ArgumentNullException("fileContents");
            }

            FileContents = fileContents;
        }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "There's no reason to tamper-proof this array since it's supplied to the type's constructor.")]
        public byte[] FileContents { get; private set; }

        protected override void WriteFile(HttpResponseBase response)
        {
            response.OutputStream.Write(FileContents, 0, FileContents.Length);
        }
    }
}
