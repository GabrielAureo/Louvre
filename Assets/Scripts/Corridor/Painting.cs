using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour {

	[SerializeField] Texture2D painting;
	MeshRenderer mesh;

	// Use this for initialization
	void Start () {
		mesh = GetComponentInChildren<MeshRenderer>();

		var ratio = new Vector3((float)painting.height/painting.width,(float)painting.width/painting.height, 1);

		
		//var targetSize = new Vector3(painting.width, painting.height, mesh.transform.localScale.z);
		transform.localScale = ratio;
		Debug.Log(mesh.bounds);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnValidate(){

	}
	
}
