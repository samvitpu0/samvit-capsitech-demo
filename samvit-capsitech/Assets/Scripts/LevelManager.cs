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

    private int level;
    private int totalTurns = 0;
    private int matches = 0;
    
    private List<Card> cards = new List<Card>();
    private Queue<Card> selectedCards = new Queue<Card>();
    
    private CardGridGenerator cardGridGenerator;
    private CardDataAsset cardDataAsset;
    
    [Header("Level Timers")]
    [SerializeField] private float levelReviewTime = 2f;
    [SerializeField] private float cardResetTime = 1f;
    [SerializeField] private float matchedRemoveTime = 1.5f;
    
    private void Update()
    {
        if (selectedCards.Count > 1)
        {
            var cardOne = selectedCards.Dequeue();
            var cardTwo = selectedCards.Dequeue();
            if (cardOne.CardCode == cardTwo.CardCode)
            {
                //Matched
                totalTurns++;
                matches++;
                EventBus.Publish(new TotalTurnChanged{ TotalTurns = totalTurns });
                EventBus.Publish(new TotalMatches{ Matches = matches });
                
                StartCoroutine(RemoveMatchedCards(cardOne, cardTwo));
            }
            else
            {
                //Back To Normal
                totalTurns++;
                EventBus.Publish(new TotalTurnChanged{ TotalTurns = totalTurns });
                
                StartCoroutine(ResetCards(cardOne, cardTwo));
            }
        }

        if (cards.Count <= 0)
        {
            EventBus.Publish(new Win{ LevelWon = 1});
        }
    }

    private IEnumerator RemoveMatchedCards(Card _cardOne, Card _cardTwo)
    {
        yield return new WaitForSeconds(matchedRemoveTime);
        
        _cardOne.gameObject.SetActive(false);
        _cardOne.GetComponent<Button>().onClick.RemoveAllListeners();
        cards.Remove(_cardOne);
        
        _cardTwo.gameObject.SetActive(false);
        _cardTwo.GetComponent<Button>().onClick.RemoveAllListeners();
        cards.Remove(_cardTwo);
    }

    private IEnumerator ResetCards(Card _cardOne, Card _cardTwo)
    {
        yield return new WaitForSeconds(cardResetTime);
        CardUnflip(_cardOne);
        CardUnflip(_cardTwo);
    }
    public void GenerateLevel(int _rows, int _columns, CardGridGenerator _gridGenerator, CardDataAsset _cardDataAsset)
    {
        rows = _rows;
        columns = _columns;
        cardGridGenerator = _gridGenerator;
        cardDataAsset = _cardDataAsset;
        
        cards = cardGridGenerator.GenerateGrid(rows,columns);

        for (int i = 0; i < cards.Count; i++)
        {
            int randomNumber = UnityEngine.Random.Range(1, cardDataAsset.CardsDataElements.Count);
            
            var tempCard_1 = cards[i];
            tempCard_1.CardCode = randomNumber;
            tempCard_1.CardSprite = cardDataAsset.CardsDataElements[randomNumber].CardImage;
            tempCard_1.IsFlipped = true;
            tempCard_1.GetComponent<Image>().sprite = tempCard_1.CardSprite;
            tempCard_1.GetComponent<Button>().onClick.AddListener(() => CardFlip(tempCard_1));
            
            i++;
            
            var tempCard_2 = cards[i];
            tempCard_2.CardCode = randomNumber;
            tempCard_2.CardSprite = cardDataAsset.CardsDataElements[randomNumber].CardImage;
            tempCard_2.IsFlipped = true;
            tempCard_2.GetComponent<Image>().sprite = tempCard_2.CardSprite;
            tempCard_2.GetComponent<Button>().onClick.AddListener(() => CardFlip(tempCard_2));
            
            // This loop make sure every level has a win condition
        }
        StartCoroutine(UnFlipAllCards());
    }

    private IEnumerator UnFlipAllCards()
    {
        yield return new WaitForSeconds(levelReviewTime);
        foreach (var card in cards)
        {
            CardUnflip(card);
        }
    }

    private void CardFlip(Card _card)
    {
        if (_card.IsFlipped) return;
        
        _card.IsFlipped = true;
        _card.GetComponent<Image>().sprite = _card.CardSprite;
        selectedCards.Enqueue(_card);
    }
    private void CardUnflip(Card _card)
    {
        _card.IsFlipped = false;
        _card.GetComponent<Image>().sprite = cardDataAsset.CardsDataElements[0].CardImage;
    }
    
}
