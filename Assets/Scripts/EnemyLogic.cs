using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData")] 
public class EnemyLogic : ScriptableObject
{

    [field: SerializeField] public string EnemyName { get; private set; } 
    [field: SerializeField] public int Satisfaction;
    [field: SerializeField] public int MaxSatisfaction;
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public bool IsBoss { get; private set; }
    [field: SerializeField] public string Description { get; private set; } 
    [field: SerializeField] public EnemyType Type { get; private set; } 

    public enum EnemyType
    {
        OfficeWorker,
        Kid,
        RichMan,
        Vegan
    }

    public void UpdateSatisfaction(int amount)
    {
        Satisfaction = amount;
    }

                                                                         
}
