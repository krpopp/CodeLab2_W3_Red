using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mh_GameManager : MonoBehaviour
{
    public int gridWidth = 8;
    public int gridHeight = 8;
    public float tokenSize = 1;

    protected mh_MatchManager matchManager;
    protected mh_Input inputManager;
    protected mh_Repopulate repopulateManager;
    protected mh_MoveToken moveTokenManager;

    public GameObject grid;
    public GameObject[,] gridArray;
    protected Object[] tokenTypes;
    GameObject selected;

    public mh_Mod mod;

    public virtual void Start()
    {
        tokenTypes = (Object[])Resources.LoadAll("_Core/Tokens/");
        gridArray = new GameObject[gridWidth, gridHeight];
        MakeGrid();
        matchManager = GetComponent<mh_MatchManager>();
        inputManager = GetComponent<mh_Input>();
        repopulateManager = GetComponent<mh_Repopulate>();
        moveTokenManager = GetComponent<mh_MoveToken>();
    }

    public virtual void Update()
    {
        if (!GridHasEmpty())
        {
            if (matchManager.GridHasMatch())
            {
                matchManager.RemoveMatches();
            }
            else
            {
                inputManager.SelectToken();
            }
        }
        else
        {
            if (!moveTokenManager.move)
            {
                moveTokenManager.SetupTokenMove();
            }
            if (!moveTokenManager.MoveTokensToFillEmptySpaces())
            {
                repopulateManager.AddNewTokensToRepopulateGrid();
            }
            else
            {
                //mod.Shuffle(gridArray);
            }
        }
    }

    void MakeGrid()
    {
        grid = new GameObject("TokenGrid");
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                AddTokenToPosInGrid(x, y, grid);
            }
        }
    }

    public virtual bool GridHasEmpty()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (gridArray[x, y] == null)
                {
                    return true;
                }
            }
        }

        return false;
    }


    public Vector2 GetPositionOfTokenInGrid(GameObject token)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (gridArray[x, y] == token)
                {
                    return (new Vector2(x, y));
                }
            }
        }
        return new Vector2();
    }

    public Vector2 GetWorldPositionFromGridPosition(int x, int y)
    {
        return new Vector2(
            (x - gridWidth / 2) * tokenSize,
            (y - gridHeight / 2) * tokenSize);
    }

    public void AddTokenToPosInGrid(int x, int y, GameObject parent)
    {
        Vector3 position = GetWorldPositionFromGridPosition(x, y);
        GameObject token =
            Instantiate(tokenTypes[Random.Range(0, tokenTypes.Length)],
                        position,
                        Quaternion.identity) as GameObject;
        token.transform.parent = parent.transform;
        gridArray[x, y] = token;
    }
}
