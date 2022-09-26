using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorg_MoveTokenScript : MoveTokensScript
{
    public override bool MoveTokensToFillEmptySpaces()
    {
        bool movedToken = false;

        	for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 1; y < gameManager.gridHeight ; y++){
				if(gameManager.gridArray[x, y - 1] == null){
					for(int pos = y; pos < gameManager.gridHeight; pos++){
						GameObject token = gameManager.gridArray[x, pos];
						if(token != null){
							MoveTokenToEmptyPos(x, pos, x, pos - 1, token);
							movedToken = true;
						}
					}
                    //fix movement bug to call MoveTokenToEmptyPos only once as soon as the first missing element is found
                    break; 
				}
			}
		}

		if(lerpPercent == 1){
			move = false;
		}

		return movedToken;
    }
}
