using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour
{
    
    [SerializeField] private string _pieceName;

    // Dictionary which stores Valid Card, and Follow Up

    [Serializable] private struct CardPair
    {
        public string cardPreItem;
        public string cardPostItem;
    }

    [SerializeField] private List<CardPair> _validCards = new List<CardPair>(); 
    

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name + " Piece Clicked!");

        CardHolder _ch = FindFirstObjectByType<CardHolder>();
        RandomCardsPool _rcp = FindFirstObjectByType<RandomCardsPool>();

        foreach (CardPair pair in _validCards)
        {
            if (pair.cardPreItem == _ch.HeldCard().CardName)
            {
                string _nextCardName = pair.cardPostItem;
                Card _nextCard = _rcp.GetCard(_nextCardName);
                Debug.Log("Change Card: " + _ch.HeldCard().CardName + " to " + _nextCard.CardName);
                _ch.LoadCard(_nextCard);
            }
        }

        Destroy(gameObject);

    }
}
