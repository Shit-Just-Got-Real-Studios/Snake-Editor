using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	public float xCoord;
	public float yCoord;
	private Color tempColor = Color.white;
	// Use this for initialization
	void Start () {
		xCoord = transform.position.x;
		yCoord = transform.position.y;
		//Fade(0.0f, 1.0f, 0.0f);     // fade up
		//Fade(1.0f, 0.0f, 2.0f);     // fade down
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangePos () {

		xCoord += Random.Range (-10, 10) * 1.28f;
		yCoord += Random.Range (-10, 10) * 1.28f;
		transform.position = new Vector2 (xCoord, yCoord);
	}
	void Fade (float startLevel,float endLevel , float duration) {
		float speed = 1.0f/duration;   
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime*speed) {
			tempColor.r = Mathf.Lerp(startLevel, endLevel, t);
			tempColor.b = Mathf.Lerp(startLevel, endLevel, t);
			tempColor.g = Mathf.Lerp(startLevel, endLevel, t);
		}
		this.GetComponent<SpriteRenderer> ().color = tempColor;
	}
}
