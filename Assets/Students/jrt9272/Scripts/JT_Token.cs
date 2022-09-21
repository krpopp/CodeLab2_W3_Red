using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_Token : MonoBehaviour {

    public enum GravityShift { none, up, down, left, right };
    public GravityShift gShift;
    public enum TokenColor { red, yellow, green, blue, purple };
    public TokenColor color;
    public enum TokenGraphic { circle, arrow };
    public TokenGraphic graphic;

    public Vector2 coord;

}
