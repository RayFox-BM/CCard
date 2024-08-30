using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a card deck and also governs the discard pile and works in concordance with the Hand script
/// Singleton
/// </summary>
public class Deck : MonoBehaviour
{

    public Transform PlayerZone;
    public GameObject CookZone;
    public CardLogic MeatballCardData;
    public CardLogic PizzaCardData;
    public CardLogic DoughCardData;
    public CardLogic DumplingCardData;
    public CardLogic BreadCardData;
    public CardLogic VegDumplingCardData;
    public CardLogic BeefCardData;
    public CardLogic PorkCardData;
    public CardLogic ChickenCardData;
    public CardLogic SausageCardData;
    public CardLogic HotDogCardData;
    public CardLogic NoodleCardData;
    public CardLogic DumplingNoodleCardData;

    public static Deck Instance { get; private set; } 

    public string DeckName;

    [SerializeField] public Collection playerDeck;
    [SerializeField] private Card cardPrefab;

    public int DeckCardNum;
    //now to represent the instantiated Cards
    private List<Card> DeckPile = new();
    private List<Card> DiscardPile = new();
    public List<Card> HandCards { get; private set; } = new();



    #region Methods

    private void Awake()
    {
        //typical singleton declaration
        if (Instance == null)
        {
            Instance = this;
 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (DeckManager.Instance != null)
        {
            playerDeck = DeckManager.Instance.SelectedDeck;
        }

        //we will instantiate the deck once, at the start of the game/level
        InstantiateDeck();
        CookZone = GameObject.Find("CookZone");

        DeckName = playerDeck.CollectionName;
        if(DeckName == "StarterDeck"){
            Debug.Log("Yes");
        }

    }

    private void InstantiateDeck()
    {
        for (int i = 0; i < playerDeck.CardsInCollection.Count; i++)
        {
            Card card = Instantiate(cardPrefab, PlayerZone.transform);
            card.SetUp(playerDeck.CardsInCollection[i]);
            DeckPile.Add(card);

            card.gameObject.SetActive(false); //we will later activate the cards when we draw them, for now we just want to build the pool
        }

        DeckCardNum = DeckPile.Count;
        ShuffleDeck();
    }

    private void ShuffleDeck()
    {
        for (int i = DeckPile.Count - 1; i > 0; i--) 
        {
            int j = Random.Range(0, i + 1);
            var temp = DeckPile[i];
            DeckPile[i] = DeckPile[j];
            DeckPile[j] = temp;
        }
    }

    public void DiscardCard(Card card)
    {
        if (HandCards.Contains(card))
        {
            HandCards.Remove(card);
            DiscardPile.Add(card);
            card.gameObject.SetActive(false);
        }
    }

    //puts amount cards in hand
    public void DrawHand(int amount)
    {

        
        //Balancing issues: will discuss later
        //deletes all cards from PlayerZone
        Transform[] tran = PlayerZone.GetComponentsInChildren<Transform>(includeInactive: true);
        if (tran.Length > 0){
            foreach (Transform tr in tran){
                GameObject go = tr.gameObject;
                Card card = go.GetComponent<Card>();
                if (card != null){
                    DiscardCard(card);
                }
            }
        }

        

        for (int i = 0; i < amount; i++)
        {
            if (DeckPile.Count <= 0)
            {
                Debug.Log("You used up all your cards");
                break;
            }

            if (DeckPile.Count > 0)
            {
                HandCards.Add(DeckPile[0]);
                DeckPile[0].gameObject.SetActive(true);
                DeckPile.RemoveAt(0);
            }
        }
    }

    public Card Detect(List<Card> l, string s){
       Card c = l.Find(food => food != null && food.CardData != null && food.CardData.CardName.Contains(s));
        return c;
    }

    public bool DetectMultiple(List<Card> l, string cardName, int Times)
    {
        int con = 0;

        for(int i=0; i<l.Count; i++){
            if(l[i].CardData.CardName == cardName){
                con++;
            }
        }
        
        if(con >= Times){
            return true;
        }else{
            return false;
        }

    }

    private void RemoveAndDestroyCard(List<Card> cardList, Card card)
    {
        if (card != null)
        {
            DiscardCard(card);
            Destroy(card.gameObject);
            cardList.Remove(card);
        }
    }

    public void GenerateCard(CardLogic cardData)
    {

        Card card = Instantiate(cardPrefab, new Vector2(0, 0), Quaternion.identity);
        card.transform.SetParent(PlayerZone, false);
        Card cardComponent = card.GetComponent<Card>();
        if (cardComponent != null)
        {
            cardComponent.SetUp(cardData);
        }
        else
        {
            Debug.LogError("Card component is missing.");
        }
        HandCards.Add(cardComponent);
    }


    public void Cook(){
        
        List<Card> CookZoneCards = new();
        
        Transform[] transforms = CookZone.GetComponentsInChildren<Transform>(includeInactive: true);
        if (transforms.Length > 0){
            foreach (Transform t in transforms){
                GameObject go = t.gameObject;
                if(go.CompareTag("card")){
                    Card CZcard = go.GetComponent<Card>();
                    if (CZcard != null && CZcard.CardData != null){
                        CookZoneCards.Add(CZcard);
                    }
                }
            }
        }


        if (CookZoneCards.Count > 0)
        {
            
            // Pizza: Priority Level 1
            Card tomatoCard = Detect(CookZoneCards, "Tomato");
            if (tomatoCard != null)
            {
                Card cheeseCard = Detect(CookZoneCards, "Cheese");
                if (cheeseCard != null)
                {
                    Card doughCard = Detect(CookZoneCards, "Dough");
                    if (doughCard != null)
                    {
                        // Remove and destroy the cards
                        RemoveAndDestroyCard(CookZoneCards, tomatoCard);
                        RemoveAndDestroyCard(CookZoneCards, cheeseCard);
                        RemoveAndDestroyCard(CookZoneCards, doughCard);

                        // Clear remaining cards
                        CookZoneCards.Clear();

                        // Instantiate new PizzaCard
                        GenerateCard(PizzaCardData);
                    }
                }
            }

            // Hot Dog: Priority Level 1
            Card sausageCard = Detect(CookZoneCards, "Sausage");
            if (tomatoCard != null)
            {
                Card breadCard = Detect(CookZoneCards, "bread");
                if (breadCard != null)
                {
                        // Remove and destroy the cards
                        RemoveAndDestroyCard(CookZoneCards, sausageCard);
                        RemoveAndDestroyCard(CookZoneCards, breadCard);

                        // Clear remaining cards
                        CookZoneCards.Clear();

                        // Instantiate new PizzaCard
                        GenerateCard(HotDogCardData);
                }
            }


            // Dumpling Noodle: Priority Level 1
            Card dumplingCard = Detect(CookZoneCards, "Dumpling");
            if (dumplingCard != null)
            {
                Card noodleCard = Detect(CookZoneCards, "Noodle");
                if (noodleCard != null)
                {
                        // Remove and destroy the cards
                        RemoveAndDestroyCard(CookZoneCards, dumplingCard);
                        RemoveAndDestroyCard(CookZoneCards, noodleCard);

                        // Clear remaining cards
                        CookZoneCards.Clear();

                        // Instantiate new PizzaCard
                        GenerateCard(DumplingNoodleCardData);
                }
            }
        



            // Meatball: Priority Level 2
            Card tomatoCard2 = Detect(CookZoneCards, "Tomato");
            if (tomatoCard2 != null)
            {
                Card porkCard = Detect(CookZoneCards, "Raw Pork");
                if (porkCard != null)
                {
                    // Remove and destroy the cards
                    RemoveAndDestroyCard(CookZoneCards, tomatoCard2);
                    RemoveAndDestroyCard(CookZoneCards, porkCard);

                    // Instantiate new MeatballCard
                    GenerateCard(MeatballCardData);
                }
            }

            //Dumpling: Priority Level 2
            Card doughCard2 = Detect(CookZoneCards, "Dough");
            if (doughCard2 != null)
            {
                Card porkCard2 = Detect(CookZoneCards, "Raw Pork");
                if (porkCard2 != null)
                {
                    // Remove and destroy the cards
                    RemoveAndDestroyCard(CookZoneCards, doughCard2);
                    RemoveAndDestroyCard(CookZoneCards, porkCard2);

                    GenerateCard(DumplingCardData);
                }
            }

            //Dumpling: Priority Level 2
            Card doughCard3 = Detect(CookZoneCards, "Dough");
            if (doughCard3 != null)
            {
                Card cabbageCard = Detect(CookZoneCards, "Cabbage");
                if (cabbageCard != null)
                {
                    // Remove and destroy the cards
                    RemoveAndDestroyCard(CookZoneCards, doughCard3);
                    RemoveAndDestroyCard(CookZoneCards, cabbageCard);

                    GenerateCard(VegDumplingCardData);
                }
            }

            //Cook Raw Meat: Use Furnace, Priority Lower than other Raw Food Recipes

            //Pork
            Card furnaceCard = Detect(CookZoneCards, "Furnace");
            if (furnaceCard != null)
            {
                Card porkCard3 = Detect(CookZoneCards, "Raw Pork");
                if (porkCard3 != null)
                {
                    // Remove and destroy the cards
                    RemoveAndDestroyCard(CookZoneCards, furnaceCard);
                    RemoveAndDestroyCard(CookZoneCards, porkCard3);

                    GenerateCard(PorkCardData);
                }
            }

            Card furnaceCard2 = Detect(CookZoneCards, "Furnace");
            if (furnaceCard2 != null)
            {
                Card beefCard = Detect(CookZoneCards, "Raw Beef");
                if (beefCard != null)
                {
                    // Remove and destroy the cards
                    RemoveAndDestroyCard(CookZoneCards, furnaceCard2);
                    RemoveAndDestroyCard(CookZoneCards, beefCard);

                    GenerateCard(BeefCardData);
                }
            }

            Card furnaceCard3 = Detect(CookZoneCards, "Furnace");
            if (furnaceCard3 != null)
            {
                Card chickenCard = Detect(CookZoneCards, "Raw Chicken");
                if (chickenCard != null)
                {
                    // Remove and destroy the cards
                    RemoveAndDestroyCard(CookZoneCards, furnaceCard3);
                    RemoveAndDestroyCard(CookZoneCards, chickenCard);

                    GenerateCard(ChickenCardData);
                }
            }

            //Process Raw Meat: Lower Priority than Furnace
            //Sausage
            if (DetectMultiple(CookZoneCards, "Raw Pork", 2))
            {
                List<Card> cardsToRemove = new List<Card>();

                foreach (Card card in CookZoneCards)
                {
                    if (card.CardData.CardName == "RawPork")
                    {
                        cardsToRemove.Add(card);
                        if (cardsToRemove.Count == 2)
                        {
                            break;
                        }
                    }
                }

                foreach (Card card in cardsToRemove)
                {
                    RemoveAndDestroyCard(CookZoneCards, card);
                }

                GenerateCard(SausageCardData);
            }

            // Processing Dough
            if (DetectMultiple(CookZoneCards, "Dough", 2))
            {
                List<Card> cardsToRemove = new List<Card>();

                foreach (Card card in CookZoneCards)
                {
                    if (card.CardData.CardName == "Dough")
                    {
                        cardsToRemove.Add(card);
                        if (cardsToRemove.Count == 2)
                        {
                            break;
                        }
                    }
                }

                foreach (Card card in cardsToRemove)
                {
                    RemoveAndDestroyCard(CookZoneCards, card);
                }

                // Instantiate new DoughCard
                GenerateCard(NoodleCardData);
            }


            // Bread: Only above dough
            if (DetectMultiple(CookZoneCards, "Wheat", 3))
            {
                List<Card> cardsToRemove = new List<Card>();

                foreach (Card card in CookZoneCards)
                {
                    if (card.CardData.CardName == "Wheat")
                    {
                        cardsToRemove.Add(card);
                        if (cardsToRemove.Count == 3)
                        {
                            break;
                        }
                    }
                }

                foreach (Card card in cardsToRemove)
                {
                    RemoveAndDestroyCard(CookZoneCards, card);
                }

                GenerateCard(BreadCardData);
            }

            // Dough: Lowest Priority Level
            if (DetectMultiple(CookZoneCards, "Wheat", 2))
            {
                List<Card> cardsToRemove = new List<Card>();

                foreach (Card card in CookZoneCards)
                {
                    if (card.CardData.CardName == "Wheat")
                    {
                        cardsToRemove.Add(card);
                        if (cardsToRemove.Count == 2)
                        {
                            break;
                        }
                    }
                }

                foreach (Card card in cardsToRemove)
                {
                    RemoveAndDestroyCard(CookZoneCards, card);
                }

                // Instantiate new DoughCard
                GenerateCard(DoughCardData);
            }
        }
        CookZoneCards.Clear();
    }
    #endregion
}




