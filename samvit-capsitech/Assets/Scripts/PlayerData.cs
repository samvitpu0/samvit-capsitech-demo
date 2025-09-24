using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    private int currentLevel = 0;
    private int currentTotalTurns = 0;
    private int currentMatches = 0;
    private int currentComboCounter = 0;

    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    public int CurrentTotalTurns
    {
        get { return currentTotalTurns; }
        set { currentTotalTurns = value; }
    }

    public int CurrentMatches
    {
        get { return currentMatches; }
        set { currentMatches = value; }
    }

    public int CurrentComboCounter
    {
        get { return currentComboCounter; }
        set { currentComboCounter = value; }
    }
    
    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("CurrentTotalTurns", currentTotalTurns);
        PlayerPrefs.SetInt("CurrentMatches", currentMatches);
        PlayerPrefs.SetInt("CurrentComboCounter", currentComboCounter);
    }

    public void LoadPlayerData()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        currentTotalTurns = PlayerPrefs.GetInt("CurrentTotalTurns", 0);
        currentMatches = PlayerPrefs.GetInt("CurrentMatches", 0);
        currentComboCounter = PlayerPrefs.GetInt("CurrentComboCounter", 0);
    }
}
