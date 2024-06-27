using System.Collections.Generic;
using UnityEngine;

public interface IBotScript
{
	public BotCommands GetCommands(List<Collider2D> objects);
}