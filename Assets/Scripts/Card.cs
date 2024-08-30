using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Defines what a card is and can be, will connect all data and behaviours
/// </summary>
[RequireComponent(typeof(CardUI))] //will automatically attack the CardUI Script to every object that is a card
public class Card : MonoBehaviour
{
    #region Fields and Properties

    [field: SerializeField] public CardLogic CardData { get; private set; }

    #endregion

    #region Methods

    public void SetUp(CardLogic data){
        CardData = data;
        GetComponent<CardUI>().SetCardUI();
    }

    #endregion


}

