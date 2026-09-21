using System;
using System.Data.Common;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class CardHolder : MonoBehaviour
{

    // Attribute Manager
    private AttributeManager _am;

    // Random Card Pool
    private RandomCardsPool _rcp;

    // Transform Positions
    [SerializeField] private Vector3 _centralPos;
    [SerializeField] private Vector3 _upPos;
    [SerializeField] private Vector3 _rightPos;
    [SerializeField] private Vector3 _downPos;

    [SerializeField] private Vector3 _leftPos;

    // Held Card
    private Card _heldCard;

    private bool _cardLeaning;

    private string activeSelection;

    private string lastSelection;

    private bool _canMoveCard;

    void Start()
    {
        _canMoveCard = true;

        _am = FindFirstObjectByType<AttributeManager>();
        _rcp = FindFirstObjectByType<RandomCardsPool>();

        if (_heldCard == null)
        {
            _heldCard = FindFirstObjectByType<Card>();
            _heldCard.transform.SetParent(transform);
            _heldCard.transform.localPosition = _centralPos;    
        }

    }

    public void OnKey(InputAction.CallbackContext context)
    {
        if (context.performed && _canMoveCard)
        {
            Debug.Log("Key Pressed: " + context.action.name);
            // If Space wasn't pressed, Lean in the direction of the selected arrow key
            if (context.action.name != "SelectSpace")
            {
                activeSelection = context.action.name;           
                StartCoroutine(Move(activeSelection));
            }
            // If Space was pressed, cancel Lean
            else if (context.action.name == "SelectSpace" && lastSelection != null)
            {
                Cancel();
            }
        }
    }

   IEnumerator Move(string selection)
    {
        _canMoveCard = false;
        
        // Lean the Card away from the center according to the selection
        if (lastSelection == null)
        {
            Debug.Log("Leaning Card");
            switch(activeSelection)
            {
                case "SelectUp":
                    yield return StartCoroutine(MoveCard(_heldCard, _upPos, 8f));
                    break;
                case "SelectRight":
                    yield return StartCoroutine(MoveCard(_heldCard, _rightPos, 8f));
                    break;
                case "SelectDown":
                    yield return StartCoroutine(MoveCard(_heldCard, _downPos, 8f));
                    break;
                case "SelectLeft":
                    yield return StartCoroutine(MoveCard(_heldCard, _leftPos, 8f));
                    break;
            }
            lastSelection = selection;
            activeSelection = null;

        }
        // Shift the leaning Card back to the center
        // then lean it away according to the selection
        else if (lastSelection != null && lastSelection != activeSelection)
        {
            Debug.Log("Repositioning Card");
            switch(activeSelection)
            {
                case "SelectUp":
                    yield return StartCoroutine(MoveCard(_heldCard, _centralPos, 8f));
                    yield return StartCoroutine(MoveCard(_heldCard, _upPos, 8f));
                    break;
                case "SelectRight":
                    yield return StartCoroutine(MoveCard(_heldCard, _centralPos, 8f));
                    yield return StartCoroutine(MoveCard(_heldCard, _rightPos, 8f));
                    break;
                case "SelectDown":
                    yield return StartCoroutine(MoveCard(_heldCard, _centralPos, 8f));
                    yield return StartCoroutine(MoveCard(_heldCard, _downPos, 8f));
                    break;
                case "SelectLeft":
                    yield return StartCoroutine(MoveCard(_heldCard, _centralPos, 8f));
                    yield return StartCoroutine(MoveCard(_heldCard, _leftPos, 8f));
                    break;
            }
            lastSelection = selection;
            activeSelection = null;
        }
        // If the current selection is same as the previous, 
        // commit to the active direction
        else if (lastSelection != null && lastSelection == activeSelection)
        {
            Debug.Log("Attempting Card Commit");
            switch(activeSelection)
            {
                case "SelectUp":
                    yield return StartCoroutine(Commit(activeSelection, _upPos, 0));
                    break;
                case "SelectRight":
                    yield return StartCoroutine(Commit(activeSelection, _rightPos, 1));
                    break;
                case "SelectDown":
                    yield return StartCoroutine(Commit(activeSelection, _downPos, 2));
                    break;
                case "SelectLeft":
                    yield return StartCoroutine(Commit(activeSelection, _leftPos, 3));
                    break;
            }
            lastSelection = null;
            activeSelection = null;
        }
        _canMoveCard = true;
    }

    IEnumerator Commit(string selection, Vector3 destination, int dir)
    {
        Debug.Log("Commiting Card: " + selection);

        // Shoot the Card in the direction its leaning
        yield return StartCoroutine(MoveCard(_heldCard, destination * 40, 16f));

        // After a few seconds, get the Card's Attribute changes and apply them to the Attributes
        _am.AlterAttributes(_heldCard, dir);
        
        // Get and save the Card's follow up, if there is one. Otherwise, pull a Card from the Random Pool
        Card _nextCard;
        if (_heldCard.dirSelecs[dir].FollowUpCard != null)
        {
            _nextCard = _heldCard.dirSelecs[dir].FollowUpCard;
        }
        else
        {
            // Rand Card Pool   
            _nextCard = _rcp.GetRandomCard();
        }
        
        // While the Held Card is off screen
        Debug.Log("Change Card");
        
        Destroy(_heldCard.gameObject);

        _heldCard = Instantiate(_nextCard);
        _heldCard.transform.SetParent(transform);
        _heldCard.transform.localPosition = _centralPos;

        lastSelection = null;
        activeSelection = null;
    }

    private void Cancel()
    {
        Debug.Log("Returning Card to Center");
        StartCoroutine(MoveCard(_heldCard, _centralPos, 8f));
        lastSelection = null;
    }

    IEnumerator MoveCard(Card cardToMove, Vector3 destination, float speed)
    {

        while (Vector3.Distance(cardToMove.transform.localPosition, destination) > 0.01)
        {
            cardToMove.transform.localPosition = Vector3.MoveTowards(cardToMove.transform.localPosition, destination, speed * Time.deltaTime);
            yield return null;
        }

        cardToMove.transform.localPosition = destination;
    }

}
