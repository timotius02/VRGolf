using UnityEngine;
using System.Collections;


public class clubInit : MonoBehaviour {
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		if(GameObject.FindWithTag("club")) {
			var club = GameObject.FindWithTag("club");//Assign the bone when found
			rb.transform.parent = club.gameObject.transform;//Make my head the parent of my hat
		}





	}





	// Update is called once per frame
	void Update () {
	
	}
}
