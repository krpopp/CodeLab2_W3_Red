using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class mh_MatchMod
{
    private static GameManagerScript gameManager;
    private static int x, y;
    private static GameObject[,] gridArray;

    public static bool GridHasMatch(this MatchManagerScript matchManagerScript)
    {
        bool match = false;

        Debug.Log("cool");

        //for (int x = 0; x < gameManager.gridWidth; x++)
        //{
        //    for (int y = 0; y < gameManager.gridHeight; y++)
        //    {
        //        if (x < gameManager.gridWidth - 2)
        //        {
        //            match = match || GridHasHorizontalMatch(x, y);
        //        }
        //    }
        //}

        return match;
    }

    //public static bool GridHasVerticalMatch(this MatchManagerScript matchManagerScript)
    //{
    //    Debug.Log("vert match found");
    //    #region vertical matches
    //    GameObject token1 = gameManager.gridArray[x, y + 0];
    //    GameObject token2 = gameManager.gridArray[x, y + 1]; //Token one space above the original token
    //    GameObject token3 = gameManager.gridArray[x, y + 2]; //Token two spaces above the original token

    //    if (token1 != null && token2 != null && token3 != null)
    //    { //Secondary override, make sure each token is valid
    //      //Get sprite renderer
    //        SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
    //        SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
    //        SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

    //        //Compare each token's sprite to each other, if matching return true
    //        return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
    //        Debug.Log((sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite));
    //    }
    //    else
    //    { //If there was an error
    //        return false;
    //    }
    //    #endregion
    //}

    ////public static int GetHorizontalMatchLength(this MatchManagerScript
    //#region vert match length
    //public static int GetVerticalMatchLength(int x, int y)
    //{ //store matching token positions
    //    int matchLength = 1; //default value, match is at least 1 token long

    //    GameObject first = gameManager.gridArray[x, y]; //store first selected token

    //    if (first != null)
    //    { //if not null
    //        SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>(); //store component

    //        //Loop through every remaining token below the original pos, within the grid width
    //        for (int i = y + 1; i < gameManager.gridHeight; i++)
    //        {
    //            GameObject other = gameManager.gridArray[x, i]; //Store the next token above

    //            if (other != null)
    //            { //If there is a token
    //                SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>(); //Store its sprite renderer

    //                if (sr1.sprite == sr2.sprite)
    //                { //Compare sprites between the original and this token's
    //                  //If next token matches
    //                    matchLength++; //Increase match length by 1
    //                }
    //                else
    //                { //Otherwise does not match
    //                    break; //Exit the loop
    //                }
    //            }
    //            else
    //            {
    //                break;
    //            }
    //        }
    //    }
    //    Debug.Log(matchLength);
    //    return matchLength;
    //}
    //#endregion
}