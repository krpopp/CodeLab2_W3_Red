using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaoInputManagerScript : InputManagerScript
{
	    
    public override void SelectToken()
    {
		if (Input.GetMouseButtonDown(0))//if left mouse button is clicked, 
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//creates vector3 and sets it to the position in world space of the mouse. 

			Collider2D collider = Physics2D.OverlapPoint(mousePos);//checks if there is a collider where mouse is and saves that collider. 

			if (collider != null)//if there is a collider:
			{
				if (selected == null) //and selected object reference is empty
				{
					selected = collider.gameObject;//fill it with the collider at mousepoint. 
				}
				else//other wise saves position of the selected object and the collider object. 
				{
					Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
					Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

					if ((Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y)) == 1) // change the position we put the absolute number at
																														//	to fix the diagonally swapping bug
					{
						moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);//switches the two object's positions, and states that they are reversible.
					}
					selected = null;//empties selected game object. 
				}
			}
		}
	}
}
