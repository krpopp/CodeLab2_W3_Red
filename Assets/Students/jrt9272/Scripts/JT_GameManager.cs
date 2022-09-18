using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_GameManager : GameManagerScript
{

	[SerializeField] new GameObject[] tokenTypes; 
	public bool fullGrid = false; 

	public override void Start () {
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
				_AddTokenToPosInGrid(x, y, grid);
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

	public void _AddTokenToPosInGrid(int x, int y, GameObject parent){
		Vector3 position = GetWorldPositionFromGridPosition(x, y);
		GameObject token = 
			Instantiate(tokenTypes[Random.Range(0, tokenTypes.Length)], 
			            position, 
			            Quaternion.identity) as GameObject;
		token.transform.parent = parent.transform;
		gridArray[x, y] = token;
	}

}
