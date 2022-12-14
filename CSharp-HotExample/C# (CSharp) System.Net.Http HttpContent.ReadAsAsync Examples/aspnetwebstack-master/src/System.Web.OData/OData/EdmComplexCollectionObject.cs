// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;
using System.Web.OData.Properties;
using Microsoft.OData.Edm;

namespace System.Web.OData
{
    /// <summary>
    /// Represents an <see cref="IEdmObject"/> that is a collection of <see cref="IEdmComplexObject"/>s.
    /// </summary>
    public class EdmComplexObjectCollection : Collection<IEdmComplexObject>, IEdmObject
    {
        private IEdmCollectionTypeReference _edmType;

        /// <summary>
        /// Initialzes a new instance of the <see cref="EdmComplexObjectCollection"/> class.
        /// </summary>
        /// <param name="edmType">The edm type of the collection.</param>
        public EdmComplexObjectCollection(IEdmCollectionTypeReference edmType)
        {
            Initialize(edmType);
        }

        /// <summary>
        /// Initialzes a new instance of the <see cref="EdmComplexObjectCollection"/> class.
        /// </summary>
        /// <param name="edmType">The edm type of the collection.</param>
        /// <param name="list">The list that is wrapped by the new collection.</param>
        public EdmComplexObjectCollection(IEdmCollectionTypeReference edmType, IList<IEdmComplexObject> list)
            : base(list)
        {
            Initialize(edmType);
        }

        /// <inheritdoc/>
        public IEdmTypeReference GetEdmType()
        {
            return _edmType;
        }

        private void Initialize(IEdmCollectionTypeReference edmType)
        {
            if (edmType == null)
            {
                throw Error.ArgumentNull("edmType");
            }
            if (!edmType.ElementType().IsComplex())
            {
                throw Error.Argument("edmType",
                    SRResources.UnexpectedElementType, edmType.ElementType().ToTraceString(), edmType.ToTraceString(), typeof(IEdmComplexType).Name);
            }

            _edmType = edmType;
        }
    }
}
