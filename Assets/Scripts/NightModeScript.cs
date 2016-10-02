using UnityEngine;
using System.Collections;

public class NightModeScript : MonoBehaviour {

	[SerializeField]
	private GameObject playerPrefab;

	[SerializeField]
	private GameObject AIPrefab;

	[SerializeField]
	private Camera mainCam;

	[SerializeField]
	private Material nightModeMat;

	[SerializeField]
	private Material origSnakeMat;

	[SerializeField]
	private Material origAIMat;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject gameController = GameObject.Find ("GameController");
		GameController gc = gameController.GetComponent<GameController> ();

		if (gc.score >= 10) {
			playerPrefab.GetComponent<Renderer> ().sharedMaterial = nightModeMat;
			AIPrefab.GetComponent<Renderer> ().sharedMaterial = nightModeMat;
			mainCam.backgroundColor = new Color (0.0f, 0.0f, 0.0f);
		} else {
			playerPrefab.GetComponent<Renderer> ().sharedMaterial = origSnakeMat;
			AIPrefab.GetComponent<Renderer> ().sharedMaterial = origAIMat;
		}
	}
		
}
