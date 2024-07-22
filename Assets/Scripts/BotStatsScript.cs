

using UnityEngine;
using UnityEngine.UI;

public class BotStatsScript : MonoBehaviour
{
    public GlobalVariables globalVariables;

    public float health;
    public float score = 0;
    public float hits = 0;
    public float numberOfBulletsFired = 0;
    public string playerName;
    

    Text healthText ;
    Text scoreText;
    Text hitsText;
    public Text playerNameText;
    Text numberOfBulletsFiredText;
    public Color textColor = Color.white;
    void Start()
    {
        globalVariables = Resources.Load<GlobalVariables>("GlobalVariables");
        health = globalVariables.maxHealth;

        healthText = transform.Find("Health").GetComponent<Text>();
        scoreText = transform.Find("Score").GetComponent<Text>();
        numberOfBulletsFiredText = transform.Find("BulletsFired").GetComponent<Text>();
        hitsText = transform.Find("Hits").GetComponent<Text>();
        playerNameText = transform.Find("PlayerName").GetComponent<Text>();
        UpdateTextColor(textColor);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatsUI();
    }

    public void addScore(int score)
    {
        this.score = score;
    }
    public void updateHealth(float health)
    {
        this.health = health;
    }

    public void addHits()
    {
        hits += 1;
    }
    public void addBulletsFired()
    {
        numberOfBulletsFired += 1;
    }
    public void UpdateTextColor(Color color)
    {
        healthText.color = color;
        scoreText.color = color;
        hitsText.color = color;
        numberOfBulletsFiredText.color = color;
        playerNameText.color = color;
    }
    void UpdateStatsUI()
    {
        healthText.text = "Health : " + Mathf.Round(health);
        scoreText.text = "Score : " + score;
        hitsText.text = "NO.of Hits : " + hits;
        numberOfBulletsFiredText.text = "NO. Bullets Fired : "+ numberOfBulletsFired;
    }
}
