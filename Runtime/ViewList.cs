﻿using System.Collections.Generic;
 using TNRD;
 using UnityEngine;

namespace ModelView
{
    public class ViewList : MonoBehaviour
    {
        [SerializeField] private SerializableInterface<IViewProvider> _viewProvider;

        [SerializeField] private Transform _viewsContainer;

        public void PopulateModels(IList<object> models)
        {
            foreach (var model in models)
            {
                var exists = _viewProvider.Value.TryGetViewForModel(model, out var view);
                if (exists)
                {
                    if (view is Component componentView)
                    {
                        componentView.transform.SetParent(_viewsContainer, worldPositionStays: false);
                    }
                }
            }
        }
    }
}