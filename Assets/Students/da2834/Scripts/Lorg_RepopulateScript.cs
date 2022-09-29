using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorg_RepopulateScript : RepopulateScript
{
	public override void Start () {
		gameManager = GetComponent<Lorg_GameManager>();
	}
}
