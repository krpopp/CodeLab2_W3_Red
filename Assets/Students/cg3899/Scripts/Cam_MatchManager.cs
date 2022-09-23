using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Cam_MatchManager : MatchManagerScript
{
    public bool GridHasVerticalMatch(int x, int y)
    {
        GameObject token1 = gameManager.gridArray[x, y + 0];
        GameObject token2 = gameManager.gridArray[x, y + 1];
        GameObject token3 = gameManager.gridArray[x, y + 2];

        if (token1 != null && token2 != null && token3 != null)
        {
            SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
            SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
            SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

            return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
        }
        else
        {
            return false;

        }
        
    }
}
