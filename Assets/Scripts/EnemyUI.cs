using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class EnemyUI : MonoBehaviour
{
    private Enemy enemy;

    [Header("Prefab Elements")]
    [SerializeField] private Image EnemyImage;
    /*
    //Do later
    [SerializeField] private Image TypeIcon;
    [SerializeField] private Image BossIcon;

    [SerializeField] private Sprite IsBossIcon;
    [SerializeField] private Sprite NoBossIcon;

    [SerializeField] private Sprite OfficeWorkerIcon;
    [SerializeField] private Sprite KidIcon;
    [SerializeField] private Sprite RichManIcon;
    [SerializeField] private Sprite VeganIcon;

    */

    [SerializeField] private TextMeshProUGUI EnemyName;
    [SerializeField] public TextMeshProUGUI Satisfaction;
    [SerializeField] private TextMeshProUGUI MaxSatisfaction;
    [SerializeField] private TextMeshProUGUI Description;



    #region Methods

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        SetEnemyUI();
    }

    //calls Awake every time the inspector/editor gets refreshed
    //- lets you see changes also in editor no need to start game
    private void OnValidate()
    {
        Awake();
    }

    public void SetEnemyUI()
    {
        if (enemy != null && enemy.EnemyData != null)
        {
            SetEnemyTexts();
            SetEnemyImage();
            /*
            //Do Later
            SetBossIcon();
            SetEnemyIcon();
            */
        }
    }

    private void SetEnemyTexts()
    {

        EnemyName.text = enemy.EnemyData.EnemyName;
        Description.text = enemy.EnemyData.Description;
        Satisfaction.text = enemy.EnemyData.Satisfaction.ToString();
        MaxSatisfaction.text = enemy.EnemyData.MaxSatisfaction.ToString();
    }


    private void SetEnemyImage()
    {
        EnemyImage.sprite = enemy.EnemyData.Image;
    }

    /*
    //Do Later
    private void SetBossIcon()
    {
        switch (enemy.EnemyData.IsBoss)
        {
            case true:
                BossIcon.sprite = IsBossIcon;
                break;
            case false:
                BossIcon.sprite = NoBossIcon;
                break;
        }
    }

    private void SetEnemyIcon()
    {
        switch (enemy.EnemyData.Type)
        {
            case EnemyLogic.EnemyType.OfficeWorker:
                TypeIcon.sprite = OfficeWorkerIcon;
                break;
            case EnemyLogic.EnemyType.Kid:
                TypeIcon.sprite = KidIcon;
                break;
            case EnemyLogic.EnemyType.RichMan:
                TypeIcon.sprite = RichManIcon;
                break;
            case EnemyLogic.EnemyType.Vegan:
                TypeIcon.sprite = VeganIcon;
                break;
        }
    }
    */

    #endregion
}
