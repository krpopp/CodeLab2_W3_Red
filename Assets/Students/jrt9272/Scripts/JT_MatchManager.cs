using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_MatchManager : MatchManagerScript
{
	new protected JT_GameManager gameManager;
	[SerializeField] List<JT_Token> matchedTokens = new List<JT_Token>();

	public override void Start() {
		gameManager = GetComponent<JT_GameManager>();
	}

    public override bool GridHasMatch()
    {
		bool match = false;
		
		for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				match = CheckForMatch(x, y) || match;
			}
		}
		Debug.Log("Matched? " + match);
		return match;
    }

	//I combined the vert + horz HasMatch and MatchLength functions
	public bool CheckForMatch(int x, int y) {
		int yMatchLength = 0;
		int xMatchLength = 0;
		//Check input token for matches above and to the right
		if (y < gameManager.gridHeight - 2) {		
			for (int i = y + 1; i < gameManager.gridHeight; i++) { 
				if (gameManager.tokenArray[x,y].color == gameManager.tokenArray[x,i].color) {
					yMatchLength = i - y + 1;
				} else break;
			}
		}
		if (x < gameManager.gridWidth - 2) { 
			for (int i = x + 1; i < gameManager.gridWidth; i++) { 
				if (gameManager.tokenArray[x,y].color == gameManager.tokenArray[i,y].color) {
					xMatchLength = i - x + 1;
				} else break;
			}
		}
		//If there is a match of 3 or more, store the tokens to be removed
		if (yMatchLength > 2) { 
			for (int i = y; i < y + yMatchLength; i++) {
				//If we haven't already stored this token
				if (!matchedTokens.Find(t => t.coord == gameManager.tokenArray[x,i].coord))
					matchedTokens.Add(gameManager.tokenArray[x,i]); //Store it
			}
		}
		if (xMatchLength > 2) { 
			for (int i = x; i < x + xMatchLength; i++) {
				//If we haven't already stored this token
				if (!matchedTokens.Find(t => t.coord == gameManager.tokenArray[i,y].coord))
					matchedTokens.Add(gameManager.tokenArray[i,y]); //Store it
			}
		}
		//If either directions had a match of 3 or more
		return (xMatchLength > 2) || (yMatchLength > 2);
	}

    public override int RemoveMatches() {
        int numRemoved = 0;
		int[] colorCount = { 0, 0, 0, 0, 0 };

		//BUG FIX
		foreach(JT_Token token in matchedTokens) {
			if (gameManager.tokenArray[(int)token.coord.x, (int)token.coord.y] != null) { 
				switch(token.color) { 
					default;
						break;
					case JT_Token.TokenColor.red:
						colorCount[0]++;
						break;
					case JT_Token.TokenColor.yellow:
						colorCount[1]++;
						break;
					case JT_Token.TokenColor.green:
						colorCount[2]++;
						break;
					case JT_Token.TokenColor.blue:
						colorCount[3]++;
						break;
					case JT_Token.TokenColor.purple:
						colorCount[4]++;
						break;
				}

				Destroy(token.gameObject);
				gameManager.tokenArray[(int)token.coord.x, (int)token.coord.y] = null;
				numRemoved++;
			}
		}
		matchedTokens = new List<JT_Token>();
		return numRemoved;
    }

}
