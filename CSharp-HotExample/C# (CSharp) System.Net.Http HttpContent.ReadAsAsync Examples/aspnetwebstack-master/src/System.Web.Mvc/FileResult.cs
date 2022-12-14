// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Net.Mime;
using System.Text;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
    public abstract class FileResult : ActionResult
    {
        private string _fileDownloadName;

        protected FileResult(string contentType)
        {
            if (String.IsNullOrEmpty(contentType))
            {
                throw new ArgumentException(MvcResources.Common_NullOrEmpty, "contentType");
            }

            ContentType = contentType;
        }

        public string ContentType { get; private set; }

        public string FileDownloadName
        {
            get { return _fileDownloadName ?? String.Empty; }
            set { _fileDownloadName = value; }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = ContentType;

            if (!String.IsNullOrEmpty(FileDownloadName))
            {
                // From RFC 2183, Sec. 2.3:
                // The sender may want to suggest a filename to be used if the entity is
                // detached and stored in a separate file. If the receiving MUA writes
                // the entity to a file, the suggested filename should be used as a
                // basis for the actual filename, where possible.
                string headerValue = ContentDispositionUtil.GetHeaderValue(FileDownloadName);
                context.HttpContext.Response.AddHeader("Content-Disposition", headerValue);
            }

            WriteFile(response);
        }

        protected abstract void WriteFile(HttpResponseBase response);

        internal static class ContentDispositionUtil
        {
            private const string HexDigits = "0123456789ABCDEF";

            private static void AddByteToStringBuilder(byte b, StringBuilder builder)
            {
                builder.Append('%');

                int i = b;
                AddHexDigitToStringBuilder(i >> 4, builder);
                AddHexDigitToStringBuilder(i % 16, builder);
            }

            private static void AddHexDigitToStringBuilder(int digit, StringBuilder builder)
            {
                builder.Append(HexDigits[digit]);
            }

            private static string CreateRfc2231HeaderValue(string filename)
            {
                StringBuilder builder = new StringBuilder("attachment; filename*=UTF-8''");

                byte[] filenameBytes = Encoding.UTF8.GetBytes(filename);
                foreach (byte b in filenameBytes)
                {
                    if (IsByteValidHeaderValueCharacter(b))
                    {
                        builder.Append((char)b);
                    }
                    else
                    {
                        AddByteToStringBuilder(b, builder);
                    }
                }

                return builder.ToString();
            }

            public static string GetHeaderValue(string fileName)
            {
                // If fileName contains any Unicode characters, encode according
                // to RFC 2231 (with clarifications from RFC 5987)
                foreach (char c in fileName)
                {
                    if ((int)c > 127)
                    {
                        return CreateRfc2231HeaderValue(fileName);
                    }
                }

                // Knowing there are no Unicode characters in this fileName, rely on
                // ContentDisposition.ToString() to encode properly.
                // In .Net 4.0, ContentDisposition.ToString() throws FormatException if
                // the file name contains Unicode characters.
                // In .Net 4.5, ContentDisposition.ToString() no longer throws FormatException
                // if it contains Unicode, and it will not encode Unicode as we require here.
                // The Unicode test above is identical to the 4.0 FormatException test,
                // allowing this helper to give the same results in 4.0 and 4.5.         
                ContentDisposition disposition = new ContentDisposition() { FileName = fileName };
                return disposition.ToString();
            }

            // Application of RFC 2231 Encoding to Hypertext Transfer Protocol (HTTP) Header Fields, sec. 3.2
            // http://greenbytes.de/tech/webdav/draft-reschke-rfc2231-in-http-latest.html
            private static bool IsByteValidHeaderValueCharacter(byte b)
            {
                if ((byte)'0' <= b && b <= (byte)'9')
                {
                    return true; // is digit
                }
                if ((byte)'a' <= b && b <= (byte)'z')
                {
                    return true; // lowercase letter
                }
                if ((byte)'A' <= b && b <= (byte)'Z')
                {
                    return true; // uppercase letter
                }

                switch (b)
                {
                    case (byte)'-':
                    case (byte)'.':
                    case (byte)'_':
                    case (byte)'~':
                    case (byte)':':
                    case (byte)'!':
                    case (byte)'$':
                    case (byte)'&':
                    case (byte)'+':
                        return true;
                }

                return false;
            }
        }
    }
}
