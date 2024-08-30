using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyUI))]
public class Enemy : MonoBehaviour
{

    [field: SerializeField] public EnemyLogic EnemyData { get; private set; }

    public void SetUp(EnemyLogic data){
        EnemyData = data;
        GetComponent<EnemyUI>().SetEnemyUI();
    }

}
