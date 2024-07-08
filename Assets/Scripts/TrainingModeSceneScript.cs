using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainingModeSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    Dropdown BotOneDropdown, BotTwoDropdown;
    List<string> BotOptions;
    string SelectedBot1; 
    string SelectedBot2;


    void Start()
    {
        initializeData();
    }

    private void initializeData()
    {
        BotOneDropdown = GameObject.Find("Bot1Dropdown").GetComponent<Dropdown>();
        BotTwoDropdown = GameObject.Find("Bot2Dropdown").GetComponent<Dropdown>();
        BotOptions = (new List<string> { "Select Script" });
        BotOptions.AddRange(getBotNames("Assets\\Scripts\\Bots"));

        if (BotOneDropdown != null && BotTwoDropdown != null)
        {
            BotOneDropdown.ClearOptions();
            BotOneDropdown.AddOptions(BotOptions.ToList());

            BotTwoDropdown.ClearOptions();
            BotTwoDropdown.AddOptions(BotOptions.ToList());
        }
        BotOneDropdown.onValueChanged.AddListener(delegate { BotOneDropDownSelected(BotOneDropdown); });
        BotTwoDropdown.onValueChanged.AddListener(delegate { BotTwoDropDownSelected(BotTwoDropdown); });

    }
    void BotOneDropDownSelected(Dropdown dropdown)
    {
        SelectedBot1 = dropdown.options[dropdown.value].text;
    }

    void BotTwoDropDownSelected(Dropdown dropdown)
    {
        SelectedBot2 = dropdown.options[dropdown.value].text;
    }
    List<string> getBotNames(string path)
    {

        string[] filePaths = Directory.GetFiles(path);

        List<string> fileNames = new List<string>();
        foreach (string filePath in filePaths)
        {
            fileNames.Add(Path.GetFileName(filePath).Split(".")[0]);
        }
        return fileNames.Distinct().ToList();

    }

    public void onStart()
    {
        if (SelectedBot1 == null || SelectedBot2 == null)
        {

            EditorUtility.DisplayDialog("Mmm", $"You did not select bots to train", "Ok");

            return;
        }
        Type botOneType = Type.GetType(SelectedBot1);
        Type botTwoType = Type.GetType(SelectedBot2);

        if (botOneType != null || botTwoType != null)
        {
            object _botOne = Activator.CreateInstance(botOneType);
            object _botTwo = Activator.CreateInstance(botTwoType);

            if (_botOne is IBotScript botScriptOne && _botTwo is IBotScript botScriptTwo)
            {
                SharedData.Bots = new Dictionary<string, IBotScript>() {
                    { SelectedBot2, botScriptTwo },
                    {SelectedBot1,botScriptOne},
                };
                SceneManager.LoadScene("Base");
            }
            else
            {
                selectedBotError($"{SelectedBot1 ?? "Bot 1"} or {SelectedBot2 ?? "Bot 2"}");
            }

        }
        else
        {
            selectedBotError($"{SelectedBot1 ?? "Bot 1"} or {SelectedBot2 ?? "Bot 2"}");
        }

    }
    void selectedBotError(string bot)
    {
        EditorUtility.DisplayDialog("Mmm", $" {bot} does not exist or does not implement IBotScript", "Ok");

    }
}
