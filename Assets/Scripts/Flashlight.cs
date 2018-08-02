using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flashlight : MonoBehaviour {

	Light source;

	float intensity;
	float range;

	[SerializeField] float switchDuration = .5f;

	void Awake(){
		source = GetComponentInChildren<Light>(); 
		intensity = source.intensity;
		range = source.range;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){
			//source.DOIntensity(source.intensity == 0 ? intensity: 0 ,switchDuration).Play();
			DOTween.To(()=>source.range, x=>source.range = x, source.range == 0 ? range: 0,switchDuration);
			
		}
					
	}
}
