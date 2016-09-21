using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int snakeLength;
	public int currentLength;
	public GameObject snakePrefab;
	public SnakeController Head;
	public SnakeController Tail;
	public int NESW;
	public Vector2 nextPos;
	public GameObject Eatable;
	public int score = 0;
	private bool nightMode = false;
	private int direction = 1;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Move", 0, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		ComChangeD ();
		Food food = Eatable.GetComponent <Food> ();
		if (Head.transform.position == Eatable.transform.position) {
			food.ChangePos ();
			snakeLength++;
			score++;
			if (score >= 3)
				nightMode = true;
		}
	}

	void Move () {
		Movement ();
		if (currentLength >= snakeLength)
			TailFunc ();
		else
			currentLength++;
	}
	void Movement () {
		GameObject temp;
		nextPos = Head.transform.position;
		if (nightMode)
			direction = -1;
		switch (NESW) {
		case 0:
			nextPos = new Vector2 (nextPos.x, nextPos.y + 1.0f * direction);
			break;

		case 1:
			nextPos = new Vector2 (nextPos.x + 1.0f * direction, nextPos.y);
			break;
		
		case 2:
			nextPos = new Vector2 (nextPos.x, nextPos.y - 1.0f * direction);
			break;

		case 3:
			nextPos = new Vector2 (nextPos.x - 1.0f * direction, nextPos.y);
			break;
		}

		temp = (GameObject)Instantiate (snakePrefab, nextPos, transform.rotation);
		Head.SetNext (temp.GetComponent <SnakeController> ());
		Head = temp.GetComponent <SnakeController> ();
	}

	void ComChangeD () {
		if (NESW != 2 && Input.GetKeyDown (KeyCode.W))
			NESW = 0;
		if (NESW != 3 && Input.GetKeyDown (KeyCode.D))
			NESW = 1;
		if (NESW != 0 && Input.GetKeyDown (KeyCode.S))
			NESW = 2;
		if (NESW != 1 && Input.GetKeyDown (KeyCode.A))
			NESW = 3;
		
	}

	void TailFunc () {
		SnakeController temp = Tail;
		Tail = Tail.GetNext ();
		temp.RemoveTail ();
	}
}
