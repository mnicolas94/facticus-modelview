﻿﻿using UnityEngine;

 namespace ModelView
{
    public abstract class ViewBaseBehaviour<TM> : MonoBehaviour, IView
    {
        private TM _model;
        
        public abstract bool CanRenderModel(TM model);
        public abstract void Initialize(TM model);
        public abstract void UpdateView(TM model);
        
        public bool CanRenderModel(object model)
        {
            if (model is TM m)
            {
                return CanRenderModel(m);
            }

            return false;
        }

        public void Initialize(object model)
        {
            if (model is TM m)
            {
                _model = m;
                Initialize(m);
            }
        }

        public void UpdateView(object model)
        {
            if (model is TM m)
            {
                _model = m;
                UpdateView(m);
            }
        }
    }
}