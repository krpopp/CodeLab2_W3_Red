using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mh_MatchManager : MonoBehaviour
{
    protected mh_GameManager gameManager;
    public mh_Mod mod;

    public virtual void Start()
    {
        gameManager = GetComponent<mh_GameManager>();
    }

    public virtual bool GridHasMatch()
    {
        bool match = false;

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                if (x < gameManager.gridWidth - 2)
                {
                    match = match || GridHasHorizontalMatch(x, y);
                }
            }
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                if (y < gameManager.gridHeight - 2)
                {
                    match = match || GridHasVerticalMatch(x, y);
                }
            }
        }

        return match;
    }

    public bool GridHasHorizontalMatch(int x, int y)
    {
        GameObject token1 = gameManager.gridArray[x + 0, y];
        GameObject token2 = gameManager.gridArray[x + 1, y];
        GameObject token3 = gameManager.gridArray[x + 2, y];

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

    #region vert matches added
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

    public virtual int RemoveVerticalMatches()
    {
        int numRemoved = 0;

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                if (y < gameManager.gridHeight - 2)
                {

                    int VerticalMatchLength = GetVerticalMatchLength(x, y);

                    if (VerticalMatchLength > 2)
                    {

                        for (int i = y; i < y + VerticalMatchLength; i++)
                        {
                            GameObject token = gameManager.gridArray[x, i];
                            Destroy(token);

                            gameManager.gridArray[x, i] = null;
                            numRemoved++;
                        }
                    }
                }
            }
        }

        return numRemoved;
    }

    public int GetVerticalMatchLength(int x, int y)
    {
        int VerticalMatchLength = 1;

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
                        VerticalMatchLength++;
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

        return VerticalMatchLength;
    }
    #endregion

    public int GetHorizontalMatchLength(int x, int y)
    {
        int matchLength = 1;

        GameObject first = gameManager.gridArray[x, y];

        if (first != null)
        {
            SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();

            for (int i = x + 1; i < gameManager.gridWidth; i++)
            {
                GameObject other = gameManager.gridArray[i, y];

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

    public virtual int RemoveMatches()
    {
        int numRemoved = 0;

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                if (x < gameManager.gridWidth - 2)
                {

                    int horizonMatchLength = GetHorizontalMatchLength(x, y);

                    if (horizonMatchLength > 2)
                    {

                        for (int i = x; i < x + horizonMatchLength; i++)
                        {
                            GameObject token = gameManager.gridArray[i, y];
                            Destroy(token);

                            gameManager.gridArray[i, y] = null;
                            numRemoved++;
                        }
                    }
                }
            }
        }

        #region 1 lines added
        RemoveVerticalMatches();
        #endregion

        return numRemoved;

    }

    internal void GridHasHorizontalMatch()
    {
        throw new NotImplementedException();
    }

    internal void GetHorizontalMatchLength()
    {
        throw new NotImplementedException();
    }

    internal void MakeGrid()
    {
        throw new NotImplementedException();
    }
}
