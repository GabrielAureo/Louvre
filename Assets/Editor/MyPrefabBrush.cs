using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MyPrefab brush", menuName = "MyPrefab brush")]
[CustomGridBrush(false, true, false, "MyPrefab Brush")]
public class MyPrefabBrush : GridBrushBase
{
    public List<GameObject> m_prefabs;
    public GameObject activePrefab;
	public int m_Z;
	static Bounds testBound;

    public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
	{
		// Do not allow editing palettes
		if (brushTarget.layer == 31)
			return;
        
        GameObject instance = (GameObject) PrefabUtility.InstantiatePrefab(activePrefab);
		if (instance != null)
		{
			Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint Prefabs");
			Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Prefabs");
			instance.transform.SetParent(brushTarget.transform);
			instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x, position.y, m_Z) + Vector3.right));
		}
    }

	public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
		{
			// Do not allow editing palettes
			if (brushTarget.layer == 31)
				return;

			Transform erased = GetObjectInCell(grid, brushTarget.transform, new Vector3Int(position.x, position.y, m_Z));
			if (erased != null)
				Undo.DestroyObjectImmediate(erased.gameObject);
		}
		
 	private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position)
		{
			Debug.Log("bounds: "+ grid.GetBoundsLocal(position));
			int childCount = parent.childCount;
			Vector3 min = grid.CellToWorld((position));
			Debug.Log("min = " + min.x + ", " + min.z);
			Vector3 max = grid.CellToWorld((position + Vector3Int.one));
			Debug.Log("max = " + max.x + ", " + max.z);
			Bounds bounds = new Bounds((max + min)*.5f, max - min);
			Debug.Log(bounds.center+ ", " + bounds.size);
			for (int i = 0; i < childCount; i++)
			{
				Transform child = parent.GetChild(i);
				if (bounds.Contains(child.position))
					return child;
			}
			return null;
		}

		public void InsertBlock(GameObject block){
			m_prefabs.Add(block);
		}
}

	[CustomEditor(typeof(MyPrefabBrush))]
	public class MyPrefabBrushEditor : GridBrushEditorBase
	{
		public Texture2D selectionBox;
		private MyPrefabBrush prefabBrush { get { return target as MyPrefabBrush; } }

		private SerializedProperty m_Prefabs;
		private SerializedProperty m_actPrefab;
		private SerializedObject m_SerializedObject;
		private GameObject prefabInsert;
		private Vector2 scrollPos;
		private const float ButtonWidth = 80;
		private const float ButtonHeight = 80;
		private int selGridIndex;

		protected void OnEnable()
		{
			m_SerializedObject = new SerializedObject(target);
			m_Prefabs = m_SerializedObject.FindProperty("m_prefabs");
			m_actPrefab = m_SerializedObject.FindProperty("activePrefab");
			selGridIndex = -1;

		}

		public override void OnPaintInspectorGUI(){
			m_SerializedObject.UpdateIfRequiredOrScript();
			EditorGUILayout.BeginScrollView(scrollPos);

			selGridIndex = GUILayout.SelectionGrid(
				selGridIndex,
				GetGUIContentFromItems(),
				5,
				GetGUIStyle()
			);
			m_actPrefab.objectReferenceValue = GetSelectedItem(selGridIndex);
			GUILayout.EndScrollView();

			prefabInsert = (GameObject)EditorGUILayout.ObjectField(prefabInsert, typeof(GameObject), false);
			if(prefabInsert) prefabBrush.InsertBlock(prefabInsert);
			prefabInsert = null;
			m_SerializedObject.ApplyModifiedPropertiesWithoutUndo();

		}

		private GUIContent[] GetGUIContentFromItems(){
			List<GUIContent> guiContents = new List<GUIContent>();
			for(int i = 0; i < m_Prefabs.arraySize; i++){
				Debug.Log(m_Prefabs.GetArrayElementAtIndex(i).objectReferenceValue);
				GameObject go = (GameObject) m_Prefabs.GetArrayElementAtIndex(i).objectReferenceValue;
				Texture2D tex = AssetPreview.GetAssetPreview(go);
				string name = go.name;
				var content = new GUIContent(name,tex);
				guiContents.Add(content);
			}
			return guiContents.ToArray();
		}

		private GUIStyle GetGUIStyle(){
			GUIStyle style = new GUIStyle(GUI.skin.box);
			style.alignment = TextAnchor.LowerCenter;
			style.imagePosition = ImagePosition.ImageAbove;
			style.fixedHeight = ButtonHeight;
			style.fixedWidth = ButtonWidth;
			return style;
		}

		private GameObject GetSelectedItem(int index){
			if(index != -1){
				return m_Prefabs.GetArrayElementAtIndex(index).objectReferenceValue as GameObject;
			}
			return null;
		}
	}