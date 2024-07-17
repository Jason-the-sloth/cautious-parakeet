using System;

using UnityEngine;

public class PlayerSetUpScript : MonoBehaviour
{
    //public GameObject playerTriangle;

    //public Vector2 p1;
    //public Vector2 p2;
    public GlobalVariables globalVariables;

    // Start is called before the first frame update
    void Start()
	{
        globalVariables = Resources.Load<GlobalVariables>("GlobalVariables");

        foreach (var bot in SharedData.Bots)
		{

			float randomX = UnityEngine.Random.Range(-1 * globalVariables.Width, globalVariables.Width);
			float randomY = UnityEngine.Random.Range(-1 * (globalVariables.Height - 2), (globalVariables.Height - 2));

			CreatePlayer(new Vector2(randomX, randomY), bot.Key, bot.Value);
		}
	}

	void CreatePlayer(Vector2 vec, String name, IBotScript botScript)
	{
		GameObject player = Instantiate(globalVariables.playerTriangle, vec, Quaternion.identity);
		player.transform.Rotate(new Vector3(0, 0, 180 - Vector2.SignedAngle(vec, Vector2.up)));
		player.name = name;
		player.transform.SetParent(transform);
		player.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        player.GetComponent<BotScript>().SetBotScript(botScript);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
