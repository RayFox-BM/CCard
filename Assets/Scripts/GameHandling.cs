using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandling : MonoBehaviour
{
    public Collection playerDeck; 
    [SerializeField] public Deck deck;
    public int DrawCounts;
    public int DrawAmount;
    public EnemySpawner enemySpawner;
    public Enemy enemy;
    public GameObject CookZone;
    public SceneSwitch sceneSwitch;
    public TextMeshProUGUI drawCountText;
    private float satisfactionMultiplier = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        CookZone = GameObject.Find("CookZone");

        satisfactionMultiplier = PlayerPrefs.GetFloat("SatisfactionMultiplier", 1.0f);

        if (DeckManager.Instance.SelectedDeck != null)
        {
            playerDeck = DeckManager.Instance.SelectedDeck;
        }
        
        deck.playerDeck = playerDeck;

        //allow n+1 DrawCounts because player enterscene they auto draw once
        if(playerDeck.CollectionName == "StarterDeck"){
            DrawCounts = 4;
            DrawAmount = 8;
        }else{
            DrawCounts = 4;
            DrawAmount = 6;
        }
        
        int randomNumber = UnityEngine.Random.Range(0, 2);

        //select enemy base on random number
        switch(randomNumber){

            case 0:
            enemy = enemySpawner.SpawnEnemy("BobTheWorker");
            break;

            case 1:
            enemy = enemySpawner.SpawnEnemy("GoldenPeter");
            break;

        }


        //used for level progression
        enemy.EnemyData.MaxSatisfaction = Mathf.RoundToInt(enemy.EnemyData.MaxSatisfaction * satisfactionMultiplier);

        StartCoroutine(DelayedDraw());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateDrawCountUI()
    {
        drawCountText.text = "Draws Left: " + DrawCounts.ToString();
        
    }

    public void Draw(){

        deck.DrawHand(DrawAmount);
        DrawCounts--;
        UpdateDrawCountUI();
        EndTurnServe();

        
    }


    private IEnumerator DelayedDraw()
    {
        yield return new WaitForEndOfFrame(); // Wait until the end of the frame
        Draw();
        UpdateDrawCountUI(); 
    }

    public void EndTurnServe(){
        int TotalValue = 0;

        Transform[] FirstTransform = CookZone.GetComponentsInChildren<Transform>(includeInactive: true);
    
        if (FirstTransform.Length > 0)
        {
            foreach (Transform t1 in FirstTransform)
            {
                GameObject go = t1.gameObject;
                Card card = go.GetComponent<Card>();
                if (card != null)
                {

                    //Rich Man base case
                    if(enemy.EnemyData.Type == EnemyLogic.EnemyType.RichMan){
                        
                        //Positive multiplier for processed food
                        if(card.CardData.Processed == true){
                            double tempValueDouble = card.CardData.HungerValue *1.2;
                            int tempValue = (int)Math.Round(tempValueDouble);
                            TotalValue += tempValue;
                        //Negative multiplier for non-processed food
                        }else{
                            double tempValueDouble = card.CardData.HungerValue *0.8;
                            int tempValue = (int)Math.Round(tempValueDouble);
                            TotalValue += tempValue;
                        }
                    }else{
                        //Office worker base case 
                        TotalValue += card.CardData.HungerValue;
                    }
                    

                    deck.DiscardCard(card);
                    Destroy(card);
                }
            }
        }

        ModifySatisfaction(enemy, TotalValue);
    }

    public void ModifySatisfaction(Enemy enemy, int amount)
    {
        int currentSatisfaction = enemy.EnemyData.Satisfaction;
        int newSatisfaction = currentSatisfaction + amount; 


        enemy.EnemyData.Satisfaction = newSatisfaction;


        enemy.GetComponent<EnemyUI>().SetEnemyUI();
        Canvas.ForceUpdateCanvases();

        //Victory Condition
        if(enemy.EnemyData.Satisfaction >= enemy.EnemyData.MaxSatisfaction){
            sceneSwitch.SwitchToVictory();
        }

        //Losing Condition
        if(DrawCounts < 0){
            if(enemy.EnemyData.Satisfaction >= enemy.EnemyData.MaxSatisfaction){
                sceneSwitch.SwitchToVictory();
            }else{
                sceneSwitch.SwitchToLose();
            }   
        } 

    }

    private void ProgressToNextLevel()
    {
        satisfactionMultiplier += 0.2f;
        PlayerPrefs.SetFloat("SatisfactionMultiplier", satisfactionMultiplier);

        sceneSwitch.SwitchToVictory();
    }

}



