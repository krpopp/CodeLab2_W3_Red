using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_GameManager : GameManagerScript
{
	public JT_Token[,] tokenArray;

	[SerializeField] GameObject[] tokenPrefabs; 
	public bool fullGrid = false; 

	public override void Start () {
		matchManager = GetComponent<JT_MatchManager>();
		inputManager = GetComponent<JT_InputManager>();
		moveTokenManager = GetComponent<JT_MoveTokenManager>();
		repopulateManager = GetComponent<JT_RepopulateManager>();

		tokenArray = new JT_Token[gridWidth, gridHeight];
		MakeGrid();


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
				if(tokenArray[x, y] == null){
					empty = true;
				}
			}
		}		
		return empty;
	}

	public void _AddTokenToPosInGrid(int x, int y, GameObject parent){
		Vector3 position = GetWorldPositionFromGridPosition(x, y);
		JT_Token token = 
			(Instantiate(tokenPrefabs[Random.Range(0, tokenPrefabs.Length)], 
			            position, 
			            Quaternion.identity) as GameObject).GetComponent<JT_Token>();
		token.transform.parent = parent.transform;
		tokenArray[x, y] = token;
		token.coord = new Vector2 (x,y);
	}
}
