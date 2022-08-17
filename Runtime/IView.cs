﻿﻿namespace ModelView
{
    public interface IView
    {
        bool CanRenderModel(object model);
        void Initialize(object model);
        void UpdateView(object model);
    }
}