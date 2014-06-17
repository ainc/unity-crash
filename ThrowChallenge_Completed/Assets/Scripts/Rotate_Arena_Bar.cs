using UnityEngine;
using System.Collections;

public class Rotate_Arena_Bar : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		this.gameObject.rigidbody.angularVelocity = Vector3.up * .1f;
	}
	
	
}
