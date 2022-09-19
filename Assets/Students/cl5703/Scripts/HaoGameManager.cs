using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaoGameManager : GameManagerScript
{
    public GameObject smileFace;
    public GameObject smileFaceParent;

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
}
