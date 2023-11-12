using System;
using UnityEngine;

namespace Data.Variables
{
    [CreateAssetMenu(fileName = "ObservableFloatValue", menuName = "Data/Variables/ObservableFloatValue", order = 1)]
    public class ObservableFloatValue : ScriptableObject
    {
        [SerializeField] private float value;
        public event Action<float> OnValueChanged;
        
        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                OnValueChanged?.Invoke(value);
            }
        }
    }
}