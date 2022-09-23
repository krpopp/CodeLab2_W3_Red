using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_RepopulateManager : RepopulateScript
{
	//Refs
	new JT_GameManager gameManager;

	//Override child class refs
    public override void Start () {
		gameManager = GetComponent<JT_GameManager>();
	}

	//Override so tokens are added to edges of the grid depending on currentGravity
	public override void AddNewTokensToRepopulateGrid(){
		//Row of tokens added
		if (gameManager.currentGravity.y != 0) {
			for(int x = 0; x < gameManager.gridWidth; x++){
				//Add to the bottom
				if (gameManager.currentGravity.y > 0) { 
					if (gameManager.tokenArray[x, 0] == null) {
						gameManager._AddTokenToPosInGrid(x, 0, gameManager.grid);
					}
				//Add to the top
				} else {
					if(gameManager.tokenArray[x, gameManager.gridHeight - 1] == null) {
						gameManager._AddTokenToPosInGrid(x, gameManager.gridHeight - 1, gameManager.grid);
					}
				}	
			}
		//Column of tokens added
		} else { 
			for (int y = 0; y < gameManager.gridHeight; y++) { 
				//Add to the left side
				if (gameManager.currentGravity.x > 0) { 
					if (gameManager.tokenArray[0, y] == null) {
						gameManager._AddTokenToPosInGrid(0, y, gameManager.grid);
					}
				//Add to the right side
				} else {
					if(gameManager.tokenArray[gameManager.gridWidth - 1, y] == null) {
						gameManager._AddTokenToPosInGrid(gameManager.gridWidth - 1, y, gameManager.grid);
					}
				}	
			}
		}
		
	}

}
