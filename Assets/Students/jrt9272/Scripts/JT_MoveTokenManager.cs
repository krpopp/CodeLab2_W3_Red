using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_MoveTokenManager : MoveTokensScript
{
	//MATCH-3 MOD; Vector which affects the direction tokens fall
	public Vector2 currentGravity = new Vector2(0, -1);

	public override void Start() {
		//Override instance of child class
		gameManager = GetComponent<JT_GameManager>();
		//Override instance of child class
		matchManager = GetComponent<JT_MatchManager>();
		lerpPercent = 0;
	}

	public override void Update() {}

	//BUG FIX; normalizing animation speeds
	public void FixedUpdate () {

		if(move){
			lerpPercent += lerpSpeed;

			if(lerpPercent >= 1){
				lerpPercent = 1;
			}

			if(exchangeToken1 != null){
				ExchangeTokens();
			}
		}
	}

    public override bool MoveTokensToFillEmptySpaces() {
		bool movedToken = false;
		GameObject token = null;

		if (currentGravity.y != 0) { 
			for (int x = 0; x < gameManager.gridWidth; x++) { 
				//tokens fall upwards
				if (currentGravity.y > 0) { 
					for (int y = gameManager.gridHeight - 2; y > -1; y--) { 
						if (gameManager.gridArray[x + (int)currentGravity.x, y + (int)currentGravity.y] == null) {
							for(int pos = y; pos > -1; pos--) {
								token = gameManager.gridArray[x, pos];
								if(token != null){
									MoveTokenToEmptyPos(x, pos, x, pos + (int)currentGravity.y, token);
									movedToken = true;
								}
							}
							break;
						}
					}
				//tokens fall downwards
				} else {
					for (int y = 1; y < gameManager.gridHeight; y++) {
						if (gameManager.gridArray[x + (int)currentGravity.x, y + (int)currentGravity.y] == null) {
							for(int pos = y; pos < gameManager.gridHeight; pos++) {
								token = gameManager.gridArray[x, pos];
								if(token != null){
									MoveTokenToEmptyPos(x, pos, x, pos + (int)currentGravity.y, token);
									movedToken = true;
								}
							}
							break;
						}
					}
				}
			}	
		} else {
			for (int y = 0; y < gameManager.gridHeight; y++) { 
				//tokens fall to the right
				if (currentGravity.x > 0) { 
					for (int x = gameManager.gridWidth - 2; x > -1; x--) { 
						if (gameManager.gridArray[x + (int)currentGravity.x, y + (int)currentGravity.y] == null) {
							for(int pos = x; pos > -1; pos--) {
								token = gameManager.gridArray[x, pos];
								if(token != null){
									MoveTokenToEmptyPos(pos, y, pos + (int)currentGravity.x, y, token);
									movedToken = true;
								}
							}
							break;
						}
					}
				//tokens fall to the left
				} else {
					for (int x = 1; x < gameManager.gridWidth; x++) {
						if (gameManager.gridArray[x + (int)currentGravity.x, y + (int)currentGravity.y] == null) {
							for(int pos = x; pos < gameManager.gridWidth; pos++) {
								token = gameManager.gridArray[x, pos];
								if(token != null){
									MoveTokenToEmptyPos(pos, y, pos + (int)currentGravity.x, y, token);
									movedToken = true;
								}
							}
							break;
						}
					}
				}
			}
		}

		if(lerpPercent == 1) {			
			move = false;
		}

		return movedToken;
	}
}
