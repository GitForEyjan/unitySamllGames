using UnityEngine;
using System.Collections;

public class CreateCellCReateCell : MonoBehaviour {
	public GameObject cell;
	public GameObject player;

	public bool isMouseMove = false;
	// Use this for initialization
	void Start () {
		int i = 0; 
		int j = 0;
		int sum = 0;
		GameObject ren = GameObject.Instantiate (player)as GameObject;
		ren.transform.parent = this.transform;
		ren.transform .position = new Vector2 (0,0);

		for (i = 0; i < 3; i++) {
			for (j = 0; j < 3; j++) {
				GameObject gezi = GameObject.Instantiate (cell)as GameObject;
				gezi.transform.parent = this.transform;
				gezi.transform.position = new Vector2 (0.67f * i, 0.67f * j);
			}
		}

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			isMouseMove = true;
			//Debug.Log(Input.mousePosition.x);
		}

		if (Input.GetMouseButtonUp(0)) {
			isMouseMove = false;
		}


		if (isMouseMove) {
			Debug.Log(Input.mousePosition.x);
		}
		//if (Input.GetKeyDown(KeyCode.Mouse0)) {
			
		//}
	}
}