using UnityEngine;
using System.Collections;
using SocketIO;
using System;


public class swing : MonoBehaviour {
	public float torque = 5;
	public Rigidbody rb;

	public float ogAx = 0;
	public float ogAy = 0;
	public float ogAz = 0;

	public float currentAx = 0;
	public float currentAy = 0;
	public float currentAz = 0;
	
	private SocketIOComponent socket;
	public Vector3 eulerAngleVelocity;
 

	void Start () {
		rb = GetComponent<Rigidbody> ();

		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();
		
		Debug.Log ("Does this even run?");
		
		socket.On("open", TestOpen);
		socket.On("error", TestError);
		socket.On("close", TestClose);
		socket.On("acceleration", getAcceleration);
	}


	public void TestOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}
	
	public void getAcceleration(SocketIOEvent e){

		//Debug.Log("Acceleration X: " +  e.data["aX"].ToString());
		//Debug.Log("Acceleration Y: " +  e.data["aY"].ToString());
		//Debug.Log("Acceleration Z: " +  e.data["aZ"].ToString());

		currentAx = float.Parse((e.data["aX"]).ToString());
		currentAy = float.Parse((e.data["aY"]).ToString());;
		//Probably most important value
		currentAz = float.Parse((e.data["aZ"]).ToString());

		if (currentAx > 0.5 || currentAx < -0.5) {
			ogAx = currentAx;
		} 
		else {
			ogAx = 0;
		}
		if (currentAy > 0.5 || currentAy < -0.5) {
			ogAy = currentAy;
		} 
		else {
			ogAy = 0;
		}
		if (currentAz > 0.5 || currentAz < -0.5) {
			ogAz = currentAz;
		} else {
			ogAz = 0;
		}
		
	}

	public void TestError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}
	
	public void TestClose(SocketIOEvent e)
	{	
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}


	void FixedUpdate() {
		//float turn = Input.GetAxis("Vertical");

		//Timotius's key input
		//rb.AddTorque(new Vector3(0.0f, 0.0f, turn * 500));


//		Debug.Log (ogAx + " " + ogAy + " "+ ogAz);
//		float turn = currentAy;
//		if (turn > 1 || turn < -1) {
//			rb.AddTorque(new Vector3(0.0f, 0.0f, turn * 500));
//		}
		float x = (float)(180 / Math.PI) * ogAx;
		float y = (float)(180 / Math.PI) * ogAy;
		float z = (float)(180 / Math.PI) * ogAz;
//		Debug.Log (x + " " + y + " "+ z);
//		Quaternion angle = Quaternion.Euler (x, 0.0f, 0.0f);
//		rb.MoveRotation (angle);
		Quaternion deltaRotation = Quaternion.Euler(new Vector3(y, 0.0f, 0.0f) * Time.deltaTime);
		rb.MoveRotation(rb.rotation * deltaRotation);




	}
}
