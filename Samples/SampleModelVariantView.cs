using TMPro;
using UnityEngine;

namespace ModelView.Samples
{
    public class SampleModelVariantView : ViewBaseBehaviour<SampleModel>
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public override bool CanRenderModel(SampleModel model)
        {
            return model is SampleModelVariant;
        }

        public override void Initialize(SampleModel model)
        {
            UpdateView(model);
        }

        public override void UpdateView(SampleModel model)
        {
            if (model is SampleModelVariant variant)
            {
                _text.text = $"{variant.Name} --- {variant.Number}";
            }
        }
    }
}