﻿using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace ModelView
{
    public class MultiViewDelegate : MonoBehaviour, IView
    {
        [SerializeField] private SerializableInterface<IViewProvider> _viewProvider;

        private IView _currentView;

        private Dictionary<Component, Component> _prefabsToViewDictionary = new();

        private Component GetViewFromPrefab(Component viewPrefab)
        {
            if (!_prefabsToViewDictionary.ContainsKey(viewPrefab))
            {
                var view = Instantiate(viewPrefab, transform, worldPositionStays: false);
                _prefabsToViewDictionary.Add(viewPrefab, view);
            }

            return _prefabsToViewDictionary[viewPrefab];
        }

        private void ReleaseView(Component view)
        {
            view.gameObject.SetActive(false);
        }

        public void ReleaseAllViews()
        {
            foreach (var view in _prefabsToViewDictionary.Values)
            {
                ReleaseView(view);
            }
        }
        
        public bool CanRenderModel(object model)
        {
            return _viewProvider.Value.TryGetViewForModel(model, out _);
        }

        public void Initialize(object model)
        {
            UpdateView(model);
        }

        public void UpdateView(object model)
        {
            ReleaseAllViews();
            _viewProvider.Value.TryGetViewForModel(model, out var viewPrefab);
            if (viewPrefab is Component objectPrefab)
            {
                var view = GetViewFromPrefab(objectPrefab);
                view.gameObject.SetActive(true);
                ((IView)view).Initialize(model);
            }
        }
    }
}