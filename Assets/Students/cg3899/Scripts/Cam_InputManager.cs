using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cam_InputManager : InputManagerScript
{
    public int moveCount; //will track a limited amount of moves.
    public int timerTranstion;//after this value is reached timer will speed up. 

    public TextMeshProUGUI moveCounter;

    public override void Start()
    {
        moveManager = GetComponent<Cam_MoveToken>();
        gameManager = GetComponent<Cam_GameManager>();

        moveCounter.text = "Moves Left: " + moveCount;

        timerTranstion = 0; //this will ensure that when the game starts the player will bet about time cycles of the
                            //timer cycling before it starts to speed up. 

        Invoke("CountDown", 4);

    }

    private void Update()
    {
        moveCounter.text = "Moves Left: " + moveCount;
        
    }

    private void CountDown()
    {
        moveCount--;
        timerTranstion++;
        
        if (timerTranstion <= 10) //as long as timer transition is below 10, countdown will be called "every" 2 seconds.
        {
            Invoke("CountDown", 2);
        }
        else
        {
            Invoke("CountDown", 1);
        }
    }

    public override void SelectToken()
    {
        if (moveCount >= 0) //if player has NOT run out of moves.
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Collider2D collider = Physics2D.OverlapPoint(mousePos);

                if (collider != null)
                {
                    if (selected == null)
                    {
                        selected = collider.gameObject;
                        //moveCount = moveCount - 1; 
                    }
                    else
                    {
                        Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
                        Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

                        if (Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y) == 1)
                        {
                            moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
                            //moveCount--;
                        }
                        selected = null;
                        //moveCount = moveCount - 1;
                    }
                }
            }
        }
    }
}
