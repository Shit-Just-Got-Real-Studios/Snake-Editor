using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour {
	private SnakeController next;

	public void SetNext (SnakeController IN) {
		next = IN;
	}

	public SnakeController GetNext () {
		return next;
	}

	public void RemoveTail () {
		Destroy (this.gameObject);
	}


}
