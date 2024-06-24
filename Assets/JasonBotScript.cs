using System;
using System.Collections.Generic;
using UnityEngine;

public class JasonBotScript : BotScriptInterface
{
	public JasonBotScript (){}

	public BotCommands GetCommands(List<GameObject> gameObjects) 
	{

		BotCommands botCommands = new(Vector2.zero, 0f, false);

		return botCommands;
	}
}