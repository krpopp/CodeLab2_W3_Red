using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Cam_MatchManager : MatchManagerScript
{
    private List<Vector2Int> dualMatchList = new List<Vector2Int>();

    private Cam_InputManager InputManager;

    public override void Start()
    {
        gameManager = GetComponent<Cam_GameManager>();
        InputManager = GetComponent<Cam_InputManager>();
    }
    
    public override bool GridHasMatch()
    {
        bool match = false;
        
        for(int x = 0; x < gameManager.gridWidth; x++){ //for each position in grid width, and for each position in grid height,
            for(int y = 0; y < gameManager.gridHeight ; y++){ //
                if(x < gameManager.gridWidth - 2){ //Checks if this x is one of the last elements in the row, since the
                    //match check needs to be performed through tthe first element of the grid row (left ot right)
                    match = match || GridHasHorizontalMatch(x, y); //checks if match is true or false based on hasMATCH function
                }

                //-----------------------------------
                if (y < gameManager.gridWidth - 2)
                { //Checks if this x is one of the last elements in the row, since the
                    //match check needs to be performed through tthe first element of the grid row (left ot right)
                    match = match || GridHasVerticalMatch(x, y); //checks if match is true or false based on hasMATCH function. 
                }
            }
        }

        return match; //returns the bool value.
    }
    
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

    public int GetVerticalMatchLength(int x, int y)
    {
        int matchLength = 1; //

        GameObject first = gameManager.gridArray[x, y];

        if (first != null)
        {
            SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();

            for (int i = y + 1; i < gameManager.gridHeight; i++)
            {
                GameObject other = gameManager.gridArray[x, i];

                if (other != null)
                {
                    SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();
                    if (sr1.sprite == sr2.sprite)
                    {
                        matchLength++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        return matchLength;
    }

    public override int RemoveMatches()
    {
        int numRemoved = 0; //declares integer value of how many tokens removed and sets to zero.

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                if (x < gameManager.gridWidth - 2)
                {
                    int horizonMatchLength = GetHorizontalMatchLength(x, y);
                    //InputManager.moveCount++; - this doesn't work because its always going through the loop and for each time it's adding to the move count. 
                    if (horizonMatchLength > 2)
                    {
                        for (int i = x; i < x + horizonMatchLength; i++)
                        {
                            GameObject token = gameManager.gridArray[i, y];
                            dualMatchList.Add(new Vector2Int(i, y));
                            //numRemoved++;
                        }
                    }
                }

                if (y < gameManager.gridHeight - 2)
                {
                    int verticalMatchLength = GetVerticalMatchLength(x, y);

                    if (verticalMatchLength > 2)
                    {
                        for (int i = y; i < y + verticalMatchLength; i++)
                        {
                            GameObject token = gameManager.gridArray[x, i];
                            dualMatchList.Add(new Vector2Int(x, i));
                            //numRemoved++;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < dualMatchList.Count; i++)
        {
            Destroy(gameManager.gridArray[dualMatchList[i].x, dualMatchList[i].y]);
            gameManager.gridArray[dualMatchList[i].x, dualMatchList[i].y] = null;
            numRemoved++; //having numRemoved in the previous forloops of remove matches is redundant, since
            //we can just have it here. 
        }
        dualMatchList.Clear();
        
        //if the number of removed tokens is greater than 4 it will add that number plus one to the move counter.
        if (numRemoved >= 4)
        {
            InputManager.moveCount = (InputManager.moveCount + (numRemoved + 1));
        }
        else //otherwise will just add a normal amount of 1 to the count. 
        {
            InputManager.moveCount++;
        }
        return numRemoved; //returns number of tokens removed. 
    }
}
