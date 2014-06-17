using UnityEngine;
using System.Collections;

public class BallSpawnerComponent : MonoBehaviour
{
    public GameObject Prefab;
    public int Throws;
	public float InitialMinDelay = 1;
	public float InitialMaxDelay = 2;	
	public float SpawnMinDelay = 5;
	public float SpawnMaxDelay = 10;
    private GameObject Ball;
	public AudioClip PickupSound;

	private void Start ()
    {
		float randomdelay = Random.Range(InitialMaxDelay, InitialMinDelay);
		StartCoroutine(SpawnBall(randomdelay));
	}
	
	private void Update ()
    {
	    if (Ball != null)
		{
			Ball.transform.position = this.gameObject.transform.position + new Vector3(0f, 1f + Mathf.Cos(Time.timeSinceLevelLoad) * 0.3f, 0f);			
		}
	}

    private void OnTriggerEnter(Collider trigger)
    {
        if (Ball != null)
        {
            ThrowComponent comp = trigger.gameObject.GetComponentInChildren<ThrowComponent>();
            if (comp != null)
            {
				if (comp.Prefab == null)
				{
                	comp.Prefab = this.Prefab;
					PickupBall(comp.transform);
				}
            }
        }
    }
	
	private void PickupBall(Transform trans)
	{
		Ball.transform.parent = trans;
		Ball.transform.localPosition = new Vector3(0f, -0.7f, 1f);
		Ball = null;
		
		this.audio.PlayOneShot(PickupSound);

		float randomdelay = Random.Range(SpawnMinDelay, SpawnMaxDelay);
	
		StartCoroutine(SpawnBall(randomdelay));
	}
	
	private void CreateBall()
	{
	    Ball = GameObject.Instantiate(Prefab) as GameObject;
        Ball.transform.position = this.gameObject.transform.position + new Vector3(0f, 1f, 0f);
        Rigidbody rbcomp = Ball.GetComponent<Rigidbody>();
		rbcomp.useGravity = false;
		
		//TODO play spawn sound
	}
	
	private IEnumerator SpawnBall(float Delay)
    {
        yield return new WaitForSeconds(Delay);
		
		CreateBall();
    }

}
