using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager _Instance;
	public static GameManager Instance
	{
		get { return _Instance; }
	}
	
	public Texture RetryImage;
	public Texture NextImage;
	public Texture MenuImage;
	
	private GUIStyle style = new GUIStyle();
	
    private enum GameStatus {Playing, LevelLose, LevelComplete, GameComplete};

    private GameStatus Status = GameStatus.Playing;

    private void Awake()
    {
        _Instance = this;
    }

	private void Start()
	{		
		style.fontSize = 25;
		style.normal.textColor = new Color(0.5f, 0.5f, 1f);
		
		_ThrowsCount = Throws;
	}

	private void Update()
	{
		if (Input.GetButtonUp("Jump"))
		{
			GameManager.Instance.ResetLevel();
		}
	}	
	
	private void OnGUI()
	{		
        switch (Status)
        {
            case GameStatus.Playing:
                GUI.Label(new Rect(100f, Screen.height - 200f, 100f, 50f), "Boxes Left: " + _BoxCount.ToString(), style);
                GUI.Label(new Rect(100f, Screen.height - 100f, 100f, 50f), "Throws Left: " + _ThrowsCount.ToString(), style);
                break;
			
            case GameStatus.LevelComplete:
				DoLevelComplete();
                break;

            case GameStatus.LevelLose:
				DoLevelLose();
                break;
			
            case GameStatus.GameComplete:
				DoGameComplete();
                break;
        }
	}

    public string NextLevel;
    public int Throws;

	private int _ThrowsCount = 0;
	public int ThrowsCount
	{
		get { return _ThrowsCount; }
		set
		{
			_ThrowsCount = value;
			
			if (_ThrowsCount == 0)
			{
                StartCoroutine(CheckResults());
			}
		}
	}
	
	private int _BoxCount = 0;
	public int BoxCount
	{
		get { return _BoxCount; }
		set
		{
			_BoxCount = value;
			
			if (_BoxCount == 0)
			{
                StartCoroutine(CheckResults());
			}
		}
	}

	public void ResetLevel()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}
    
    private IEnumerator CheckResults()
    {
		GameObject player = GameObject.Find("Player");
		MouseLook comp = player.GetComponent<MouseLook>();
		comp.enabled = false;
		
        yield return new WaitForSeconds(3f);

		if (_BoxCount > 0)
		{
            Status = GameStatus.LevelLose;
		}		
		else if (NextLevel == string.Empty)
		{
            Status = GameStatus.GameComplete;
		}
		else
		{
			Status = GameStatus.LevelComplete;
		}
    }
	
	private void DoLevelComplete()
	{
		Rect buttonPos;

		Screen.lockCursor = false;
		GUI.Box(new Rect((Screen.width * 0.5f) - (200f), (Screen.height * 0.5f) - (200f), 400f, 400f), string.Empty);
	
		buttonPos = new Rect(
			(Screen.width * 0.5f) - (50f),
			(Screen.height * 0.5f) + (25f),
			100f,
			100f);
		if (GUI.Button(buttonPos, NextImage))
		{
            Application.LoadLevel(NextLevel);   
		}

        GUI.Label(new Rect((Screen.width * 0.5f) - (100f), (Screen.height * 0.5f) - (100f), 100f, 50f), "Level Complete", style);
	}
	
	private void DoLevelLose()
	{
		Rect buttonPos;
	
		Screen.lockCursor = false;
		GUI.Box(new Rect((Screen.width * 0.5f) - (200f), (Screen.height * 0.5f) - (200f), 400f, 400f), string.Empty);
	
		buttonPos = new Rect(
			(Screen.width * 0.5f) - (50f),
			(Screen.height * 0.5f) + (25f),
			100f,
			100f);
		if (GUI.Button(buttonPos, RetryImage))
		{
            ResetLevel();   
		}

        GUI.Label(new Rect((Screen.width * 0.5f) - (60f), (Screen.height * 0.5f) - (100f), 100f, 50f), "Game Over", style);
	}
	
	private void DoGameComplete()
	{
		Rect buttonPos;
	
		Screen.lockCursor = false;			
		GUI.Box(new Rect((Screen.width * 0.5f) - (200f), (Screen.height * 0.5f) - (200f), 400f, 400f), string.Empty);
	
		Rect pos2 = new Rect(
			(Screen.width * 0.5f) - (50f),
			(Screen.height * 0.5f) + (25f),
			100f,
			100f);
		if (GUI.Button(pos2, MenuImage))
		{
			Application.LoadLevel("Menu");
		}

        GUI.Label(new Rect((Screen.width * 0.5f) - (100f), (Screen.height * 0.5f) - (100f), 100f, 50f), "You Win!!!", style);
	}
}
