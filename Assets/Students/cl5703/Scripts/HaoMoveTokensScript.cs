using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaoMoveTokensScript : MoveTokensScript
{

	HaoGameManager haoGameManager;

    public override void Start()
    {
        base.Start();
		haoGameManager = GetComponent<HaoGameManager>();
    }

    public override void ExchangeTokens()
    {
        base.ExchangeTokens();
		if (lerpPercent == 1) //when the swapping is done
		{
			if (!matchManager.GridHasMatch() ) //if there is no match
			{
				haoGameManager.score-=2; // reduce your point if you swapped the tokens without getting a match
			}

		}
	}


    public override bool MoveTokensToFillEmptySpaces()
	{
		bool movedToken = false;//creates a bool to signify if a token has been moved

		for (int x = 0; x < gameManager.gridWidth; x++)//for each x in the grid width, 
		{
			for (int y = 1; y < gameManager.gridHeight; y++)//and each y in the grid height, 
			{
				if (gameManager.gridArray[x, y - 1] == null)//if the grid above given position is empty
				{
					for (int pos = y; pos < gameManager.gridHeight; pos++)//for each y in grid height, 
					{
						GameObject token = gameManager.gridArray[x, pos];//adds a token object to y position until gird is filled again.
						if (token != null)//if token is not null, 
						{
							MoveTokenToEmptyPos(x, pos, x, pos - 1, token);//given the local x and pos values, and the token, calls the function
							movedToken = true;//indicates token has been moved. 
						}
					}
					break; // fix the glitching movement bug
				}
			}
		}
		//if lerp is complete
		if (lerpPercent == 1)
		{
			move = false;//stop moving. 
		}

		return movedToken;//return if the token has moved. 
	}

}
