using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    [CreateAssetMenu(fileName = "SimpleEventPublisher", menuName = "Data/Event/SimpleEventPublisher", order = 0)]
    public class SimpleEventPublisher : ScriptableObject
    {
        public UnityAction OnEventOccured;

        public void PublishMessage()
        {
            OnEventOccured?.Invoke();
        }
    }
}