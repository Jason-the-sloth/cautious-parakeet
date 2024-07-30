using System;

using UnityEngine;

public class PlayerSetUpScript : MonoBehaviour
{
    //public GameObject playerTriangle;

    //public Vector2 p1;
    //public Vector2 p2;
    public GlobalVariables globalVariables;
	private int numberOfTeams = 0;

    // Start is called before the first frame update
    void Start()
	{
        globalVariables = Resources.Load<GlobalVariables>("GlobalVariables");

        foreach (var bot in SharedData.Bots)
		{
			numberOfTeams++;
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
		player.GetComponent<Renderer>().material.color = SharedData.TeamColors[numberOfTeams - 1];
        player.GetComponent<BotScript>().SetBotScript(botScript);
		player.GetComponent<BotScript>().team = $"Team{numberOfTeams}";
        player.GetComponent<BotScript>().teamColor = SharedData.TeamColors[numberOfTeams-1];
    }

	// Update is called once per frame
	void Update()
	{

	}
}
