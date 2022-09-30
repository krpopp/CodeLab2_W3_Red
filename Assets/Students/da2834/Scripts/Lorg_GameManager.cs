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
		Debug.Log(tokenTypes.Length);
		gridArray = new GameObject[gridWidth, gridHeight];
		MakeGrid();
		
		matchManager = GetComponent<Lorg_MatchManager>();
		inputManager = GetComponent<Lorg_InputManager>();
		repopulateManager = GetComponent<Lorg_RepopulateScript>();
		moveTokenManager = GetComponent<Lorg_MoveTokenScript>();
	}

	public override void Update(){
	if(!GridHasEmpty()){
		if(matchManager.GridHasMatch()){
			matchManager.RemoveMatches();
		} else {
			inputManager.SelectToken();
		}
	} else {
			if(!moveTokenManager.move){
			moveTokenManager.SetupTokenMove();
			}
			if(!moveTokenManager.MoveTokensToFillEmptySpaces()){
				repopulateManager.AddNewTokensToRepopulateGrid();
			}
		}
	}

	public void TextUpdate()
	{
		scoreText.text = score.ToString();
	}

	public void AddSpecialTokenToPosInGrid(int x, int y, GameObject parent, bool isHorizontal)
	{
		Vector3 position = GetWorldPositionFromGridPosition(x, y);
		GameObject token = 
			Instantiate(tokenTypes[Random.Range(0, tokenTypes.Length)], 
			            position, 
			            Quaternion.identity) as GameObject;
		token.transform.parent = parent.transform;
		token.transform.tag = "Special";
		GameObject arrow = Instantiate(specialTokenPrefab, token.transform);
		//arrow.transform.parent = token.transform;
		if (isHorizontal)
		{
			Quaternion rotation = Quaternion.Euler(0, 0, 90);
			arrow.transform.rotation = rotation;
		}
		if (gridArray[x,y]!= null)
		{
			Destroy(gridArray[x,y]);
			gridArray[x,y] = null;
		}
		gridArray[x, y] = token;
		Debug.Log("X pos: " + x + " Y pos: " + y);
		Debug.Log(token.name);
		Debug.Log(arrow.transform.position);
	}

	public bool GridHasSpecial()
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
