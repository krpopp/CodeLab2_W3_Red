using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_Token : MonoBehaviour {

    //enum for gravity shift value
    public enum GravityShift { none, up, down, left, right };
    public GravityShift gShift;

    //enum for matching colors
    public enum TokenColor { red, yellow, green, blue, purple, orange };
    public TokenColor color;

    //enum for visuals
    public enum TokenGraphic { circle, arrow };
    public TokenGraphic graphic;
    //Vector storing position in token array
    public Vector2 coord;

    //Randomly assign direction if arrow
    void Start() { 
        if (graphic == TokenGraphic.arrow) {
            int dir = Random.Range(0, 4);
            switch (dir) {
                default:
                    break;
                case 0:
                    gShift = GravityShift.left;
                    break;
                case 1:
                    gShift = GravityShift.up;
                    gameObject.transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
                case 2:
                    gShift = GravityShift.right;
                    gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case 3:
                    gShift = GravityShift.down;
                    gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
            }
        }
    }
}
