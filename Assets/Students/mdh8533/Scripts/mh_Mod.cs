using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mh_Mod : MonoBehaviour
{
    public static void Shuffle(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            //find random index
            int destIndex = Randomize(0, gameObjects.Length);
            GameObject source = gameObjects[i];
            GameObject dest = gameObjects[destIndex];

            //if not identical
            if (source != dest)
            {
                //swap position
                source.transform.position = dest.transform.position;

                gameObjects[i] = gameObjects[destIndex];
            }
        }
    }

    private static int Randomize(int v, int length)
    {
        throw new NotImplementedException();
    }
}
