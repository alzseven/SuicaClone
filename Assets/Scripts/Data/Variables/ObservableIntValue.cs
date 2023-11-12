using System;
using UnityEngine;

namespace Data.Variables
{
    [CreateAssetMenu(fileName = "ObservableIntValue", menuName = "Data/Variables/ObservableIntValue", order = 0)]
    public class ObservableIntValue : ScriptableObject
    {
        [SerializeField] private int value;
        public event Action<int> OnValueChanged;
        
        public int Value
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