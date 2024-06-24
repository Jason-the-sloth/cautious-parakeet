using System;
using System.Collections.Generic;
using UnityEngine;

public class JasonBotScript : MonoBehaviour
{
	public JasonBotScript (){}

	public static BotCommands getCommands(List<GameObject> gameObjects) 
	{

		BotCommands botCommands = new BotCommands(Vector2.zero, 0f, false);

		return botCommands;
	}
}