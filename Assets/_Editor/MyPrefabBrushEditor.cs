﻿# if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Linq;

[CustomEditor(typeof(MyPrefabBrush))]
public class MyPrefabBrushEditor : GridBrushEditorBase
{
	[SerializeField] private Texture2D selectionBox;
	private MyPrefabBrush prefabBrush { get { return target as MyPrefabBrush; } }

	private SerializedProperty m_Prefabs;
	private SerializedProperty m_actPrefab;
	private SerializedObject m_SerializedObject;
	private PrefabBlock prefabInsert;
	private Vector2 scrollPos;
	private const float ButtonWidth = 80;
	private const float ButtonHeight = 80;
	private int selGridIndex;
	private bool toggleInsert;

	protected void OnEnable()
	{
		m_SerializedObject = new SerializedObject(target);
		m_Prefabs = m_SerializedObject.FindProperty("m_prefabs");
		m_actPrefab = m_SerializedObject.FindProperty("activePrefab");
		selGridIndex = -1;
		prefabInsert = PrefabBlock.Empty;

	}

	public override void OnPaintInspectorGUI(){
		m_SerializedObject.UpdateIfRequiredOrScript();
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		
		var style = GetGUIStyle();
		Vector2 itemsMargin = new Vector2(style.margin.horizontal/2, style.margin.vertical/2);
		int rowLength = Mathf.Max(Mathf.FloorToInt(Screen.width/ButtonWidth), 1);
		selGridIndex = GUILayout.SelectionGrid(
			selGridIndex,
			GetGUIContentFromItems(),
			rowLength,
			GetGUIStyle()
		);
		if(selGridIndex > -1){
			m_actPrefab.objectReferenceValue = GetSelectedItem(selGridIndex);
			if(Event.current.clickCount == 2) EditorGUIUtility.PingObject(m_actPrefab.objectReferenceValue);
			var gridRect = GUILayoutUtility.GetLastRect();

			Rect selRect = new Rect(
				gridRect.xMin + ((ButtonWidth + itemsMargin.x) * (selGridIndex % rowLength)),
				gridRect.yMin + ((ButtonHeight + itemsMargin.y) * (selGridIndex / rowLength)),
				ButtonWidth, 
				ButtonHeight);


			GUI.DrawTexture(selRect,selectionBox);
			if(DrawDeleteButton(selRect, 20)){
				prefabBrush.RemoveBlockAt(selGridIndex);
				selGridIndex = -1;
			}

		}
				
		GUILayout.EndScrollView();

		if(toggleInsert = EditorGUILayout.Foldout(toggleInsert,"Adicionar Bloco", true, EditorStyles.foldout)){
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.BeginVertical();
				EditorGUIUtility.labelWidth = 50;
				prefabInsert.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefabInsert.prefab, typeof(GameObject), false);
				prefabInsert.name = (string)EditorGUILayout.TextField("Name", prefabInsert.name);
				prefabInsert.display = (Texture2D)EditorGUILayout.ObjectField("Display", prefabInsert.display, typeof(Texture2D), false);
				EditorGUIUtility.labelWidth = 0; //Reset labelWidth to default value
				EditorGUILayout.EndVertical();
			
			EditorGUI.BeginDisabledGroup(prefabInsert.prefab == null);
			var addBtn = GUILayout.Button("+",GUILayout.Width(20f),GUILayout.Height(20f));
			EditorGUI.EndDisabledGroup();
			if(addBtn){
				if(prefabInsert.name == "") prefabInsert.name = prefabInsert.prefab.name;
				if(prefabInsert.display == null) prefabInsert.display = AssetPreview.GetAssetPreview(prefabInsert.prefab);
				prefabBrush.InsertBlock(prefabInsert);
				prefabInsert = PrefabBlock.Empty;
			}
			EditorGUILayout.EndHorizontal();
		}
		m_SerializedObject.ApplyModifiedPropertiesWithoutUndo();

	}

	private bool DrawDeleteButton(Rect selRect, float size){
		var defaultBg = GUI.backgroundColor;
		var defaultTxt = GUI.contentColor;

		GUI.backgroundColor = Color.red;
		GUI.contentColor = Color.white;
		GUIStyle delStyle = EditorStyles.miniButton;
		delStyle.font = EditorStyles.miniFont;
		delStyle.clipping = TextClipping.Overflow;
		
		bool press = GUI.Button(new Rect(new Vector2(selRect.xMax - (size/2), selRect.yMin), size * Vector2.one), "x", delStyle) ;
		GUI.backgroundColor = defaultBg;
		GUI.contentColor = defaultTxt;
		return press;
	}

	private GUIContent[] GetGUIContentFromItems(){
		List<GUIContent> guiContents = new List<GUIContent>();
		for(int i = 0; i < m_Prefabs.arraySize; i++){
			//Debug.Log(m_Prefabs.GetArrayElementAtIndex(i).objectReferenceValue);
			SerializedProperty block = m_Prefabs.GetArrayElementAtIndex(i);
			GameObject go = (GameObject)block.FindPropertyRelative("prefab").objectReferenceValue;
			string name = block.FindPropertyRelative("name").stringValue;
			Texture2D display = block.FindPropertyRelative("display").objectReferenceValue as Texture2D;
			
			var content = new GUIContent(name,display);
			guiContents.Add(content);
		}
		return guiContents.ToArray();
	}

	private GUIStyle GetGUIStyle(){
		GUIStyle style = new GUIStyle(GUI.skin.textArea);
		style.alignment = TextAnchor.LowerCenter;
		style.imagePosition = ImagePosition.ImageAbove;
		style.fixedHeight = ButtonHeight;
		style.fixedWidth = ButtonWidth;
		
		return style;
	}

	private GameObject GetSelectedItem(int index){
		if(index != -1){
			return m_Prefabs.GetArrayElementAtIndex(index).FindPropertyRelative("prefab").objectReferenceValue as GameObject;
		}
		return null;
	}

	public override GameObject[] validTargets
		{
			get
			{
				return GameObject.FindObjectsOfType<Tilemap>().Select(x => x.gameObject).ToArray();
			}
		}
}
//iahala
#endif