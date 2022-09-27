using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorg_GameManager : GameManagerScript
{
	new Lorg_MatchManager matchManager;
	new Lorg_InputManager inputManager;
	new Lorg_RepopulateScript repopulateManager;
	new Lorg_MoveTokenScript moveTokenManager;
	new public GameObject[,] gridArray;
	new protected Object[] tokenTypes;
	public override void Start () {
		tokenTypes = (Object[])Resources.LoadAll("_Core/Tokens/");
		Debug.Log(tokenTypes.Length);
		gridArray = new GameObject[gridWidth, gridHeight];
		MakeGrid();
		
		matchManager = GetComponent<Lorg_MatchManager>();
		inputManager = GetComponent<Lorg_InputManager>();
		repopulateManager = GetComponent<Lorg_RepopulateScript>();
		moveTokenManager = GetComponent<Lorg_MoveTokenScript>();
	}
	public override void Update(){
		base.Update();
}

    public virtual void AddSpecialToken(int x, int y, GameObject parent)
    {
		Debug.Log("specialtokenadded)");
    }
}
