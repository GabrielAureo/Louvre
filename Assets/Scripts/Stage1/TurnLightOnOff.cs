using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurnLightOnOff : MonoBehaviour
{
    Light source;
    float range;
    [SerializeField] float switchDuration = .5f;

    void Awake()
    {
        source = GetComponentInChildren<Light>();
        range = source.range;
    }

    public void TurnOnOff()
    {
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
