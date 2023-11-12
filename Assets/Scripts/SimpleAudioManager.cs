using System;
using Data;
using UnityEngine;

public class SimpleAudioManager : MonoBehaviour
{
    [SerializeField] private SfxEventPublisher onThrowBallPublisher;
    [SerializeField] private SfxEventPublisher onMergeBallPublisher;
    [SerializeField] private AudioSource sfxSource;

    private void OnEnable()
    {
        onThrowBallPublisher.OnSfxEventOccured += PlayOnShotClip;
        onMergeBallPublisher.OnSfxEventOccured += PlayOnShotClip;
    }

    private void OnDisable()
    {
        onThrowBallPublisher.OnSfxEventOccured -= PlayOnShotClip;
        onMergeBallPublisher.OnSfxEventOccured -= PlayOnShotClip;
    }

    private void PlayOnShotClip(AudioClip clip) => sfxSource.PlayOneShot(clip);
}