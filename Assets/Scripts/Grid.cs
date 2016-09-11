using UnityEngine;
using System.Collections;
public class Grid : MonoBehaviour {
	
	public float gridWidth = 32.0f;

	public float gridHeight = 32.0f;

	public Color gridColor = Color.white;

	public Transform tilePrefab;

	public TileSet tileSet;
	void OnDrawGizmos () {
		Vector3 cameraPos = Camera.current.transform.position;
		Gizmos.color = gridColor;

		for (float y = cameraPos.y - 800.0f; y < cameraPos.y + 800.0f; y += gridHeight) {
			Gizmos.DrawLine (new Vector3(-1000000.0f, Mathf.Floor(y / gridHeight) * gridHeight, 0.0f),new Vector3(1000000.0f, Mathf.Floor(y / gridHeight) * gridHeight, 0.0f));
		}

		for (float y = cameraPos.x - 1200.0f; y < cameraPos.x + 1200.0f; y += gridWidth) {
			Gizmos.DrawLine (new Vector3(Mathf.Floor(y / gridWidth) * gridWidth, -1000000.0f, 0.0f),new Vector3(Mathf.Floor(y / gridWidth) * gridWidth, 1000000.0f, 0.0f));
		}
	}
}
