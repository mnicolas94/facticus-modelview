using System;
using TNRD;
using UnityEngine;

namespace ModelView
{
    public class ViewListPopulator : MonoBehaviour
    {
        [SerializeField] private ViewList _viewList;
        [SerializeField] private SerializableInterface<IModelLoader> _modelsLoader;
        [SerializeField] private bool _populateOnStart;

        private void Start()
        {
            if (_populateOnStart)
            {
                Populate();
            }
        }

        public void Populate()
        {
            var models = _modelsLoader.Value.LoadModels();
            _viewList.PopulateModels(models);
        }
    }
}