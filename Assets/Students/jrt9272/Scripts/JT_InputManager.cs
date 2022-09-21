using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_InputManager : InputManagerScript
{
	new JT_GameManager gameManager;
	new JT_MoveTokenManager moveManager;
	JT_Token selectedToken;

	public override void Start() {
		moveManager = GetComponent<JT_MoveTokenManager>();
		gameManager = GetComponent<JT_GameManager>();
	}

    public override void SelectToken()
    {
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Collider2D collider = Physics2D.OverlapPoint(mousePos);

			if (collider != null)
			{
				if (selectedToken == null && collider.gameObject.GetComponent<JT_Token>()) {
					selectedToken = collider.gameObject.GetComponent<JT_Token>();
				}
				else if (collider.gameObject.GetComponent<JT_Token>()) {
					JT_Token swapToken = collider.gameObject.GetComponent<JT_Token>();

					//BUG FIX
					if (Mathf.Abs(selectedToken.coord.x - swapToken.coord.x) + 
						Mathf.Abs(selectedToken.coord.y - swapToken.coord.y) == 1) {
						moveManager.SetupTokenSwap(selectedToken, new Vector2 (swapToken.coord.x, swapToken.coord.y),
													swapToken, new Vector2 (selectedToken.coord.x, selectedToken.coord.y), true);
					}
					selectedToken = null;
				}
			}
		}
	}
}
