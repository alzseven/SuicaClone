using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    [CreateAssetMenu(fileName = "SfxEventPublisher", menuName = "Data/Event/SfxEventPublisher", order = 1)]
    public class SfxEventPublisher : ScriptableObject
    {
        [SerializeField] private AudioClip sfxClip;
        public UnityAction<AudioClip> OnSfxEventOccured;
        
        public void PublishMessage()
        {
            OnSfxEventOccured?.Invoke(sfxClip);
        }
    }
}