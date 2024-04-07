using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtils : MonoBehaviour
{

    public static bool IsPaused;
    
    // Pausing/unpausing the game (setting the timescale to 0 effectively pauses time)
    public static void Pause()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }
    
    public static void UnPause()
    {
        Time.timeScale = 1;
        IsPaused = false;
    }

}
