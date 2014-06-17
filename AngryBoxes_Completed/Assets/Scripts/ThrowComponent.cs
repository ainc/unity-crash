using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowComponent : MonoBehaviour {
	
	public GameObject Prefab;

	private void Update ()
	{
		if (Input.GetButtonUp("Fire1"))
		{
			if ((Prefab != null) && (GameManager.Instance.ThrowsCount > 0))
			{
				GameObject newobj = GameObject.Instantiate(Prefab) as GameObject;
				newobj.rigidbody.velocity = this.gameObject.transform.forward * 25;
				newobj.transform.position = this.gameObject.transform.position;
				
				GameManager.Instance.ThrowsCount--;
				
				Destroy(newobj, 3);
			}
		}
	}
}
