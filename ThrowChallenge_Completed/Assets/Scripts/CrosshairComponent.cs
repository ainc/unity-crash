using UnityEngine;
using System.Collections;

public class CrosshairComponent : MonoBehaviour
{
	Texture CrossHairTexture = null;
	
	private void Start()
	{
		CrossHairTexture = Resources.Load("HUD/Crosshair") as Texture;
		
		Screen.lockCursor = true;
	}

    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            Screen.lockCursor = true;
        }
    }
	
	private void OnGUI()
	{		
		if (CrossHairTexture != null)
		{
			GUI.color = new Color(1, 1, 1, 0.8f);
			Rect drawrect = new Rect(
				(Screen.width * 0.5f) - (CrossHairTexture.width * 0.5f),
				(Screen.height * 0.5f) - (CrossHairTexture.height * 0.5f),
				CrossHairTexture.width,
				CrossHairTexture.height
				);
			GUI.DrawTexture(drawrect, CrossHairTexture);
			GUI.color = Color.white;
		}
	
	}	
}
