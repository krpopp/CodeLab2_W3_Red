using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Responsible for calculating and removing matched tokens
// Also does my mod, which should really be in its own script. Changes gravity and optional dungon crawler
public class JT_MatchManager : MatchManagerScript
{
	// Refs
	new protected JT_GameManager gameManager;
	[SerializeField] JT_DungeonCrawler crawler;
	
	// New list of tokens to be removed
	[SerializeField] List<JT_Token> matchedTokens = new List<JT_Token>();

	public override void Start() {
		gameManager = GetComponent<JT_GameManager>();
	}

	// Returns true after checking all tokens for matches
    public override bool GridHasMatch() {
		bool match = false;
		// Loop through all tokens
		for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				// Not the top right four
				if (x < gameManager.gridWidth - 2 || y < gameManager.gridHeight - 2)
					match = CheckForMatch(x, y) || match; 
			}
		}
		return match;
    }

	// Returns true if the input token is part of a match, and adds matched tokens to list
	// I combined the vert + horz HasMatch and MatchLength functions, and revised how RemoveMatches now recieves a matched token list
	public bool CheckForMatch(int x, int y) {
		int yMatchLength = 0;
		int xMatchLength = 0;
		//Check input token for matches above and to the right
		if (y < gameManager.gridHeight - 2) { // Check above
			for (int i = y + 1; i < gameManager.gridHeight; i++) { 
				if (gameManager.tokenArray[x,y].color == gameManager.tokenArray[x,i].color) {
					yMatchLength = i - y + 1;
				} else break;
			}
		}
		if (x < gameManager.gridWidth - 2) {  // Check to the right
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

	// Loops through matched tokens list, calculates majority gravity, changes gravity and optionally moves in dungeon crawler
    // Returns number removed for score keeping purposes
	public override int RemoveMatches() {
        int numRemoved = 0;
		int[] gravityCount = { 0, 0, 0, 0 };

		//BUG FIX; matched tokens calculated before call to remove
		foreach(JT_Token token in matchedTokens) {
			if (gameManager.tokenArray[(int)token.coord.x, (int)token.coord.y] != null) {
				
				// Increase colorCount array by token's color
				switch(token.gShift) {
					default:
						break;
					case JT_Token.GravityShift.down:
						gravityCount[0]++;
						break;
					case JT_Token.GravityShift.up:
						gravityCount[1]++;
						break;
					case JT_Token.GravityShift.left:
						gravityCount[2]++;
						break;
					case JT_Token.GravityShift.right:
						gravityCount[3]++;
						break;
				}
				// Destroy token and ref
				Destroy(token.gameObject);
				gameManager.tokenArray[(int)token.coord.x, (int)token.coord.y] = null;
				numRemoved++;
			}
		}
		bool gravityChange = false;
		for (int i = 0; i < gravityCount.Length; i++) 
			gravityChange = gravityChange || (gravityCount[i] > 0);
        if (gravityChange) { 
			// Calculate gravity majority
			int majorityGravity = gravityCount.Max();
			int majorityIndex = gravityCount.ToList().IndexOf(majorityGravity);
			//trigger gravity change and dungeon crawler based on majority
			switch (majorityIndex) {
				default:
					break;
				case 0:
					gameManager.currentGravity = new Vector2(0, -1);
					if (crawler) crawler.Movement(-2);
					break;
				case 1:
					gameManager.currentGravity = new Vector2(0, 1);
					if (crawler) crawler.Movement(0);
					break;
				case 2:
					gameManager.currentGravity = new Vector2(-1, 0);
					if (crawler) crawler.Movement(-1);
					break;
				case 3:
					gameManager.currentGravity = new Vector2(1, 0);
					if (crawler) crawler.Movement(1);
					break;
			

			}
			gameManager.ToggleGravityArrows(majorityIndex); // Gravity visuals
		} else { // default move the crawler same as last movement
			if (crawler) { 
				if (gameManager.currentGravity.x > 0)	crawler.Movement(-1);
				if (gameManager.currentGravity.x < 0)	crawler.Movement(1);
				if (gameManager.currentGravity.y > 0)	crawler.Movement(0);
				if (gameManager.currentGravity.y < 0)	crawler.Movement(-2);
			}
		}
		// Zero out matched token list
		matchedTokens = new List<JT_Token>();
		return numRemoved; // Keep score, not used
    }

}
