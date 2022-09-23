using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mh_Repopulate : MonoBehaviour
{
    protected mh_GameManager gameManager;

    public virtual void Start()
    {
        gameManager = GetComponent<mh_GameManager>();
    }

    public virtual void AddNewTokensToRepopulateGrid()
    {
        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            GameObject token = gameManager.gridArray[x, gameManager.gridHeight - 1];
            if (token == null)
            {
                gameManager.AddTokenToPosInGrid(x, gameManager.gridHeight - 1, gameManager.grid);
            }
        }
    }
}
