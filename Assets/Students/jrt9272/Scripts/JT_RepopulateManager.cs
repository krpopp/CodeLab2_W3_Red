using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_RepopulateManager : RepopulateScript
{
	JT_GameManager gameManager;
	JT_MoveTokenManager moveManager;

    public override void Start () {
		gameManager = GetComponent<JT_GameManager>();
		moveManager = GetComponent<JT_MoveTokenManager>();
	}

	public override void AddNewTokensToRepopulateGrid(){

		if (gameManager.currentGravity.y != 0) {
			for(int x = 0; x < gameManager.gridWidth; x++){

				if (gameManager.currentGravity.y > 0) { 
					if (gameManager.tokenArray[x, 0] == null) {
						gameManager._AddTokenToPosInGrid(x, 0, gameManager.grid);
					}
				} else {
					if(gameManager.tokenArray[x, gameManager.gridHeight - 1] == null) {
						gameManager._AddTokenToPosInGrid(x, gameManager.gridHeight - 1, gameManager.grid);
					}
				}	
			}
		} else { 
			for (int y = 0; y < gameManager.gridHeight; y++) { 
				if (gameManager.currentGravity.x > 0) { 
					if (gameManager.tokenArray[0, y] == null) {
						gameManager._AddTokenToPosInGrid(0, y, gameManager.grid);
					}
				} else {
					if(gameManager.tokenArray[gameManager.gridWidth - 1, y] == null) {
						gameManager._AddTokenToPosInGrid(gameManager.gridWidth - 1, y, gameManager.grid);
					}
				}	
			}
		}
		
	}

}
