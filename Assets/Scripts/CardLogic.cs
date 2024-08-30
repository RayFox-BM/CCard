using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all data for each individual card
/// </summary>

[CreateAssetMenu(menuName = "CardData")] //lets you create a new CardData Object with the right-click menu in the editor
public class CardLogic : ScriptableObject
{
    //field: SerializeField lets you reveal properties in the inspector like they were public fields
    [field: SerializeField] public string CardName { get; private set; } 
    [field: SerializeField] public int HungerValue { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public bool Processed { get; private set; }
                                                                         
}


