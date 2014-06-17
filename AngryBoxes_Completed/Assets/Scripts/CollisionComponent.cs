using UnityEngine;
using System.Collections;

public class CollisionComponent : MonoBehaviour {

	public AudioClip collisionSound;
	
	private void OnCollisionEnter(Collision theCollision)
	{
		//Debug.Log("I collided");
		if (theCollision.impactForceSum.magnitude > 10)
		{
			audio.PlayOneShot(collisionSound);
		}
	}
}
