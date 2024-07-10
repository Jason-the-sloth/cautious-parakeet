using Control.Controllers;
using Control.Models;
using UnityEngine;

namespace Control.Services
{
    public class BotService : IBotService
    {
        private GameObject botGameObject;
        private readonly ILogger<BotController> _logger;

        public BotService(ILogger<BotController> logger)
        {
            _logger = logger;
        }

        public BotCommands GetCommands(List<Collider2D> colliders)
        {
            var colliderMap = new Dictionary<String, List<Collider2D>>();

            SetGameObject(colliders);

            if (botGameObject == null)
            {
                new BotCommands();
            }

            foreach (var collider in colliders)
            {
                var gameObject = collider.gameObject;
                var tag = gameObject.tag;
                if (gameObject == botGameObject) continue;

                if (!colliderMap.ContainsKey(tag))
                {
                    colliderMap[tag] = new List<Collider2D>();
                }

                colliderMap[tag].Add(collider);
            }

            //if found the enemy player shoot at and try to maintain distance
            if (colliderMap.TryGetValue("player", out var enemy))
            {
                return FoundEnemy(enemy[0]);
            }
            //else search
            else if (colliderMap.TryGetValue("border", out var borders))
            {
                return SearchForEnemy(borders);
            }

            return new BotCommands();
        }

        private BotCommands SearchForEnemy(List<Collider2D> borders)
        {
            BotCommands botCommands;
            if (borders != null && borders.Count > 0)
            {
                Vector2 pos = botGameObject.transform.position;
                float rotate = 0f;

                float angle = Vector2.SignedAngle(Vector2.zero - pos, botGameObject.transform.up);

                if (angle < -20.0F)
                {
                    rotate = 1f;
                }
                else if (angle > 20.0F)
                {
                    rotate = -1f;
                }
                botCommands = new(Vector2.up, rotate, false);
            }
            else
            {
                //fly straight
                botCommands = new(Vector2.up, 0f, false);
            }
            return botCommands;
        }

        private BotCommands FoundEnemy(Collider2D firstPlayer)
        {
            Vector2 move = Vector2.zero;
            float rotate = 0f;
            bool shoot = false;

            float angle = Vector2.SignedAngle(firstPlayer.transform.position - botGameObject.transform.position, botGameObject.transform.up);

            if (angle < -5.0F)
            {
                rotate = 1f;
            }
            else if (angle > 5.0F)
            {
                rotate = -1f;
            }
            else
            {
                shoot = true;
            }

            float distance = Vector2.Distance(firstPlayer.transform.position, botGameObject.transform.position);
            if (distance > 4)
            {
                move = Vector2.up;
            }
            else if (distance < 2)
            {
                move = Vector2.down;
            }
            return new BotCommands(move, rotate, shoot);
        }

        private void SetGameObject(List<Collider2D> colliders)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("player"))
                {
                    botGameObject = collider.gameObject;
                    _logger.LogDebug("I am {name}", botGameObject.name);
                }
            }
        }
    }
}
