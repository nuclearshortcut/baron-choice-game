using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Card : MonoBehaviour
{

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
