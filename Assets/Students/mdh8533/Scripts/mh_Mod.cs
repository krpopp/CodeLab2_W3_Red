using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class mh_Mod : MonoBehaviour
{
    GameObject current;
    GameObject target;
    GameObject temporary;

    public mh_GameManager gameManager;

    public void Shuffle(GameObject[,] gameObjects)
    {
        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                int destIndexX = Random.Range(0, gameManager.gridWidth);
                int destIndexY = Random.Range(0, gameManager.gridHeight);

                GameObject current = gameManager.gridArray[x, y];
                GameObject target = gameManager.gridArray[destIndexX, destIndexY];
                GameObject temporary = target;

                gameManager.gridArray[destIndexX, destIndexY] = current;
                gameManager.gridArray[x, y] = temporary;

            }
        }
    }
}
