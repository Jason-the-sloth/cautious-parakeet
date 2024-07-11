using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSceneScript : MonoBehaviour
{

   
    public void onTournamentMode()
    {
        SceneManager.LoadScene("TournamentMode");
    }
    public void onTrainingMode()
    {
        SceneManager.LoadScene("TrainingMode");

    }
    
    
}
