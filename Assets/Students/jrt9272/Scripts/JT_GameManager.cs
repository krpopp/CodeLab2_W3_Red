using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_GameManager : GameManagerScript
{

	public bool fullGrid = false; 

	public virtual void Start () {
		tokenTypes = new GameObject[] { (GameObject)Resources.Load("_Core/Tokens/blue"),
										(GameObject)Resources.Load("_Core/Tokens/red"),
										(GameObject)Resources.Load("_Core/Tokens/green"),
										(GameObject)Resources.Load("_Core/Tokens/yellow")};
		gridArray = new GameObject[gridWidth, gridHeight];
		MakeGrid();

		matchManager = GetComponent<JT_MatchManager>();
		inputManager = GetComponent<JT_InputManager>();
		moveTokenManager = GetComponent<JT_MoveTokenManager>();
		repopulateManager = GetComponent<JT_RepopulateManager>();
	}

	
    public override void Update() { 
		if (!GridHasEmpty()){
			if (!fullGrid) { // BUG FIX; first frame the grid is full
				moveTokenManager.move = false; // Disable move so the player can click
				fullGrid = true; // Run statement once
			}
			if (matchManager.GridHasMatch()) {
				matchManager.RemoveMatches();
			} else if (moveTokenManager.move == false) { //BUG FIX; Only allow clicks when move is false				
				inputManager.SelectToken();
			}
		} else {		
			if (!moveTokenManager.move) {
				moveTokenManager.SetupTokenMove();
			}
			if(!moveTokenManager.MoveTokensToFillEmptySpaces()) {
				repopulateManager.AddNewTokensToRepopulateGrid();
			}
			fullGrid = false; //BUG FIX, tracks the first frame the grid is full
		}
	}


	void MakeGrid() {
		grid = new GameObject("TokenGrid");
		for(int x = 0; x < gridWidth; x++){
			for(int y = 0; y < gridHeight; y++){
				AddTokenToPosInGrid(x, y, grid);
			}
		}
	}

	//Returns true when there is an empty space
	public override bool GridHasEmpty() {
		bool empty = false; 
		for(int x = 0; x < gridWidth; x++){
			for(int y = 0; y < gridHeight ; y++){
				if(gridArray[x, y] == null){
					empty = true;
				}
			}
		}		
		return empty;
	}

}
