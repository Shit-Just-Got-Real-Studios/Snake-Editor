using UnityEngine;
using System.Collections;

public class AICollider : MonoBehaviour {

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "WorldBlocks") {
			Debug.Log ("Collided");
			AIController aic = GetComponentInParent<AIController> ();
			aic.callComChangeD ();
		}
	}
	void Update () {
		
	}
}
