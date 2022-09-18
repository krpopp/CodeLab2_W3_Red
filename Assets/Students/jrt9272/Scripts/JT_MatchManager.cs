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
				match = match || CheckForMatch(x, y);
				if (match) break;
			}
			if (match) break;
		}

		return match;
    }

	public bool CheckForMatch(int x, int y) {
		//Check for horz matches
		if (x < gameManager.gridWidth - 2) {
			JT_Token token1 = gameManager.tokenArray[x, y];
			JT_Token token2 = gameManager.tokenArray[x + 1, y];
			JT_Token token3 = gameManager.tokenArray[x + 2, y];

			if (token1 != null && token2 != null && token3 != null)
				return (token1.color == token2.color && token2.color == token3.color);
			else return false;
		//Check for vert matches
		} else if (y < gameManager.gridWidth - 2) {
			JT_Token token1 = gameManager.tokenArray[x, y];
			JT_Token token2 = gameManager.tokenArray[x, y + 1];
			JT_Token token3 = gameManager.tokenArray[x, y + 2];
		
			if (token1 != null && token2 != null && token3 != null)
				return (token1.color == token2.color && token2.color == token3.color);
			else return false;
		} else return false;
	}

	public List<JT_Token> GetMatchedTokens() {
		List<JT_Token> tokens = new List<JT_Token>();

		for(int x = 0; x < gameManager.gridWidth; x++) {
			for(int y = 0; y < gameManager.gridHeight ; y++) {
				if(x < gameManager.gridWidth - 2 || y < gameManager.gridHeight - 2) {
					Vector2 matchLength = GetMatchLength(x, y);
					if(matchLength.x > 2) {
						for(int i = x; i < x + matchLength.x; i++){
							tokens.Add(gameManager.tokenArray[i, y]); 						
						}
					}
					if(matchLength.y > 2) {
						for(int i = x; i < x + matchLength.x; i++){
							tokens.Add(gameManager.tokenArray[x, i]); 						
						}
					}
				}
			}
		}
		return tokens;
	}

	public Vector2 GetMatchLength(int x, int y) {
		int xMatch = 0;
		int yMatch = 0;
		JT_Token xNext = null;
		JT_Token yNext = null;
		if (gameManager.tokenArray[x,y] != null) { 
			for (int i = 1; i < gameManager.gridWidth - x; i++) {
				xNext = gameManager.tokenArray[x + i, y];				
				if (xNext != null && gameManager.tokenArray[x, y].color == xNext.color)
					xMatch++;				
			}
			for (int i = 1; i < gameManager.gridHeight - y; i++) {
				yNext = gameManager.tokenArray[x, y + i];
				if (yNext != null && gameManager.tokenArray[x, y].color == yNext.color)
					yMatch++;
			}		
		}
		return new Vector2(xMatch, yMatch);
	}


    public override int RemoveMatches() {
        int numRemoved = 0;
		matchedTokens = GetMatchedTokens();
		foreach(JT_Token token in matchedTokens) {
			Debug.Log("(" + token.coord.x + ", " + token.coord.y + ")");
		}
		//BUG FIX
		foreach(JT_Token token in matchedTokens) {
			if (gameManager.tokenArray[(int)token.coord.x, (int)token.coord.y] != null) { 
				
				Destroy(gameManager.tokenArray[(int)token.coord.x, (int)token.coord.y].gameObject);
				gameManager.tokenArray[(int)token.coord.x, (int)token.coord.y] = null;
				numRemoved++;
			}
		}
		return numRemoved;
    }

}
