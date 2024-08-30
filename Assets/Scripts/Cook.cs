using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour
{

    public GameObject PlayerZone;
    public GameObject CookZone;
    public GameObject PizzaCard;

    public Dictionary<string, int> FoodMap = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        CookZone = GameObject.Find("CookZone");

        //after creating a new prefab for card please add it here
        FoodMap["tomato"] = 0;
        FoodMap["cheese"] = 0;
        FoodMap["pork"] = 0;
        FoodMap["pizza"] = 0;
        FoodMap["wheat"] = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){

        //deletes all cards from CookZone, will be used for game core mechanics later, might add another button too
        Transform[] transforms = CookZone.GetComponentsInChildren<Transform>(includeInactive: true);
        if (transforms.Length > 0){
            foreach (Transform t in transforms){
                GameObject go = t.gameObject;
                
                //after creating a new prefab for card please add it here
                if (go.CompareTag("wheat card")){
                    FoodMap["wheat"]++;
                    Destroy(go);
                }else if (go.CompareTag("cheese card")){
                    FoodMap["cheese"]++;
                    Destroy(go);
                }else if(go.CompareTag("tomato card")){
                    FoodMap["tomato"]++;
                    Destroy(go);
                }else if(go.CompareTag("pork card")){
                    FoodMap["pork"]++;
                    Destroy(go);
                }else if(go.CompareTag("pizza card")){
                    FoodMap["pizza"]++;
                    Destroy(go);
                }
            }
        }

        //insert any food combination here, just follow the format
        if(FoodMap["wheat"] >= 1 && FoodMap["cheese"] >= 1 && FoodMap["tomato"] >= 1){
            GameObject PiCard = Instantiate(PizzaCard, new Vector2(0,0), Quaternion.identity);
            PiCard.transform.SetParent(PlayerZone.transform, false);
            FoodMap["wheat"] --;
            FoodMap["cheese"] --;
            FoodMap["tomato"] --;
        }

    }

}
