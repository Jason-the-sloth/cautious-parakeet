using System;
using System.Collections.Generic;
using UnityEngine;

public class JasonBotScript : IBotScript
{
	public JasonBotScript (){}

	public BotCommands GetCommands(List<Collider2D> gameObjects) 
	{

		BotCommands botCommands = new(Vector2.zero, 0f, false);

		return botCommands;
	}
}