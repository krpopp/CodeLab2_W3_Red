using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_MoveTokenManager : MoveTokensScript
{
	new JT_GameManager gameManager;
	//MATCH-3 MOD; Vector which affects the direction tokens fall
	
	// Replaces local exchange variables
	public JT_Token swapToken1, swapToken2;
	//why not protected :( 
	protected bool userSwap;

	public override void Start() {
		//Override instance of child class
		gameManager = GetComponent<JT_GameManager>();
		matchManager = GetComponent<JT_MatchManager>();
		lerpPercent = 0;
	}

	//BUG FIX; normalizing animation speeds
	public override void Update() {}
	public void FixedUpdate () {

		if(move){
			lerpPercent += lerpSpeed;

			if(lerpPercent >= 1){
				lerpPercent = 1;
			}

			if(swapToken1 != null){
				ExchangeTokens();
			}
		}
	}
	// Couldn't override and needed to pass new params
	public void SetupTokenSwap(JT_Token token1, Vector2 pos1,
								JT_Token token2, Vector2 pos2, bool reversable) {
		SetupTokenMove();
		// Store swapped tokens
		swapToken1 = token1;
		swapToken2 = token2;

		this.userSwap = reversable;
	}

	// Rewritten to use new data class otherwise all the same
	public override void ExchangeTokens(){
		// Set animated positions for swap
		Vector3 startPos = gameManager.GetWorldPositionFromGridPosition((int)swapToken1.coord.x, (int)swapToken1.coord.y);
		Vector3 endPos = gameManager.GetWorldPositionFromGridPosition((int)swapToken2.coord.x, (int)swapToken2.coord.y);

		Vector3 movePos1 = SmoothLerp(startPos, endPos, lerpPercent);
		Vector3 movePos2 = SmoothLerp(endPos, startPos, lerpPercent);

		swapToken1.transform.position = movePos1;
		swapToken2.transform.position = movePos2;

		if (lerpPercent == 1) { // Animation is complete
			// Update refs
			gameManager.tokenArray[(int)swapToken1.coord.x, (int)swapToken1.coord.y] = swapToken2;
			gameManager.tokenArray[(int)swapToken2.coord.x, (int)swapToken2.coord.y] = swapToken1;
			// Update tokens
			Vector2 tempCoord = swapToken1.coord;
			swapToken1.coord = swapToken2.coord;
			swapToken2.coord = tempCoord;
			// Move was invalid
			if(!matchManager.GridHasMatch() && userSwap){
				SetupTokenSwap(swapToken1, new Vector2(swapToken2.coord.x, swapToken2.coord.y),
								swapToken2, new Vector2(swapToken1.coord.x, swapToken1.coord.y), false);
			} else { // Zero out refs
				swapToken1 = null;
				swapToken2 = null;
				move = false;
			}
		}
	}

	// Moves token to next empty space in grid based on currentGravity
	// Surely there is a more efficent solution bc I have a lot of duplicate code, could be revised
	// Inputs for MoveTokenToEmptyPos are the same within gravity loops so I'd like to combine those at least
    public override bool MoveTokensToFillEmptySpaces() {
		bool movedToken = false;
		JT_Token token = null;

		// tokens fall up or down, loop through columns
		if (gameManager.currentGravity.y != 0) { 
			for (int x = 0; x < gameManager.gridWidth; x++) { 
				//tokens fall upwards
				if (gameManager.currentGravity.y > 0) {
					//loop through columns below top row
					for (int y = gameManager.gridHeight - 2; y > -1; y--) { 
						//find first empty space
						if (gameManager.tokenArray[x + (int)gameManager.currentGravity.x, y + (int)gameManager.currentGravity.y] == null) {
							//loop through all tokens below
							for(int pos = y; pos > -1; pos--) {
								token = gameManager.tokenArray[x, pos];
								if(token != null){ 
									// move token based on current gravity
									MoveTokenToEmptyPos(x, pos, x, pos + (int)gameManager.currentGravity.y, token.gameObject);
									movedToken = true;
								}
							}
							//BUGFIX; only call MoveToken once when first empty is found
							break;
						}
					}
				//tokens fall downwards
				} else {
					//loop through columns above bottom row
					for (int y = 1; y < gameManager.gridHeight; y++) {
						//find first empty space
						if (gameManager.tokenArray[x + (int)gameManager.currentGravity.x, y + (int)gameManager.currentGravity.y] == null) {
							//loop through all tokens above
							for(int pos = y; pos < gameManager.gridHeight; pos++) {
								token = gameManager.tokenArray[x, pos];
								if(token != null){
									//move token based on current gravity
									MoveTokenToEmptyPos(x, pos, x, pos + (int)gameManager.currentGravity.y, token.gameObject);
									movedToken = true;
								}
							}
							//BUGFIX; only call MoveToken once when first empty is found
							break;
						}
					}
				}
			}	
		// tokens fall right or left, loop through rows
		} else {
			for (int y = 0; y < gameManager.gridHeight; y++) { 
				//tokens fall to the right
				if (gameManager.currentGravity.x > 0) { 
					//loop through rows left of rightmost column
					for (int x = gameManager.gridWidth - 2; x > -1; x--) { 
						//find first empty space
						if (gameManager.tokenArray[x + (int)gameManager.currentGravity.x, y + (int)gameManager.currentGravity.y] == null) {
							//loop through all tokens to the left
							for(int pos = x; pos > -1; pos--) {
								token = gameManager.tokenArray[pos, y];
								if(token != null){
									//move token based on current gravity
									MoveTokenToEmptyPos(pos, y, pos + (int)gameManager.currentGravity.x, y, token.gameObject);
									movedToken = true;
								}
							}
							//BUGFIX; only call MoveToken once when first empty is found
							break;
						}
					}
				//tokens fall to the left
				} else {
					//loop through rows right of leftmost column
					for (int x = 1; x < gameManager.gridWidth; x++) {
						//find first empty space
						if (gameManager.tokenArray[x + (int)gameManager.currentGravity.x, y + (int)gameManager.currentGravity.y] == null) {
							//loop through all tokens to the right
							for(int pos = x; pos < gameManager.gridWidth; pos++) {
								token = gameManager.tokenArray[pos, y];
								if(token != null){
									//move token based on current gravity
									MoveTokenToEmptyPos(pos, y, pos + (int)gameManager.currentGravity.x, y, token.gameObject);
									movedToken = true;
								}
							}
							//BUGFIX; only call MoveToken once when first empty is found
							break;
						}
					}
				}
			}
		}
		//animation is complete
		if(lerpPercent == 1) {			
			move = false;
		}

		return movedToken;
	}

	// Overwritten to pass new token class to new token array
	public override void MoveTokenToEmptyPos(int startGridX, int startGridY,
											int endGridX, int endGridY,
											GameObject token) {
		//set animated positions for fall
		Vector3 startPos = gameManager.GetWorldPositionFromGridPosition(startGridX, startGridY);
		Vector3 endPos = gameManager.GetWorldPositionFromGridPosition(endGridX, endGridY);

		Vector3 pos = Vector3.Lerp(startPos, endPos, lerpPercent);

		token.transform.position = pos;

		//animation is complete
		if (lerpPercent == 1)
		{
			//Updates to token array and class
			gameManager.tokenArray[endGridX, endGridY] = token.GetComponent<JT_Token>();
			token.GetComponent<JT_Token>().coord = new Vector2(endGridX, endGridY);
			gameManager.tokenArray[startGridX, startGridY] = null;
		}
	}

	// could not be accessed in my overwritten functions :(
	private Vector3 SmoothLerp(Vector3 startPos, Vector3 endPos, float lerpPercent){
		return new Vector3(
			Mathf.SmoothStep(startPos.x, endPos.x, lerpPercent),
			Mathf.SmoothStep(startPos.y, endPos.y, lerpPercent),
			Mathf.SmoothStep(startPos.z, endPos.z, lerpPercent));
	}
}
