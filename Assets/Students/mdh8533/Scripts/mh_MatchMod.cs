using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using Random = System.Random;

public class mh_MatchMod : MatchManagerScript
{
    //private static GameManagerScript gameManagerScript;
    //private int x, y;
    //private GameObject[,] gridArray;

    //private static MatchManagerScript matchManagerScript; 

    public virtual void Start()
    {
        gameManager = GetComponent<GameManagerScript>();
        //gameManager.MakeGrid();
    }

    public override bool GridHasMatch()
    {
        base.GridHasMatch();

        bool match = false;

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                if (y < gameManager.gridHeight - 2)
                { //check tokens 2 spaces away from the top side of the grid
                    match = (match || GridHasVerticalMatch(x, y)); //check for matches in each token, unless a match has already been found
                }
            }
        }

        return match;
    }

    private bool GridHasVerticalMatch(int x, int y)
    {
        throw new NotImplementedException();
    }

    public virtual bool GridHasHorizontalMatch(int x, int y)
    {
        base.GridHasHorizontalMatch();

        Debug.Log("vert match found");
        #region vertical matches
        GameObject token1 = gameManager.gridArray[x, y + 0];
        GameObject token2 = gameManager.gridArray[x, y + 1]; //Token one space above the original token
        GameObject token3 = gameManager.gridArray[x, y + 2]; //Token two spaces above the original token

        if (token1 != null && token2 != null && token3 != null)
        { //Secondary override, make sure each token is valid
          //Get sprite renderer
            SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
            SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
            SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

            //Compare each token's sprite to each other, if matching return true
            return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
            Debug.Log((sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite));
        }
        else
        { //If there was an error
            return false;
        }
        #endregion
    }

    //public static int GetHorizontalMatchLength(this MatchManagerScript
    public virtual int GetHorizontalMatchLength(int x, int y)
    {
        base.GetHorizontalMatchLength();

        #region vert match length
        //store matching token positions
        int matchLength = 1; //default value, match is at least 1 token long

        GameObject first = gameManager.gridArray[x, y]; //store first selected token

        if (first != null)
        { //if not null
            SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>(); //store component

            //Loop through every remaining token below the original pos, within the grid width
            for (int i = y + 1; i < gameManager.gridHeight; i++)
            {
                GameObject other = gameManager.gridArray[x, i]; //Store the next token above

                if (other != null)
                { //If there is a token
                    SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>(); //Store its sprite renderer

                    if (sr1.sprite == sr2.sprite)
                    { //Compare sprites between the original and this token's
                      //If next token matches
                        matchLength++; //Increase match length by 1
                    }
                    else
                    { //Otherwise does not match
                        break; //Exit the loop
                    }
                }
                else
                {
                    break;
                }
            }
        }
        Debug.Log(matchLength);
        return matchLength;
        #endregion
    }

    public override int RemoveMatches()
    {
        int numRemoved = 0; //Default value, removed no tokens so far
        List<Vector2> tokensToBeRemoved = new List<Vector2>();

        //Loop through the grid dimensions
        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                //Don't check the two right most columns
                if (x < gameManager.gridWidth - 2)
                {
                    int horizonMatchLength = GetHorizontalMatchLength(x, y); //Calculate length of the match

                    if (horizonMatchLength > 2)
                    { //If the match length is greater than 2
                      //Loop through tokens within the bounds of the length in the row
                        for (int i = x; i < x + horizonMatchLength; i++)
                        {

                            //NEW OPERATION
                            tokensToBeRemoved.Add(new Vector2(i, y)); //store token coordinates to be removed later
                        }
                    }
                }
                if (y < gameManager.gridHeight - 2) {
                    int verticalMatchLength = GetVerticalMatchLength(x, y);

                    if (verticalMatchLength > 2) { //If the match length is greater than 2
                        //Loop through tokens within the bounds of the length in the row
                        for (int i = y; i < y + verticalMatchLength; i++) {

                            //NEW OPERATION
                            tokensToBeRemoved.Add(new Vector2(x, i)); //store token coordinates to be removed later

                        }
                    }
                }
            }
        }

        //NEW OPERATION
        foreach (Vector2 coord in tokensToBeRemoved)
        { //loop through all stored coordinates
            if (gameManager.gridArray[(int)coord.x, (int)coord.y] != null) //if not null
            {
                Destroy(gameManager.gridArray[(int)coord.x, (int)coord.y]); //Destroy token at coord

                //Update manager reference grid
                gameManager.gridArray[(int)coord.x, (int)coord.y] = null;
                numRemoved++; //Store locally how many are removed
            }
        }
        //Return number of tokens removed (unused)
        return numRemoved;
            }

    private int GetVerticalMatchLength(int x, int y)
    {
        throw new NotImplementedException();
    }

    private static void Destroy(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

    #region mod
    public static void Shuffle(GameObject[] gameObjects)
    {
        for(int i = 0; i < gameObjects.Length; i++)
        {
            //find random index
            int destIndex = Randomize(0, gameObjects.Length);
            GameObject source = gameObjects[i];
            GameObject dest = gameObjects[destIndex];

            //if not identical
            if(source != dest)
            {
                //swap position
                source.transform.position = dest.transform.position;

                gameObjects[i] = gameObjects[destIndex];
            }
        }
    }

    private static int Randomize(int v, int length)
   {
        throw new NotImplementedException();
   }
   #endregion
}