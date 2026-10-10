using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RandomCardsPool : MonoBehaviour
{

    // TODO: Have dirs get pieces and load them to the inventory content
    // TODO: Show dir text beside each lean
    // TODO: Fix bug where Cards are not removed from the pool when picking the appropriate direction
    // TODO: Fix cards spawning in the scene before they should
    // TODO: Set up demo

    private static RandomCardsPool instance;

    [SerializeField] private List<Card> _cardPool = new List<Card>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public Card GetCard(string cardName)
    {
        foreach(Card card in _cardPool)
        {
            if (card.CardName == cardName)
            {
                return card;
            }
        }
        Debug.LogError($"No Card of the name {cardName} found!");
        return null;
    }

    public Card GetRandomCard()
    {
        if (_cardPool.Count < 1) {return null;}

        Card nextCard;

        // Check to make sure the Card is tagged as Random
        while(true)
        {
            int randomIndex = Random.Range(0, _cardPool.Count - 1);
            nextCard = _cardPool[randomIndex];  
            if (nextCard.IsRandCard == true) {break;}
        }

        return nextCard;
    }

    public void AddCard(Card card)
    {
        _cardPool.Add(card);
    }

    public void RemoveCard(Card card)
    {
       _cardPool.Remove(card);
    }

    public void AddCardPack(string filename)
    {
        string folderPath = Path.Combine(Application.streamingAssetsPath, "CardPacks");
        string filePath = Path.Combine(folderPath, filename + ".json");
        string cardPackString;

        if (File.Exists(filePath))
        {
            cardPackString = File.ReadAllText(filePath);

            // Read the entire packet file into the wrapper
            CardPackContainer packData = JsonUtility.FromJson<CardPackContainer>(cardPackString);

            if (packData != null && packData.cards != null)
            {
                // Loop through every object in the pack
                int i = 0;
                foreach (CardDataContainer rawData in packData.cards)
                {
                    Card newCard = new GameObject("card_" + filename + "_" + i).AddComponent<Card>();

                    // Construct card
                    newCard.init(
                        rawData.cardName, 
                        rawData.cardDesc, 
                        rawData.cardDialogue, 
                        rawData.dirSelecs, 
                        rawData._doomCard,
                        rawData._randCard
                    );

                    // Add card to the pool
                    _cardPool.Add(newCard);
                    i++;
                }
                
            }
        }
        else
        {
            Debug.LogError($"Card Pack JSON file for {filename} not found at: {filePath}");
        }

    }

    [System.Serializable] public class CardPackContainer
    {
        public CardDataContainer[] cards;
    }

    [System.Serializable] public class CardDataContainer
    {
        public string cardName;
        public string cardDesc;
        public string cardDialogue;
        public Card.DirSelection[] dirSelecs;
        public bool _doomCard;
        public bool _randCard;
    }

}
