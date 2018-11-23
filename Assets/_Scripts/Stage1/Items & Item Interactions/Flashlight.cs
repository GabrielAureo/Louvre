using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flashlight : Item
{

    //Light source;

    //float intensity;
    //float range;

    //[SerializeField] float switchDuration = .5f;

    private TurnLightOnOff lightOnOffScript;

	void Awake(){
		//source = GetComponentInChildren<Light>();
        lightOnOffScript = GetComponent<TurnLightOnOff>();
        //intensity = source.intensity;
        //range = source.range;
	}

    // Update is called once per frame
    //void Update () {
    //		if(Input.GetButtonDown("Fire1")){
    //source.DOIntensity(source.intensity == 0 ? intensity: 0 ,switchDuration).Play();

    //	}

    //}

    /*
        public void TurnOnOff()
        {
            DOTween.To(() => source.range, x => source.range = x, source.range == 0 ? range : 0, switchDuration);
        }
    */

    public void TurnOnOff()
    {
        lightOnOffScript.TurnOnOff();
    }
    
    public override void OnInteraction()
    {
        base.OnInteraction();

        FlashlightLightController light = GetComponentInChildren<FlashlightLightController>();
        if (light)
        {
            light.enabled = true;
        }
    }

    public override void OnDropped()
    {
        FlashlightLightController light = GetComponentInChildren<FlashlightLightController>();
        if (light)
        {
            light.enabled = false;
        }
    }
}
