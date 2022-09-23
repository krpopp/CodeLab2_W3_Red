using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_GameManager : GameManagerScript
{
    public override void Start()
    {
        tokenTypes = (Object[])Resources.LoadAll("_Core/Tokens/"); //loads game token prefabs. 
        gridArray = new GameObject[gridWidth, gridHeight]; //sets dimensions of the grid array. 
        MakeGrid();
        
        //assigns appropriate scripts to their respective managers. 
        matchManager = GetComponent<Cam_MatchManager>();
        inputManager = GetComponent<Cam_InputManager>();
        repopulateManager = GetComponent<RepopulateScript>();
        moveTokenManager = GetComponent<Cam_MoveToken>();
    }

    public override void Update()
    {
        base.Update();
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
}
