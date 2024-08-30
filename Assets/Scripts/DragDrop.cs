using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{

    public GameObject Canvas;

    public GameObject CookZone;
    public GameObject PlayerZone;
    private bool isOverCookZone;

    public bool isDragging = false;

    private GameObject StartParent;
    private Vector2 StartPosition;
    private GameObject cookZone;



    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("MainCanvas");
        CookZone = GameObject.Find("CookZone");
        PlayerZone = GameObject.Find("PlayerZone");
    }

    private void OnCollisionEnter2D(Collision2D Collision){
        isOverCookZone = true;
        cookZone = Collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D Collision){
        isOverCookZone = false;
        cookZone = PlayerZone;
    }

    public void StartDrag(){
        isDragging = true;
        StartParent = transform.parent.gameObject;
        StartPosition = transform.position;
    }

    public void EndDrag(){
        isDragging = false;

        if(isOverCookZone){
            transform.SetParent(cookZone.transform, false);
        }else{
            transform.SetParent(PlayerZone.transform, false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if(isDragging){
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }

    }
}
