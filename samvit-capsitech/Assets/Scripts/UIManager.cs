using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text totalTurnsText;
    [SerializeField] private TMP_Text totalMatchesText;
    [SerializeField] private TMP_Text currentLevelText;
    [SerializeField] private Transform winPanel;

    private void OnEnable()
    {
        EventBus.Subscribe<TotalTurnChanged>(OnTotalTurnChanged);
        EventBus.Subscribe<TotalMatches>(OnTotalMatchesChanged);
        EventBus.Subscribe<Win>(OnWin);
    }
    private void OnTotalTurnChanged(TotalTurnChanged _eventData)
    {
        totalTurnsText.text = _eventData.TotalTurns.ToString();
    }
    private void OnTotalMatchesChanged(TotalMatches _eventData)
    {
        totalMatchesText.text = _eventData.Matches.ToString();
    }

    private void OnWin(Win _eventData)
    {
        winPanel.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<TotalTurnChanged>(OnTotalTurnChanged);
        EventBus.Unsubscribe<TotalMatches>(OnTotalMatchesChanged);
        EventBus.Unsubscribe<Win>(OnWin);
    }
}
