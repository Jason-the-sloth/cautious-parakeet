
using System.Collections.Generic;
using UnityEngine;

public class SharedData : MonoBehaviour
{
    public static Dictionary<string , IBotScript> Bots = new Dictionary<string, IBotScript>() { { nameof(HumanPlayer),new HumanPlayer() } };
   
}
