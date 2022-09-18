using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_RepopulateManager : RepopulateScript
{

	JT_MoveTokenManager moveManager;

    public override void Start () {
		gameManager = GetComponent<JT_GameManager>();
		moveManager = GetComponent<JT_MoveTokenManager>();
	}

	public override void AddNewTokensToRepopulateGrid(){

		if (moveManager.currentGravity.y != 0) {
			for(int x = 0; x < gameManager.gridWidth; x++){

				if (moveManager.currentGravity.y > 0) { 
					if (gameManager.gridArray[x, 0] == null) {
						gameManager.AddTokenToPosInGrid(x, 0, gameManager.grid);
					}
				} else {
					if(gameManager.gridArray[x, gameManager.gridHeight - 1] == null) {
						gameManager.AddTokenToPosInGrid(x, gameManager.gridHeight - 1, gameManager.grid);
					}
				}	
			}
		}
		
	}

}
