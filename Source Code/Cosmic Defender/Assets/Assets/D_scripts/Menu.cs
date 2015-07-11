using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	void OnGUI()
	{	
		GUI.skin.box.normal.textColor = Color.cyan;
		GUI.Box (new Rect (800, 230, 250, 200), "Defender Menu");
		GUI.contentColor = Color.white;
		GUI.color = Color.cyan;
		if(GUI.Button(new Rect(820,300,200,30), "Let's play")) 
		{

				Application.LoadLevel(1);
		}

	}
}
