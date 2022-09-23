using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaoGameManager : GameManagerScript
{
    public int score;
    public GameObject smileFace;
    public GameObject smileFaceParent;
    float timerForDisplayingFaces;
    public Text scoreText;
    public GameObject win;
    public GameObject instruction;

    public override void Start()
    {
        base.Start();
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(GetWorldPositionFromGridPosition(x, y).x, GetWorldPositionFromGridPosition(x, y).y,-5);//get the position of each token but set the z value closer to the camera to block the player's view
                GameObject faces  =//instantiate emoji to the position we just got
            Instantiate(smileFace,
                        position,
                        Quaternion.identity) as GameObject;
                faces.transform.parent = smileFaceParent.transform; // put them under the same parent gameobject

            }
        }
    }

    public override void Update()
    {
        base.Update();

        if (!GridHasEmpty())//if there is no empty space, which means there is no match
        {
            timerForDisplayingFaces += Time.deltaTime; //set a timer and start it
            if (timerForDisplayingFaces > 2-score/200) // if the timer reach the time limit (the time limit will become shorter if your score is higher)
            {
                smileFaceParent.SetActive(true); //activate the emoji to block players' view
            }
            
        }
        else
        {
            timerForDisplayingFaces = 0; // set the timer to zero so the emoji will stay deactivated
            smileFaceParent.SetActive(false);
        }

        scoreText.text ="Score : " + score.ToString(); //display the score

        if(score > 200) //you win if you reach 200 points
        {
            win.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //press space to hide the instruction and start playing
        {
            instruction.SetActive(false);
        }

        if (instruction.activeSelf) //if players are still reading the instruction
        {
            score = 0; // set score to zero
            timerForDisplayingFaces = 0;//set timer to zero
        }

     }

}
