using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager _Instance;
	public static GameManager Instance
	{
		get { return _Instance; }
	}
	
	private GUIStyle style = new GUIStyle();
	
    private enum GameStatus {Waiting, Playing, LevelComplete};

    private GameStatus Status = GameStatus.Waiting;
	
	[HideInInspector]	
    public int Score;
	
	public float RoundTime = 60f;
	private float TimeLeft = 0f;

	public Texture RetryImage;
	

    private void Awake()
    {
        _Instance = this;
    }

	private void Start()
	{		
		style.fontSize = 25;
		style.normal.textColor = new Color(0.5f, 0.5f, 1f);
		StartCoroutine(StartGame());
	}
	
	private IEnumerator StartGame()
    {
		Status = GameStatus.Waiting;
		Score = 0;
		TimeLeft = RoundTime;
	
        yield return new WaitForSeconds(3f);

		Status = GameStatus.Playing;
    }
	
	private void Update()
	{
		if (Status == GameStatus.Playing)
		{
			TimeLeft -= Time.deltaTime;
			
			if (TimeLeft <= 0)
			{
				GameObject player = GameObject.Find("Player");
				MouseLook[] comps = player.GetComponentsInChildren<MouseLook>();
				for (int i = 0; i < comps.Length; i++)
				{
					comps[i].enabled = false;
				}
				ThrowComponent comp2 = player.GetComponentInChildren<ThrowComponent>();
				comp2.enabled = false;
	
				Status = GameStatus.LevelComplete;
			}
		}
	}
	
	private void OnGUI()
	{
        switch (Status)
        {
            case GameStatus.Playing:
				GUI.Label(new Rect(100f, Screen.height - 200f, 100f, 50f), "Score: " + Score.ToString(), style);
				GUI.Label(new Rect(100f, Screen.height - 150f, 100f, 50f), string.Format("Time Left: {0:00}", TimeLeft), style);
                break;
            case GameStatus.LevelComplete:
				Screen.lockCursor = false;
				GUI.Box(new Rect((Screen.width * 0.5f) - (200f), (Screen.height * 0.5f) - (200f), 400f, 400f), string.Empty);
			
				Rect buttonPos = new Rect(
					(Screen.width * 0.5f) - (50f),
					(Screen.height * 0.5f) + (25f),
					100f,
					100f);
				if (GUI.Button(buttonPos, RetryImage))
				{
		            ResetLevel();   
				}

                GUI.Label(new Rect((Screen.width * 0.5f) - (60f), (Screen.height * 0.5f) - (100f), 100f, 50f), "Your Score: " + Score.ToString(), style);
                break;
        }
	}
		
	public void ResetLevel()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}
}
