using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_InputManager : InputManagerScript
{
	//Refs
	new JT_MoveTokenManager moveManager;
	//local saved token, replaces selected bool
	JT_Token selectedToken;

	//Override child ref
	public override void Start() {
		moveManager = GetComponent<JT_MoveTokenManager>();
	}

	//Overridden to use new token class
    public override void SelectToken() {
		//click
		if (Input.GetMouseButtonDown(0)) {
			//Get mouse pos and check for collision
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D collider = Physics2D.OverlapPoint(mousePos);

			if (collider != null) {	
				//if its our first click and a token
				if (selectedToken == null && collider.gameObject.GetComponent<JT_Token>()) {
					selectedToken = collider.gameObject.GetComponent<JT_Token>();
				}
				//if its our second click and a token
				else if (collider.gameObject.GetComponent<JT_Token>()) {
					JT_Token swapToken = collider.gameObject.GetComponent<JT_Token>();

					//BUG FIX; math syntax, calculate valid swap
					if (Mathf.Abs(selectedToken.coord.x - swapToken.coord.x) + 
						Mathf.Abs(selectedToken.coord.y - swapToken.coord.y) == 1) 
						moveManager.SetupTokenSwap(selectedToken, new Vector2 (swapToken.coord.x, swapToken.coord.y),
													swapToken, new Vector2 (selectedToken.coord.x, selectedToken.coord.y), true);					
					selectedToken = null;
				}
			}
		}
	}
}
