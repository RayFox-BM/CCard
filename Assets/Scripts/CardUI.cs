using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

/// <summary>
/// Will update the UI-visuals of each card, depending on it's data
/// </summary>
public class CardUI : MonoBehaviour
{

    private Card card;

    [Header("Prefab Elements")] //references to objects in the card prefab
    [SerializeField] private Image cardImage;

    [SerializeField] private TextMeshProUGUI CardName;
    [SerializeField] private TextMeshProUGUI HungerValue;



    #region Methods

    private void Awake()
    {
        card = GetComponent<Card>();
        SetCardUI();
    }

    //calls Awake every time the inspector/editor gets refreshed
    //- lets you see changes also in editor no need to start game
    private void OnValidate()
    {
        Awake();
    }

    public void SetCardUI()
    {
        if (card != null && card.CardData != null)
        {
            SetCardTexts();
            SetCardImage();
        }
    }

    private void SetCardTexts()
    {

        CardName.text = card.CardData.CardName;
        HungerValue.text = card.CardData.HungerValue.ToString();
    }


    private void SetCardImage()
    {
        cardImage.sprite = card.CardData.Image;
    }

    #endregion
}
