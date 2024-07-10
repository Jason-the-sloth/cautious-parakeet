using Control.Models;
using UnityEngine;

namespace Control.Services
{
    public interface IBotService
    {
        BotCommands GetCommands(List<Collider2D> colliders);
    }
}
