using System;
using TNRD;
using UnityEngine;

namespace ModelView
{
    public class ViewListPopulator : MonoBehaviour
    {
        [SerializeField] private ViewList _viewList;
        [SerializeField] private SerializableInterface<IModelLoader> _modelsLoader;

        private void Start()
        {
            var models = _modelsLoader.Value.LoadModels();
            _viewList.PopulateModels(models);
        }
    }
}