﻿using System;
 using System.Collections.Generic;
 using System.Linq;
 using TNRD;
 using UnityEngine;
 using Object = UnityEngine.Object;

 namespace ModelView
{
    public class ViewList : MonoBehaviour
    {
        [SerializeField] private SerializableInterface<IViewProvider> _viewProvider;

        [SerializeField] private Transform _viewsContainer;

        private Dictionary<object, IView> _modelViewDictionary = new Dictionary<object, IView>();

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
            var existsView = _viewProvider.Value.TryGetViewForModel(model, out var view);
            if (existsView)
            {
                _modelViewDictionary[model] = view;
                if (view is Component componentView)
                {
                    componentView.transform.SetParent(_viewsContainer, worldPositionStays: false);
                }

                return view;
            }

            return null;
        }

        public void Remove(object model)
        {
            if (_modelViewDictionary.TryGetValue(model, out var view))
            {
                if (view is Object viewObject)
                {
                    Destroy(viewObject);
                }

                _modelViewDictionary.Remove(model);
            }
        }

        public void Clear()
        {
            var models = _modelViewDictionary.Keys.ToString();
            foreach (var model in models)
            {
                Remove(model);
            }
            
            _modelViewDictionary.Clear();
        }
        
        public void PopulateModels(IList<object> models)
        {
            Clear();
            foreach (var model in models)
            {
                Add(model);
            }
        }
    }
}