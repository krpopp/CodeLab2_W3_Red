using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mh_Input : MonoBehaviour
{
    protected mh_GameManager gameManager;
    protected mh_MoveToken moveManager;
    protected GameObject selected = null;

    public virtual void Start()
    {
        moveManager = GetComponent<mh_MoveToken>();
        gameManager = GetComponent<mh_GameManager>();
    }

    public virtual void SelectToken()
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
                }
                else
                {
                    Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
                    Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

                    if (Mathf.Abs((pos1.x - pos2.x) + (pos1.y - pos2.y)) == 1)
                    {
                        moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
                    }
                    selected = null;
                }
            }
        }

    }
}
