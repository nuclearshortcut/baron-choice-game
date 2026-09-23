using System.Collections.Generic;
using UnityEngine;

public class RandomCardsPool : MonoBehaviour
{

     private static RandomCardsPool instance;

    [SerializeField] private List<Card>_randomCards = new List<Card>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public Card GetRandomCard()
    {
        if (_randomCards.Count < 1) {return null;}

        int randomIndex = Random.Range(0, _randomCards.Count - 1);
        Card nextCard = _randomCards[randomIndex];

        if (nextCard.OneTime) {_randomCards.Remove(_randomCards[randomIndex]);}

        return nextCard;
    }

    public void AddCard(Card newCard)
    {
        _randomCards.Add(newCard);
    }

    public void AddCardSet(List<Card> newCards)
    {
        _randomCards.AddRange(newCards);
    }

}
