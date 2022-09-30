using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lorg_GameManager : GameManagerScript
{
	public GameObject specialTokenPrefab; 
	[SerializeField] TMP_Text scoreText;
	private float score; // protected score value that can only be accessed by this script
	public float Score   // the actual variable that we're accessing
	{
		get { 
			return score; }
		set { 
			score = value; //as soon as the Score is getting changed we update the private variable AND
			scoreText.text = score.ToString(); //we update it's visual text! when it's getting set! I'm so happy I remembered you can do that:)
			} 
	}
	public override void Start () {
		tokenTypes = (Object[])Resources.LoadAll("_Core/Tokens/");
		gridArray = new GameObject[gridWidth, gridHeight];
		MakeGrid();
		
		matchManager = GetComponent<Lorg_MatchManager>(); //resetting the variable as my scripts 26`29
		inputManager = GetComponent<Lorg_InputManager>();
		repopulateManager = GetComponent<Lorg_RepopulateScript>();
		moveTokenManager = GetComponent<Lorg_MoveTokenScript>();
	}

	public void AddSpecialTokenToPosInGrid(int x, int y, GameObject parent, bool isHorizontal)
	{	//this function doesn't work the way I need it to work, but I left it here to at least show that I tried ;w;
		Vector3 position = GetWorldPositionFromGridPosition(x, y);
		GameObject token = 
			Instantiate(tokenTypes[Random.Range(0, tokenTypes.Length)], 
			            position, 
			            Quaternion.identity) as GameObject;
		token.transform.parent = parent.transform; //up until this point it's basically AddTokenToPosInGrid() !
		token.transform.tag = "Special"; //but here we add a tag to it
		GameObject arrow = Instantiate(specialTokenPrefab, token.transform); //and instantiate a child for it
		if (isHorizontal)
		{
			Quaternion rotation = Quaternion.Euler(0, 0, 90); //rotate it if it's supposed to be a horizontal token
			arrow.transform.rotation = rotation;
		}
		if (gridArray[x,y]!= null) //if the grid position is not null yet (basically match manager didn't finish working)
		{
			Destroy(gridArray[x,y]); //we destroy it and empty it for the new token?
			gridArray[x,y] = null;
		}
		gridArray[x, y] = token; //and place the token in this specific location
		// Debug.Log("X pos: " + x + " Y pos: " + y);
		// Debug.Log(token.name);
		// Debug.Log(arrow.transform.position);
	}

	public bool GridHasSpecial() //this is a function that checks if we have any special tokens, but I don't know how to use it yet
	{
		for(int x = 0; x < gridWidth; x++){
		for(int y = 0; y < gridHeight ; y++){
		if(gridArray[x, y] != null && gridArray[x, y].transform.tag == "Special"){
					Debug.Log(gridArray[x, y].transform.tag); return true; 
				}
			}
		}
		return false;
	}
}
