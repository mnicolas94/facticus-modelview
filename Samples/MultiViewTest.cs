using UnityEngine;

namespace ModelView.Samples
{
    public class MultiViewTest : MonoBehaviour
    {
        [SerializeField] private GenericView _view;
        [SerializeField] private ScriptableObject _model;

        [ContextMenu("Populate")]
        public void PopulateModel()
        {
            _view.Initialize(_model);
        }
    }
}