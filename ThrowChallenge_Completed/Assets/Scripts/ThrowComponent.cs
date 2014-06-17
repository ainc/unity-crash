using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowComponent : MonoBehaviour {
	
	public GameObject Prefab;

	private void Update ()
	{
		if (Input.GetButtonUp("Fire1"))
		{
			if ((Prefab != null))
			{
				GameObject newobj = GameObject.Instantiate(Prefab) as GameObject;
				//newobj.rigidbody.velocity = this.gameObject.transform.forward * 25;
				newobj.transform.position = this.gameObject.transform.position;
				newobj.rigidbody.AddForce(this.gameObject.transform.forward * 1000);
				
				Destroy(newobj, 3);
				
				GameObject obj = this.gameObject.transform.GetChild(0).gameObject;
				Destroy(obj);
				
				Prefab = null;
			}
		}		
	}
}
