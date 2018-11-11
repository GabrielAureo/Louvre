#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MyPrefab brush", menuName = "MyPrefab brush")]
[CustomGridBrush(false, true, false, "MyPrefab Brush")]
public class MyPrefabBrush : GridBrushBase
{
    public List<PrefabBlock> m_prefabs;
    public GameObject activePrefab;
	public int m_Z;
	static Bounds testBound;

    public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
	{
		// Do not allow editing palettes
		if (brushTarget.layer == 31)
			return;
        if(brushTarget.GetComponent<Tilemap>() != null){
			GameObject instance = (GameObject) PrefabUtility.InstantiatePrefab(activePrefab);
			if (instance != null)
			{
				Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint Prefabs");
				Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Prefabs");
				instance.transform.SetParent(brushTarget.transform);
				instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x + 1, position.y, m_Z)));
			}
		}
    }

	public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
		{
			// Do not allow editing palettes
			if (brushTarget.layer == 31)
				return;
			
			if(brushTarget.GetComponent<Tilemap>() != null){
				Transform erased = GetObjectInCell(grid, brushTarget.transform, new Vector3Int(position.x, position.y, m_Z));
				if (erased != null)
					Undo.DestroyObjectImmediate(erased.gameObject);

			}
		}
		
 	private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position)
		{
			int childCount = parent.childCount;
			Vector3 tileLocalPos = grid.LocalToWorld(grid.CellToLocalInterpolated(position + Vector3Int.one));
			for (int i = 0; i < childCount; i++)
			{
				Transform child = parent.GetChild(i);
				if(Vector3.Distance(child.localPosition, tileLocalPos) < 0.1f){
					return child;
				}
			}
			return null;
		}

		public void InsertBlock(PrefabBlock block){
			m_prefabs.Add(block);
		}
		public void RemoveBlockAt(int index){
			m_prefabs.RemoveAt(index);
		}
}
#endif