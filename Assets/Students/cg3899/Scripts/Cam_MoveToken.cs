using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Cam_MoveToken : MoveTokensScript
{
    private Cam_InputManager inputManager;

    //sadge, parent exhange vectors are private :(
    private Vector3 camExchangeGridPos1;
    private Vector3 camExchangeGridPos2;

    //private GameObject exchangeToken1;
    //private GameObject exchangeToken2;

    //sadge, also privated in parent. 
    //private bool userSwap;
    
    
    //override replaces original scripts with child classes. 
    public override void Start()
    {
        gameManager = GetComponent<Cam_GameManager>();
        matchManager = GetComponent<Cam_MatchManager>();
        inputManager = GetComponent<Cam_InputManager>();
        lerpPercent = 0;
    }

    // Update is called once per frame
    public override void Update() {}

    public void FixedUpdate()
    {
        if (move)
        {
            lerpPercent += lerpSpeed;
            if (lerpPercent >= 1)
            {
                lerpPercent = 1;
            }

            if (exchangeToken1 != null)
            {
                ExchangeTokens();
            }
        }
    }

    /*public void CamSetupTokenExchange(GameObject token1, Vector2 pos1, 
                                   GameObject token2, Vector2 pos2, bool reversable)
    {
        SetupTokenMove();

        exchangeToken1 = token1;
        exchangeToken2 = token2;

        camExchangeGridPos1 = pos1;
        camExchangeGridPos2 = pos2;

        this.userSwap = reversable;
    }

    //rewriting this because I want the move counter from input manager to go down when a token is exchanged. 
    public override void ExchangeTokens()
    {
        Vector3 startPos = 
            gameManager.GetWorldPositionFromGridPosition((int)camExchangeGridPos1.x, (int)camExchangeGridPos1.y);
        Vector3 endPos =
            gameManager.GetWorldPositionFromGridPosition((int)camExchangeGridPos2.x, (int)camExchangeGridPos2.y);

        Vector3 movePos1 = SmoothLerp(startPos, endPos, lerpPercent);
        Vector3 movePos2 = SmoothLerp(endPos, startPos, lerpPercent);

        exchangeToken1.transform.position = movePos1;
        exchangeToken2.transform.position = movePos2;

        inputManager.moveCount = inputManager.moveCount - 1;

        if (lerpPercent == 1)
        {
            gameManager.gridArray[(int)camExchangeGridPos2.x, (int)camExchangeGridPos2.y] = exchangeToken1;
            gameManager.gridArray[(int)camExchangeGridPos1.x, (int)camExchangeGridPos1.y] = exchangeToken2;

            if (!matchManager.GridHasMatch() && userSwap)
            {
                CamSetupTokenExchange(exchangeToken1, camExchangeGridPos2, exchangeToken2, camExchangeGridPos1, false);
            }
            else
            {
                exchangeToken1 = null;
                exchangeToken2 = null;
                move = false;
            }
        }
    }

    //had to rewrite because private in parent, and needed acces to smoothlerp function.
    Vector3 SmoothLerp(Vector3 startPos, Vector3 endPos, float lerpPercent)
    {
        return new Vector3(
            Mathf.SmoothStep(startPos.x, endPos.x, lerpPercent),
            Mathf.SmoothStep(startPos.y, endPos.y, lerpPercent),
            Mathf.SmoothStep(startPos.z, endPos.z, lerpPercent));
    }*/
}
