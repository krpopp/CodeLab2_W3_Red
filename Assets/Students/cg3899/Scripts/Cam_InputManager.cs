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

    public TextMeshProUGUI moveCounter;

    public override void Start()
    {
        moveManager = GetComponent<Cam_MoveToken>();
        gameManager = GetComponent<Cam_GameManager>();

        moveCounter.text = "Moves Left: " + moveCount;

    }

    private void Update()
    {
        moveCounter.text = "Moves Left: " + moveCount;
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
