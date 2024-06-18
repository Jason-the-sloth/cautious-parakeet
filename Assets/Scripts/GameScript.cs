using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [SerializeField] Text menuTextComponent;
    [SerializeField] MenuState menuState;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject game;

    MenuState currentMenuState;


    public void Start()
    {
        currentMenuState = menuState;
        menuTextComponent.text = currentMenuState.GetMenuText();
    }

    public void Update()
    {
        MenuStateEnum status = currentMenuState.GetCurrentState();
        quitCheck(status);
        pauseCheck(status);
        startCheck(status);
    }

    public void quitCheck(MenuStateEnum state)
    {
        if (
            state == MenuStateEnum.GAME_LAUNCHED
            || state == MenuStateEnum.GAME_OVER
            || state == MenuStateEnum.GAME_PAUSED
        )
        {
            bool canExit = currentMenuState.GetCanExit();
            if (canExit && Input.GetKeyDown(KeyCode.Q))
            {
#if UNITY_STANDALONE
                Application.Quit();
#endif

                // If we are running in the editor
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }
    public void pauseCheck(MenuStateEnum status)
    {
        if (status == MenuStateEnum.GAME_STARTED
        || status == MenuStateEnum.GAME_PAUSED
        )
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                currentMenuState = currentMenuState.GetMenuStates()[0];
                canvas.enabled = !canvas.enabled;
                game.GetComponent<GameMode>().toggleGameMode(status);
                menuTextComponent.text = currentMenuState.GetMenuText();
            }
        }
    }
    public void startCheck(MenuStateEnum status)
    {
        if (status == MenuStateEnum.GAME_LAUNCHED
        || status == MenuStateEnum.GAME_OVER
        )
        {
            string message = (status == MenuStateEnum.GAME_LAUNCHED) ? "Starting" : "Restarting";
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentMenuState = currentMenuState.GetMenuStates()[0];
                menuTextComponent.text = currentMenuState.GetMenuText();
                canvas.enabled = false;
                game.GetComponent<GameMode>().toggleGameMode(status);
            }
        }
    }

}