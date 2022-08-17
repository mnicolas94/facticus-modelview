using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModelView.ModelLoaders
{
    public class ListModelLoader<T> : MonoBehaviour, IModelLoader
    {
        [SerializeField] private List<T> _models;
        
        public IList<object> LoadModels()
        {
            return _models.Cast<object>().ToList();
        }
    }
}