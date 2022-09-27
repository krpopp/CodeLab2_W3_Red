using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorg_RepopulateScript : RepopulateScript
{
    new Lorg_GameManager gameManager;
	public override void Start () {
		gameManager = GetComponent<Lorg_GameManager>();
	}
}
