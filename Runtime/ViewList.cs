﻿using System;
 using System.Collections.Generic;
 using System.Linq;
 using TNRD;
 using UnityEngine;
 using UnityEngine.Events;
 using UnityEngine.Pool;
 using Object = UnityEngine.Object;

 namespace ModelView
{
    public class ViewList : MonoBehaviour
    {
        [SerializeField] private SerializableInterface<IViewProvider> _viewProvider;
        [SerializeField] private Transform _viewsContainer;
        
        [SerializeField] private UnityEvent<object> _added;
        public UnityEvent<object> Added => _added;

        [SerializeField] private UnityEvent<object> _removed;
        public UnityEvent<object> Removed => _removed;

        private Dictionary<object, IView> _modelViewDictionary = new Dictionary<object, IView>();
        private Dictionary<Object, ObjectPool<Object>> _viewPrefabsPoolsDictionary = new Dictionary<Object, ObjectPool<Object>>();
        private Dictionary<Object, ObjectPool<Object>> _viewsPoolsDictionary = new Dictionary<Object, ObjectPool<Object>>();

        public int Count => _modelViewDictionary.Count;

        private ObjectPool<Object> GetPoolForViewPrefab(Object viewPrefab)
        {
            if (!_viewPrefabsPoolsDictionary.ContainsKey(viewPrefab))
            {
                var viewsPool = new ObjectPool<Object>(
                    () => Instantiate(viewPrefab, _viewsContainer, worldPositionStays: false),
                    view => ((Component)view).gameObject.SetActive(true),
                    view => ((Component)view).gameObject.SetActive(false)
                );
                
                _viewPrefabsPoolsDictionary.Add(viewPrefab, viewsPool);

                return viewsPool;
            }

            return _viewPrefabsPoolsDictionary[viewPrefab];
        }

        private IView InstantiateView(IView viewPrefab)
        {
            IView view;
            if (viewPrefab is Object objectViewPrefab)
            {
                var pool = GetPoolForViewPrefab(objectViewPrefab);
                var objectView = pool.Get();

                if (!_viewsPoolsDictionary.ContainsKey(objectView))  // keep a record of each view's owner pool
                {
                    _viewsPoolsDictionary.Add(objectView, pool);
                }
                
                view = objectView as IView;
            }
            else
            {
                view = viewPrefab;
            }

            return view;
        }

        private void ReleaseView(IView view)
        {
            if (view is Object objectView)
            {
                if (_viewsPoolsDictionary.ContainsKey(objectView))
                {
                    var pool = _viewsPoolsDictionary[objectView];
                    pool.Release(objectView);
                }
            }
        }

        public bool IsModelInList(object model)
        {
            return _modelViewDictionary.ContainsKey(model);
        }

        public bool GetViewFromModel<T>(object model, out T view)
        {
            if (_modelViewDictionary.TryGetValue(model, out var iview))
            {
                if (iview is T casted)
                {
                    view = casted;
                    return true;
                }
            }

            view = default;
            return false;
        }

        public List<T> GetViews<T>()
        {
            return _modelViewDictionary.Values.Cast<T>().ToList();
        }
        
        public IView Add(object model)
        {
            var existsView = _viewProvider.Value.TryGetViewForModel(model, out var viewPrefab);
            if (existsView)
            {
                var view = InstantiateView(viewPrefab);
                view.Initialize(model);
                _modelViewDictionary[model] = view;
                _added.Invoke(model);

                return view;
            }

            return null;
        }

        public void Remove(object model)
        {
            if (_modelViewDictionary.TryGetValue(model, out var view))
            {
                ReleaseView(view);
                _modelViewDictionary.Remove(model);
                _removed.Invoke(model);
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            var models = new List<object>(_modelViewDictionary.Keys);
            foreach (var model in models)
            {
                Remove(model);
            }
            
            _modelViewDictionary.Clear();
        }
        
        public void PopulateModels(IList<object> models)
        {
            Clear();
            PopulateModels(models);
        }
        
        public void PopulateModels<T>(IList<T> models)
        {
            Clear();
            for (int i = 0; i < models.Count; i++)
            {
                var model = models[i];
                var view = Add(model);
                if (view is Component component)
                {
                    component.transform.SetSiblingIndex(i);
                }
            }
        }
    }
}