using UnityEngine;

namespace ModelView.Samples
{
    [CreateAssetMenu(fileName = "SampleModelVariantA", menuName = "Samples/SampleModelVariantA", order = 0)]
    public class SampleModelVariant : SampleModel
    {
        [SerializeField] private int _number;

        public int Number => _number;
    }
}