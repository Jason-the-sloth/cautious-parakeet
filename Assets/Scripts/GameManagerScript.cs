using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour 
{
    public GameObject TrainingMode;
    public GameObject TournamentMode;
    public GameObject FirstMenuPanel;
    Dropdown TrainingBotDropDownSelect;
    string SelectedBot;

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        TrainingBotDropDownSelect = GameObject.Find("BotDropdown").GetComponent<Dropdown>();
        TrainingMode.SetActive(false);
        TournamentMode.SetActive(false);
    }

    #region First Menu
    public void onTournamentMode()
    {
        FirstMenuPanel.SetActive(false);
        TournamentMode.SetActive(true);
    }
    public void onTrainingMode()
    {
        FirstMenuPanel.SetActive(false);

        // TODO -> GetScripts
        var options = getBotNames("Assets\\Scripts\\Bots");
       
        if (TrainingBotDropDownSelect != null)
        {
            TrainingBotDropDownSelect.ClearOptions();
            TrainingBotDropDownSelect.AddOptions(options.ToList());
        }
        TrainingBotDropDownSelect.onValueChanged.AddListener(delegate { BotDropDownSelected(TrainingBotDropDownSelect); });
        TrainingMode.SetActive(true);

    }
    void BotDropDownSelected(Dropdown dropdown)
    {
        SelectedBot = dropdown.options[dropdown.value].text;
    }
    List<string> getBotNames(string path)
    {
        
            string[] filePaths = Directory.GetFiles(path);

            List<string> fileNames = new List<string>();
            foreach (string filePath in filePaths)
            {
                fileNames.Add(Path.GetFileName(filePath));
            }
            return fileNames.ToList();
        
    }
    #endregion

    #region training mode region
    public void onStationary()
    {

    }
    public void onHumanControlled()
    {
        SceneManager.LoadScene("Base");
    }
    public void onBasicBot()
    {

    }
    public void onBotSelect(string botName)
    {

    }
     
    #endregion

    #region tournament mode region

    #endregion
}
