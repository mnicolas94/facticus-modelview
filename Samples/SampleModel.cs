using UnityEngine;

namespace ModelView.Samples
{
    [CreateAssetMenu(fileName = "SampleModel", menuName = "Samples/SampleModel", order = 0)]
    public class SampleModel : ScriptableObject
    {
        [SerializeField] private string _name;

        public string Name => _name;
    }
}