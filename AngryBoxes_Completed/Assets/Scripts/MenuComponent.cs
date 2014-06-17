using UnityEngine;
using System.Collections;

public class MenuComponent : MonoBehaviour
{	
	private GUIStyle style = new GUIStyle();
	public Texture TitleImage;
	public Texture PlayImage;
	
	private void Start()
	{		
		style.fontSize = 25;
		style.normal.textColor = new Color(0.5f, 0.5f, 1f);
	}
	
	private void OnGUI()
	{
		Rect posTitle = new Rect(
			(Screen.width * 0.5f) - (512f),
			(Screen.height * 0.5f) - (400f),
			1024f,
			600f);
	
		GUI.DrawTexture(posTitle, TitleImage, ScaleMode.ScaleToFit);
		
		Rect pos2 = new Rect(
			(Screen.width * 0.5f) - (50f),
			(Screen.height * 0.5f) + (175f),
			100f,
			100f);
		
		if (GUI.Button(pos2, PlayImage))
		{
			Application.LoadLevel("Level1");
		}
	}
}
