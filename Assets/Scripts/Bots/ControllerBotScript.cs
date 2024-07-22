using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Services;
using Unity.Serialization.Json;
using UnityEngine;

public class ControllerBotScript : IBotScript
{
    private ControlService controlService;
    public ControllerBotScript()
    {
        controlService = new ControlService();
    }

    public BotCommands GetCommands(BotInput botInput)
    {
        return controlService.GetCommands(botInput);
    }
}