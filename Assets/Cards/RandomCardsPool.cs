using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RandomCardsPool : MonoBehaviour
{

     private static RandomCardsPool instance;

    [SerializeField] private List<Card>_cardPool = new List<Card>();

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
        if (_cardPool.Count < 1) {return null;}

        int randomIndex = Random.Range(0, _cardPool.Count - 1);
        Card nextCard = _cardPool[randomIndex];

        return nextCard;
    }

    public void AddCard(Card newCard)
    {
        _cardPool.Add(newCard);
    }

    public void AddCardPack(string newCardPack)
    {
        // TODO: Read json files 
        string folderPath = Path.Combine(Application.persistentDataPath, "CardPacks");
        string filePath = Path.Combine(folderPath, newCardPack + ".json");
        string cardPackData;

        if (File.Exists(filePath))
        {
            cardPackData = File.ReadAllText(filePath);
        }

        while (true)
        {
            // Read and Get Card Name

            // Read and Get Card Desc

            // Read and Get Card Dialogue

            // Read and Get DirSelecs
              // Attribute Effects
              // Follow Up Card String
              // New Cards File Name
              // Delete?

            // Doom Card?
        }


        // _cardPool.AddRange(newCards);
    }

}
