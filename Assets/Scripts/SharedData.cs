
using System.Collections.Generic;
using UnityEngine;

public class SharedData : MonoBehaviour
{
    public static Dictionary<string, IBotScript> Bots = new Dictionary<string, IBotScript>() { { nameof(HumanPlayer), new HumanPlayer() } };
    public static int TeamOneScore = 0;
    public static int TeamTwoScore = 0;

    public static List<Color> TeamColors = new List<Color> { Color.yellow, Color.cyan, Color.blue}; 

}
