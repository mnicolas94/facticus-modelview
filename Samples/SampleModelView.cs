using TMPro;
using UnityEngine;

namespace ModelView.Samples
{
    public class SampleModelView : ViewBaseBehaviour<SampleModel>
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public override bool CanRenderModel(SampleModel model)
        {
            return true;
        }

        public override void Initialize(SampleModel model)
        {
            UpdateView(model);
        }

        public override void UpdateView(SampleModel model)
        {
            _text.text = model.Name;
        }
    }
}