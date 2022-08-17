using System.Collections.Generic;

namespace ModelView
{
    public interface IModelLoader
    {
        IList<object> LoadModels();
    }
}