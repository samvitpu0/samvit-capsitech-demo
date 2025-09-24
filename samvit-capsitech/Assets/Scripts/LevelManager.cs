using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private  int rows;
    private int columns;
    
    private List<Card> cards = new List<Card>();
    private Queue<Card> selectedCards = new Queue<Card>();
    
    [SerializeField] private CardGridGenerator cardGridGenerator;
    [SerializeField] private CardDataAsset cardDataAsset;
    
    [Header("Level Timers")]
    [SerializeField] private float levelReviewTime = 2f;
    [SerializeField] private float cardResetTime = 1f;
    [SerializeField] private float matchedRemoveTime = 1.5f;

    private void Start()
    {
        rows = 3;
        columns = 4;
        GenerateLevel();
    }

    private void Update()
    {
        if (selectedCards.Count > 1)
        {
            var cardOne = selectedCards.Dequeue();
            var cardTwo = selectedCards.Dequeue();
            if (cardOne.CardCode == cardTwo.CardCode)
            {
                //Matched
                StartCoroutine(RemoveMatchedCards(cardOne, cardTwo));
            }
            else
            {
                //Back To Normal
                StartCoroutine(ResetCards(cardOne, cardTwo));
            }
        }
    }

    private IEnumerator RemoveMatchedCards(Card _cardOne, Card _cardTwo)
    {
        yield return new WaitForSeconds(matchedRemoveTime);
        _cardOne.gameObject.SetActive(false);
        _cardTwo.gameObject.SetActive(false);
    }

    private IEnumerator ResetCards(Card _cardOne, Card _cardTwo)
    {
        yield return new WaitForSeconds(cardResetTime);
        CardFlipUnflip(_cardOne);
        CardFlipUnflip(_cardTwo);
    }
    private void GenerateLevel()
    {
        cards = cardGridGenerator.GenerateGrid(rows,columns);
        foreach (var card in cards)
        {
            
            int randomNumber = UnityEngine.Random.Range(1, cardDataAsset.CardsDataElements.Count);
            
            card.CardCode = randomNumber;
            card.CardSprite = cardDataAsset.CardsDataElements[randomNumber].CardImage;
            card.IsFlipped = true;
            
            card.GetComponent<Image>().sprite = card.CardSprite;
            var currentCard = card;
            card.GetComponent<Button>().onClick.AddListener(() => CardFlipUnflip(currentCard));
        }
        StartCoroutine(UnFlipAllCards());
    }

    private IEnumerator UnFlipAllCards()
    {
        yield return new WaitForSeconds(levelReviewTime);
        foreach (var card in cards)
        {
            CardFlipUnflip(card);
        }
    }
    private void CardFlipUnflip(Card card)
    {
        if (card.IsFlipped)
        {
            card.IsFlipped = false;
            card.GetComponent<Image>().sprite = cardDataAsset.CardsDataElements[0].CardImage;
        }
        else
        {
            card.IsFlipped = true;
            card.GetComponent<Image>().sprite = card.CardSprite;
            selectedCards.Enqueue(card);
        }
    }
}
