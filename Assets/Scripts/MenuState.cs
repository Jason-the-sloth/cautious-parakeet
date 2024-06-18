using UnityEngine;

[CreateAssetMenu(menuName = "MenuState")]
public class MenuState : ScriptableObject
{
    [SerializeField] MenuStateEnum stateEnum;
    [TextArea(10, 14)][SerializeField] string text;
    [SerializeField] MenuState[] optionList;
    [SerializeField] bool canExit;

    public MenuStateEnum GetCurrentState()
    {
        return stateEnum;
    }
    public string GetMenuText()
    {
        return text;
    }

    public MenuState[] GetMenuStates()
    {
        return optionList;
    }

    public bool GetCanExit()
    {
        return canExit;
    }
}
