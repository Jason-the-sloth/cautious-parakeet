using System.Collections.Generic;
using UnityEngine;

public interface BotScriptInterface
{
	public BotCommands GetCommands(List<RaycastHit2D> objects);
}