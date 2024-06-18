using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logic : MonoBehaviour
{
    // Start is called before the first frame update
    public int PlayerScore;
    public Text ScoreText;
    public GameObject gameOverObject;
    [ContextMenu("Increase Score")]
    public void addScore()
    {
        PlayerScore = PlayerScore + 1;
        ScoreText.text = $"Score : {PlayerScore}";
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverObject.SetActive(true);
    }
}
