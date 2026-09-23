using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Card : MonoBehaviour
{

    // Internal name of the Card
    [SerializeField] private string cardName;
    public string CardName()
    {
        return cardName;
    }

    // Description
    public string cardDesc;

    // Dialogue/Text
    public string cardDialogue;

    // A class for possible Directional Selections made via Card swiping
    [System.Serializable] public class DirSelection
    {

        // Effects on Attributes
        public int loveEffect;
        public int approEffect;
        public int presEffect;
        public int infEffect;
        public int armyEffect;
        public int churEffect;
        public int goldEffect;
        public int WeekProgress;
        public int YearProgress;

        public Card FollowUpCard; // The card that comes immediately after the current one

        public List<Card> newCards; // New Cards to add
    }

    // Four maximum Directional Selections
    // [0] Up, [1] Right, [2] Down, [3] Left
    [SerializeField] public DirSelection[] dirSelecs = new DirSelection[4];

    [SerializeField] private bool _doomCard;

    public bool OneTime;

    void Update()
    {
        // *Always lock dirSelecs at 4 max
    }




}
