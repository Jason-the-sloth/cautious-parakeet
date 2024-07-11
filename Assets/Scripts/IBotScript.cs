using System.Collections.Generic;
using UnityEngine;

public interface IBotScript
{
	public BotCommands GetCommands(string botinput);
}