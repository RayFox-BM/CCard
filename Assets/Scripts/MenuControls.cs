using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuControls : MonoBehaviour
{

    public Collection UserDeck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene(string sceneName){

        DeckManager.Instance.SelectedDeck = UserDeck;
        SceneManager.LoadScene(sceneName);
    }
}
