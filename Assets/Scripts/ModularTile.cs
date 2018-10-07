using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Louvre Tile")]
public class ModularTile : TileBase{
	public Sprite auxSprite;
	public GameObject prefab;

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData){
		tileData.gameObject = prefab;
		tileData.sprite = auxSprite;
	}

	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go){
		go.name = "Aaa";
		go.transform.rotation = prefab.transform.rotation;
		return true;
		
	}
}
