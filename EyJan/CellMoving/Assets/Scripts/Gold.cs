using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {

    private Animator goldAni;

	// Use this for initialization
	void Start () {
        goldAni = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player") {
            goldAni.Play("goldJum");
        }
    }

    void removeSelf() {
        GameObject.Destroy(this);
    }
}
