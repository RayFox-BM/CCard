using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint; 
     public string enemyDataPath = "EnemyData/";

    /*
    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is called every time a scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGameScene")
        {
            SpawnEnemy();
        }
    }
    */

    public EnemyLogic SetEnemyData(string enemyTypeName)
    {
        
        string fullPath = enemyDataPath + enemyTypeName;
        EnemyLogic enemyData = Resources.Load<EnemyLogic>(fullPath);

        if (enemyData == null)
        {
            Debug.LogError("EnemyLogic asset not found at path: " + fullPath);
            return null;
        }else{
            return enemyData;
        }
    }

    // Method to spawn an enemy
    public Enemy SpawnEnemy(string enemyTypeName)
    {
        EnemyLogic enemyData = SetEnemyData(enemyTypeName);
        
        if (enemyPrefab != null && spawnPoint != null && enemyData != null)
        {
            // Instantiate the enemy at the spawn point
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Get the Enemy component from the instantiated prefab
            Enemy enemyComponent = newEnemy.GetComponent<Enemy>();

            if (enemyComponent != null)
            {
                

                EnemyLogic clonedEnemyData = Instantiate(enemyData);

                // Set up the enemy with the cloned data
                enemyComponent.SetUp(clonedEnemyData);

                return enemyComponent;
            }
            else
            {
                Debug.LogError("The instantiated prefab does not have an Enemy component attached.");
                return null;
            }
        }
        else
        {
            Debug.LogError("Missing references for spawning the enemy. Please assign all fields.");
            return null;
        }
    }
}
