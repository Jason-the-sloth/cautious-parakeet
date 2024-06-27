using System.Collections.Generic;
using UnityEngine;

public interface IBotScript
{
	public BotCommands GetCommands(List<RaycastHit2D> objects);
}