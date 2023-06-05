﻿using System;
 using System.Collections.Generic;
 using TNRD;
 using UnityEngine;

 namespace ModelView
{
    public class ViewProviderGameObject : MonoBehaviour, IViewProvider
    {
        [SerializeField] private List<SerializableInterface<IView>> _views;
        
        public bool TryGetViewForModel(object model, out IView result, Func<IView, bool> additionalRestrictions = null)
        {
            var views = _views.ConvertAll(view => view.Value);
            return IViewProviderUtils.TryGetViewForModel(views, model, out result, additionalRestrictions);
        }
    }
}