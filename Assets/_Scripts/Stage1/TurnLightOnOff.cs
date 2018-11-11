using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurnLightOnOff : MonoBehaviour
{
    Light source;
    float range;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] float switchDuration = .5f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        source = GetComponentInChildren<Light>();
        range = source.range;
    }

    public void TurnOnOff()
    {
        audioSource.PlayOneShot(audioClip);
        DOTween.To(() => source.range, x => source.range = x, source.range == 0 ? range : 0, switchDuration);
    }

    public void TurnOff()
    {
        DOTween.To(() => source.range, x => source.range = x, 0, switchDuration);
    }

    public void TurnOn()
    {
        DOTween.To(() => source.range, x => source.range = x, range, switchDuration);
    }
}
