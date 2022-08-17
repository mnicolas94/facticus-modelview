using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace ModelView
{
    public interface IViewProvider
    {
        bool TryGetViewForModel(object model, out IView result, Func<IView, bool> additionalRestrictions = null);
    }

    public static class IViewProviderUtils
    {
        public static bool TryGetViewForModel(List<IView> views, object model, out IView result, Func<IView, bool> additionalRestrictions = null)
        {
            foreach (var view in views)
            {
                bool meetsAdditionalRestrictions = additionalRestrictions?.Invoke(view) ?? true;
                if (view.CanRenderModel(model) && meetsAdditionalRestrictions)
                {
                    if (view is Object objectView)
                    {
                        result = Object.Instantiate(objectView) as IView;
                    }
                    else
                    {
                        result = view;
                    }
                    
                    result.Initialize(model);
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}