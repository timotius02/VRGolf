using UnityEngine;
using System.Collections;
using System.Threading;

public class addGravity : MonoBehaviour {
	public Rigidbody rb;
	public bool doCamera = false;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
	}
	
	void OnCollisionEnter (Collision col)
	{

		if(col.gameObject.name == "club")
		{
			Debug.Log(col.impulse.x + " " + col.impulse.y +" "+ col.impulse.z);
			rb.AddForce(50 , 10, 0);
			rb.useGravity = true;
			doCamera = true;


			StartCoroutine(Example());
		}
	}

	IEnumerator Example() {
		yield return new WaitForSeconds(4);
		int DistanceAway = 0;
		Vector3 BallPOS = GameObject.Find ("ball").transform.transform.position;
		GameObject.Find ("Main Camera").transform.position = new Vector3 (BallPOS.x - DistanceAway, BallPOS.y + 1, BallPOS.z);
	}

	void Update(){

		//Debug.Log ("Does this run?");

		if (doCamera) {

		}
	}

}
