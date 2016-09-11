using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor {

	Grid grid;

	private int oldIndex = 0;
	void OnEnable () {
		grid = (Grid)target;
	}

	[MenuItem("Assets/Create/TileSet")]
	static void CreateTileSet () {
		var asset = ScriptableObject.CreateInstance <TileSet> ();
		var path = AssetDatabase.GetAssetPath (Selection.activeObject);

		if (string.IsNullOrEmpty (path)) {
			path = "Assets";
		} else if (Path.GetExtension (path) != "") {
			path = path.Replace (Path.GetFileName (path), "");
		} else {
			path += "/";
		}

		var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "TileSet.asset");
		AssetDatabase.CreateAsset (asset,assetPathAndName);
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
		asset.hideFlags = HideFlags.DontSave;
	}
	public override void OnInspectorGUI () {
		//base.OnInspectorGUI ();

		grid.gridWidth = createSlider ("Width", grid.gridWidth);
		grid.gridHeight = createSlider ("Height", grid.gridHeight);

		if (GUILayout.Button ("Open Grid Window")) {
			GridWindow window = (GridWindow) EditorWindow.GetWindow (typeof(GridWindow));
			window.init ();
		}

		//Tile Prefab
		EditorGUI.BeginChangeCheck ();
		var newTilePrefab = (Transform)EditorGUILayout.ObjectField ("Tile Prefab", grid.tilePrefab, typeof(Transform), false);

		if (EditorGUI.EndChangeCheck ()) {
			grid.tilePrefab = newTilePrefab;
			Undo.RecordObject (target, "Grid Changed");
		}

		//Tile map
		EditorGUI.BeginChangeCheck ();
		var newTileSet = (TileSet)EditorGUILayout.ObjectField ("Tile Set", grid.tileSet, typeof(TileSet), false);
		if (EditorGUI.EndChangeCheck ()) {
			grid.tileSet = newTileSet;
			Undo.RecordObject (target, "Grid Changed");
		}

		if (grid.tileSet != null) {
			EditorGUI.BeginChangeCheck ();
			var names = new string [grid.tileSet.prefabs.Length];
			var values = new int [names.Length];

			for (int i = 0; i < names.Length; i++) {
				names [i] = grid.tileSet.prefabs [i] != null ? grid.tileSet.prefabs [i].name : "";
				values [i] = i;
			}

			var index = EditorGUILayout.IntPopup ("Select Tile", oldIndex, names, values);
			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject (target, "Grid Changed");
				if (oldIndex != index) {
					oldIndex = index;
					grid.tilePrefab = grid.tileSet.prefabs [index];
					float width = grid.tilePrefab.GetComponent <Renderer> ().bounds.size.x;
					float height = grid.tilePrefab.GetComponent <Renderer> ().bounds.size.y;

					grid.gridWidth = width;
					grid.gridHeight = height;
				}
			}
		}
	} 

	private float createSlider (string labelName, float sliderPosition) {
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Grid " + labelName);
		sliderPosition = EditorGUILayout.Slider (sliderPosition, 1f, 100f, null);
		GUILayout.EndHorizontal ();

		return sliderPosition;
	}

	void OnSceneGUI () {
		int controlID = GUIUtility.GetControlID (FocusType.Passive);
		Event e = Event.current;
		Ray ray = Camera.current.ScreenPointToRay (new Vector3 (e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
		Vector3 mousePos = ray.origin;

		if (e.isMouse && e.type == EventType.MouseDown && e.button == 0) {
			GUIUtility.hotControl = controlID;
			e.Use ();

			GameObject gameObject;
			Transform prefab = grid.tilePrefab;

			if (prefab) {
				Undo.IncrementCurrentGroup ();
				gameObject = (GameObject)PrefabUtility.InstantiatePrefab (prefab.gameObject);
				Vector3 aligned = new Vector3 (Mathf.Floor (mousePos.x/grid.gridWidth) * grid.gridWidth + grid.gridWidth/2.0f, Mathf.Floor (mousePos.y/grid.gridHeight) * grid.gridHeight + grid.gridHeight/2.0f, 0.0f);
				gameObject.transform.position = aligned;
				gameObject.transform.parent = grid.transform;
				Undo.RegisterCreatedObjectUndo (gameObject, "Create " + gameObject.name);
			}
		}

		if (e.isMouse && e.type == EventType.MouseDown && e.button == 1) {
			GUIUtility.hotControl = controlID;
			e.Use ();

			Vector3 aligned = new Vector3 (Mathf.Floor (mousePos.x/grid.gridWidth) * grid.gridWidth + grid.gridWidth/2.0f, Mathf.Floor (mousePos.y/grid.gridHeight) * grid.gridHeight + grid.gridHeight/2.0f, 0.0f);
			Transform transform = getTransformFromPosition (aligned);
			if (transform != null) {
				DestroyImmediate (transform.gameObject);
			}
		}
		if (e.isMouse && e.type == EventType.MouseUp) {
			GUIUtility.hotControl = 0;

		}
	}

	Transform getTransformFromPosition (Vector3 aligned) {
		int i = 0;
		while (i < grid.transform.childCount) {
			if (grid.transform.GetChild (i).position == aligned) {
				return grid.transform.GetChild (i);
			}
			i++;
		}
		return null;
	}
}
