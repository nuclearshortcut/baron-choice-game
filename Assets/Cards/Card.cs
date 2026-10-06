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
    public string CardName => cardName;

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

        public string followUpCard; // The card that comes immediately after the current one

        public string newCardsPack; // New Cards to add

        public bool delete;
    }

    // Four maximum Directional Selections
    // [0] Up, [1] Right, [2] Down, [3] Left
    [SerializeField] public DirSelection[] dirSelecs = new DirSelection[4];

    [SerializeField] private bool _doomCard;
    public bool IsDoomCard => _doomCard;

    [SerializeField] private bool _randCard;
    public bool IsRandCard => _randCard;

    public void init(string cardName, string cardDesc, string cardDialogue, DirSelection[] dirSelecs, bool isDoom, bool isRand)
    {
        this.cardName = cardName;
        this.cardDesc = cardDesc;
        this.cardDialogue = cardDialogue;
        for (int i = 0; i < dirSelecs.Length; i++)
        {
            this.dirSelecs[i] = dirSelecs[i];
        }
        this._doomCard = isDoom;
        this._randCard = isRand;
    }


}
