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

    public override void Start()
    {
        base.Start();
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(GetWorldPositionFromGridPosition(x, y).x, GetWorldPositionFromGridPosition(x, y).y,-5);
                GameObject faces  =
            Instantiate(smileFace,
                        position,
                        Quaternion.identity) as GameObject;
                faces.transform.parent = smileFaceParent.transform;

            }
        }
    }

    public override void Update()
    {
        base.Update();

        if (!GridHasEmpty())
        {
            timerForDisplayingFaces += Time.deltaTime;
            if (timerForDisplayingFaces > 2)
            {
                smileFaceParent.SetActive(true);
            }
            
        }
        else
        {
            timerForDisplayingFaces = 0;
            smileFaceParent.SetActive(false);
        }

        scoreText.text ="Score : " + score.ToString();


     }

}
