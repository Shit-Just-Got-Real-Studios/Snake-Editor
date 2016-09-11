using UnityEngine;
using System.Collections;
using UnityEditor;

public class GridWindow : EditorWindow {
	Grid grid;

	public void init() {
		grid = (Grid)FindObjectOfType (typeof(Grid));
	}

	void OnGUI () {
		grid.gridColor = EditorGUILayout.ColorField (grid.gridColor, GUILayout.Width (200));
	}
}
