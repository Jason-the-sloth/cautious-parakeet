using System.Collections.Generic;
using UnityEngine;

public interface IBotScript
{
	public BotCommands GetCommands(BotInput botinput);
}