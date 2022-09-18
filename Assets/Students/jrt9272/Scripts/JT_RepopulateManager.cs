using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_RepopulateManager : RepopulateScript
{
	JT_GameManager jtGameManager;
	JT_MoveTokenManager moveManager;

    public override void Start () {
		jtGameManager = GetComponent<JT_GameManager>();
		moveManager = GetComponent<JT_MoveTokenManager>();
	}

	public override void AddNewTokensToRepopulateGrid(){

		if (moveManager.currentGravity.y != 0) {
			for(int x = 0; x < jtGameManager.gridWidth; x++){

				if (moveManager.currentGravity.y > 0) { 
					if (jtGameManager.tokenArray[x, 0] == null) {
						jtGameManager._AddTokenToPosInGrid(x, 0, jtGameManager.grid);
					}
				} else {
					if(jtGameManager.tokenArray[x, jtGameManager.gridHeight - 1] == null) {
						jtGameManager._AddTokenToPosInGrid(x, jtGameManager.gridHeight - 1, jtGameManager.grid);
					}
				}	
			}
		} else { 
			for (int y = 0; y < jtGameManager.gridHeight; y++) { 
				if (moveManager.currentGravity.x > 0) { 
					if (jtGameManager.tokenArray[0, y] == null) {
						jtGameManager._AddTokenToPosInGrid(0, y, jtGameManager.grid);
					}
				} else {
					if(jtGameManager.tokenArray[jtGameManager.gridWidth - 1, y] == null) {
						jtGameManager._AddTokenToPosInGrid(jtGameManager.gridWidth - 1, y, jtGameManager.grid);
					}
				}	
			}
		}
		
	}

}
