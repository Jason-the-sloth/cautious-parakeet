using System.Collections.Generic;
using UnityEngine;

public interface BotScriptInterface
{
	public BotCommands GetCommands(List<Collider2D> objects);
}