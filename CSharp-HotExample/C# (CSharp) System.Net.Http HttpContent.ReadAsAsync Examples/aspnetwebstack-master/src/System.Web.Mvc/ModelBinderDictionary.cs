// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
    public class ModelBinderDictionary : IDictionary<Type, IModelBinder>
    {
        private readonly Dictionary<Type, IModelBinder> _innerDictionary = new Dictionary<Type, IModelBinder>();
        private IModelBinder _defaultBinder;
        private ModelBinderProviderCollection _modelBinderProviders;

        public ModelBinderDictionary()
            : this(ModelBinderProviders.BinderProviders)
        {
        }

        internal ModelBinderDictionary(ModelBinderProviderCollection modelBinderProviders)
        {
            _modelBinderProviders = modelBinderProviders;
        }

        public int Count
        {
            get { return _innerDictionary.Count; }
        }

        public IModelBinder DefaultBinder
        {
            get
            {
                if (_defaultBinder == null)
                {
                    _defaultBinder = new DefaultModelBinder();
                }
                return _defaultBinder;
            }
            set { _defaultBinder = value; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<Type, IModelBinder>)_innerDictionary).IsReadOnly; }
        }

        public ICollection<Type> Keys
        {
            get { return _innerDictionary.Keys; }
        }

        public ICollection<IModelBinder> Values
        {
            get { return _innerDictionary.Values; }
        }

        public IModelBinder this[Type key]
        {
            get
            {
                IModelBinder binder;
                _innerDictionary.TryGetValue(key, out binder);
                return binder;
            }
            set { _innerDictionary[key] = value; }
        }

        public void Add(KeyValuePair<Type, IModelBinder> item)
        {
            ((IDictionary<Type, IModelBinder>)_innerDictionary).Add(item);
        }

        public void Add(Type key, IModelBinder value)
        {
            _innerDictionary.Add(key, value);
        }

        public void Clear()
        {
            _innerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<Type, IModelBinder> item)
        {
            return ((IDictionary<Type, IModelBinder>)_innerDictionary).Contains(item);
        }

        public bool ContainsKey(Type key)
        {
            return _innerDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Type, IModelBinder>[] array, int arrayIndex)
        {
            ((IDictionary<Type, IModelBinder>)_innerDictionary).CopyTo(array, arrayIndex);
        }

        public IModelBinder GetBinder(Type modelType)
        {
            return GetBinder(modelType, true /* fallbackToDefault */);
        }

        public virtual IModelBinder GetBinder(Type modelType, bool fallbackToDefault)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            return GetBinder(modelType, (fallbackToDefault) ? DefaultBinder : null);
        }

        private IModelBinder GetBinder(Type modelType, IModelBinder fallbackBinder)
        {
            // Try to look up a binder for this type. We use this order of precedence:
            // 1. Binder returned from provider
            // 2. Binder registered in the global table
            // 3. Binder attribute defined on the type
            // 4. Supplied fallback binder

            IModelBinder binder = _modelBinderProviders.GetBinder(modelType);
            if (binder != null)
            {
                return binder;
            }

            if (_innerDictionary.TryGetValue(modelType, out binder))
            {
                return binder;
            }

            // Function is called frequently, so ensure the error delegate is stateless
            binder = ModelBinders.GetBinderFromAttributes(modelType, (Type errorModel) =>
                {
                    throw new InvalidOperationException(
                        String.Format(CultureInfo.CurrentCulture, MvcResources.ModelBinderDictionary_MultipleAttributes, errorModel.FullName));
                });

            return binder ?? fallbackBinder;
        }

        public IEnumerator<KeyValuePair<Type, IModelBinder>> GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }

        public bool Remove(KeyValuePair<Type, IModelBinder> item)
        {
            return ((IDictionary<Type, IModelBinder>)_innerDictionary).Remove(item);
        }

        public bool Remove(Type key)
        {
            return _innerDictionary.Remove(key);
        }

        public bool TryGetValue(Type key, out IModelBinder value)
        {
            return _innerDictionary.TryGetValue(key, out value);
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_innerDictionary).GetEnumerator();
        }

        #endregion
    }
}
