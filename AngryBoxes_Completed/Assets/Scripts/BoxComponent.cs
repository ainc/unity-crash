using UnityEngine;
using System.Collections;

public class BoxComponent : MonoBehaviour {
	
	public GameObject explosion;
	
	private void Start ()
    {
		GameManager.Instance.BoxCount++;	
	}
		
	public void OnCollisionEnter(Collision theCollision)
	{
		if (theCollision.gameObject.tag == "Ground")
		{
			GameObject.Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
			GameObject.Destroy(this.gameObject);
			GameManager.Instance.BoxCount--;
		}
	}
}
