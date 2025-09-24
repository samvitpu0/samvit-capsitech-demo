using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] CardGridGenerator cardGridGenerator;
    [SerializeField] CardDataAsset cardDataAsset;

    private PlayerData playerData;
    private GameEventType gameEventType;
    private void OnEnable()
    {
        EventBus.Subscribe<TotalTurnChanged>(OnTotalTurnChanged);
        EventBus.Subscribe<TotalMatches>(OnTotalMatchesChanged);
        EventBus.Subscribe<Win>(OnWin);
    }
    
    private void Start()
    {
        playerData = new PlayerData();
        playerData.LoadPlayerData();
        
        levelManager.GenerateLevel(3,4,cardGridGenerator,cardDataAsset);
    }

    private void OnTotalTurnChanged(TotalTurnChanged _eventData)
    {
        
    }
    private void OnTotalMatchesChanged(TotalMatches _eventData)
    {
        
    }

    private void OnWin(Win _eventData)
    {
        
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<TotalTurnChanged>(OnTotalTurnChanged);
        EventBus.Unsubscribe<TotalMatches>(OnTotalMatchesChanged);
        EventBus.Unsubscribe<Win>(OnWin);
    }
}
