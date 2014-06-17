using UnityEngine;
using System.Collections;

public class TargetComponent : MonoBehaviour
{
	public AudioClip GoalSound;
	
	private void Start()
	{
		light = this.gameObject.GetComponentInChildren<Light>();
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		GameObject.Destroy(collision.gameObject);
		ScoreComponent comp = collision.gameObject.GetComponent<ScoreComponent>();
		GameManager.Instance.Score += (int)(comp.Points);
		
		this.audio.PlayOneShot(GoalSound);
		DoFlash();
	}
	
	private void DoFlash()
	{
		FlashCount = 0;
		StartCoroutine(Flash());
	}
	
	private Light light;
	private int FlashCount = 0;
	
	private IEnumerator Flash()
    {
		light.enabled = false;
		
        yield return new WaitForSeconds(0.1f);

		light.enabled = true;
		
		yield return new WaitForSeconds(0.1f);
		
		FlashCount++;
		if (FlashCount < 5)
		{
			StartCoroutine(Flash());
		}
    }

}
