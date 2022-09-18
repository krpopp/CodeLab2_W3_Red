using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_MoveTokenManager : MoveTokensScript
{
	new JT_GameManager gameManager;
	//MATCH-3 MOD; Vector which affects the direction tokens fall
	public Vector2 currentGravity = new Vector2(0, -1);

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

	public void SetupTokenSwap(JT_Token token1, Vector2 pos1,
								JT_Token token2, Vector2 pos2, bool reversable) {
		SetupTokenMove();

		swapToken1 = token1;
		swapToken2 = token2;

		this.userSwap = reversable;
	}

	public override void ExchangeTokens(){

		Vector3 startPos = gameManager.GetWorldPositionFromGridPosition((int)swapToken1.coord.x, (int)swapToken1.coord.y);
		Vector3 endPos = gameManager.GetWorldPositionFromGridPosition((int)swapToken2.coord.x, (int)swapToken2.coord.y);

		Vector3 movePos1 = SmoothLerp(startPos, endPos, lerpPercent);
		Vector3 movePos2 = SmoothLerp(endPos, startPos, lerpPercent);

		swapToken1.transform.position = movePos1;
		swapToken2.transform.position = movePos2;

		if(lerpPercent == 1){
			gameManager.tokenArray[(int)swapToken1.coord.x, (int)swapToken1.coord.y] = swapToken2;
			gameManager.tokenArray[(int)swapToken2.coord.x, (int)swapToken2.coord.y] = swapToken1;
			
			Vector2 tempCoord = swapToken1.coord;
			swapToken1.coord = swapToken2.coord;
			swapToken2.coord = tempCoord;

			if(!matchManager.GridHasMatch() && userSwap){
				SetupTokenSwap(swapToken1, new Vector2(swapToken2.coord.x, swapToken2.coord.y),
								swapToken2, new Vector2(swapToken1.coord.x, swapToken1.coord.y), false);
			} else {
				swapToken1 = null;
				swapToken2 = null;
				move = false;
			}
		}
	}

    public override bool MoveTokensToFillEmptySpaces() {
		bool movedToken = false;
		JT_Token token = null;

		if (currentGravity.y != 0) { 
			for (int x = 0; x < gameManager.gridWidth; x++) { 
				//tokens fall upwards
				if (currentGravity.y > 0) { 
					for (int y = gameManager.gridHeight - 2; y > -1; y--) { 
						if (gameManager.tokenArray[x + (int)currentGravity.x, y + (int)currentGravity.y] == null) {
							for(int pos = y; pos > -1; pos--) {
								token = gameManager.tokenArray[x, pos];
								if(token != null){
									MoveTokenToEmptyPos(x, pos, x, pos + (int)currentGravity.y, token.gameObject);
									movedToken = true;
								}
							}
							break;
						}
					}
				//tokens fall downwards
				} else {
					for (int y = 1; y < gameManager.gridHeight; y++) {
						if (gameManager.tokenArray[x + (int)currentGravity.x, y + (int)currentGravity.y] == null) {
							for(int pos = y; pos < gameManager.gridHeight; pos++) {
								token = gameManager.tokenArray[x, pos];
								if(token != null){
									MoveTokenToEmptyPos(x, pos, x, pos + (int)currentGravity.y, token.gameObject);
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
						if (gameManager.tokenArray[x + (int)currentGravity.x, y + (int)currentGravity.y] == null) {
							for(int pos = x; pos > -1; pos--) {
								token = gameManager.tokenArray[pos, y];
								if(token != null){
									MoveTokenToEmptyPos(pos, y, pos + (int)currentGravity.x, y, token.gameObject);
									movedToken = true;
								}
							}
							break;
						}
					}
				//tokens fall to the left
				} else {
					for (int x = 1; x < gameManager.gridWidth; x++) {
						if (gameManager.tokenArray[x + (int)currentGravity.x, y + (int)currentGravity.y] == null) {
							for(int pos = x; pos < gameManager.gridWidth; pos++) {
								token = gameManager.tokenArray[pos, y];
								if(token != null){
									MoveTokenToEmptyPos(pos, y, pos + (int)currentGravity.x, y, token.gameObject);
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

	public override void MoveTokenToEmptyPos(int startGridX, int startGridY,
											int endGridX, int endGridY,
											GameObject token) {

		Vector3 startPos = gameManager.GetWorldPositionFromGridPosition(startGridX, startGridY);
		Vector3 endPos = gameManager.GetWorldPositionFromGridPosition(endGridX, endGridY);

		Vector3 pos = Vector3.Lerp(startPos, endPos, lerpPercent);

		token.transform.position = pos;

		if (lerpPercent == 1)
		{
			gameManager.tokenArray[endGridX, endGridY] = token.GetComponent<JT_Token>();
			token.GetComponent<JT_Token>().coord = new Vector2(endGridX, endGridY);
			gameManager.tokenArray[startGridX, startGridY] = null;
		}
	}

	//Making this private means I can't override any animation based functions
	private Vector3 SmoothLerp(Vector3 startPos, Vector3 endPos, float lerpPercent){
		return new Vector3(
			Mathf.SmoothStep(startPos.x, endPos.x, lerpPercent),
			Mathf.SmoothStep(startPos.y, endPos.y, lerpPercent),
			Mathf.SmoothStep(startPos.z, endPos.z, lerpPercent));
	}
}
