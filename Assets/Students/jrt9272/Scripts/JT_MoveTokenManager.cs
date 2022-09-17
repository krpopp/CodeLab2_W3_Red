using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_MoveTokenManager : MoveTokensScript
{

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

		for(int x = 0; x < gameManager.gridWidth; x++) {
			for(int y = 1; y < gameManager.gridHeight ; y++) {
				if(gameManager.gridArray[x, y - 1] == null) {
					for(int pos = y; pos < gameManager.gridHeight; pos++) {
						GameObject token = gameManager.gridArray[x, pos];
						if(token != null){
							MoveTokenToEmptyPos(x, pos, x, pos - 1, token);
							movedToken = true;
						}
					}
					//BUG FIX
					break;
				}
			}
		}

		if(lerpPercent == 1) {
			
			move = false;
			Debug.Log(move);
		}

		return movedToken;
	}
}
