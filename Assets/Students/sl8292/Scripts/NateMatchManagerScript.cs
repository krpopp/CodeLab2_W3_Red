using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NateMatchManagerScript : MatchManagerScript
{
    protected NateGameManager nateGameManager;
    
    public NateUIManager nateUIManager;
    // Start is called before the first frame update
    public override void Start() // extend the class this inherit
    {
        nateGameManager = GetComponent<NateGameManager>();
    }

    public override bool GridHasMatch()
    {
        bool match = false;
		
        for(int x = 0; x < nateGameManager.gridWidth; x++)
        {
            for(int y = 0; y < nateGameManager.gridHeight ; y++)
            {
                match = XCheck(x, y) || YCheck(x, y);
                // double "=" means, judge; single "=" means set value
                if (match == true)
                {
                    break;
                }
            }
            if (match == true)
            {
                break;
            }
        }
        return match;
    }
    
    public bool XCheck(int x, int y)
    {
        bool flag1 = false;
        if(x < nateGameManager.gridWidth - 1 && x > 0)
        {
            flag1 = newGridHasHorizontalMatch(x, y);
        }
        
        return flag1;
    }

    public bool YCheck(int x, int y)
    {
        bool flag2 = false;
        if (y < nateGameManager.gridHeight - 1 && y > 0)
        {
            flag2 = GridHasVerticalMatch(x, y);
        }
        
        return flag2;
    }
    
    public bool newGridHasHorizontalMatch(int x, int y)
    {
        //get token on the grid, one to its right and two to its right
        GameObject token1 = nateGameManager.gridArray[x - 1, y];
        GameObject token2 = nateGameManager.gridArray[x, y];
        GameObject token3 = nateGameManager.gridArray[x + 1, y];
		
        //check if there is token on each of these grid
        if(token1 != null && token2 != null && token3 != null)
        {
            //check if the three token share the same sprite
            SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
            SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
            SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();
			
            //if share the same sprite, then return true
            return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
        } 
        else 
        {
            //else return false
            return false;
        }
    }
    
    public bool GridHasVerticalMatch(int x, int y)
    {
        //get token on the grid, one to its right and two to its right
        GameObject token1 = nateGameManager.gridArray[x, y-1];
        GameObject token2 = nateGameManager.gridArray[x, y];
        GameObject token3 = nateGameManager.gridArray[x, y+1];
		
        //check if there is token on each of these grid
        if(token1 != null && token2 != null && token3 != null)
        {
            //check if the three token share the same sprite
            SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
            SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
            SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();
			
            //if share the same sprite, then return true
            return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
        } 
        else 
        {
            //else return false
            return false;
        }
    }
    
    public int GetHorizontalMatchLengthNate(int x, int y){
        int matchLength = 1;
		
        GameObject first = nateGameManager.gridArray[x, y];

        if(first != null){
            SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();
			
            for(int i = x + 1; i < nateGameManager.gridWidth; i++){
                GameObject other = nateGameManager.gridArray[i, y];

                if(other != null){
                    SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

                    if(sr1.sprite == sr2.sprite){
                        matchLength++;
                    } else {
                        break;
                    }
                } else {
                    break;
                }
            }
        }
		
        return matchLength;
    }
    
    public int GetVerticalMatchLength(int x, int y)
    {
        //start from a token itself
        int matchLength = 1;
		
        GameObject first = nateGameManager.gridArray[x, y];
		
        //if the grid has a token
        if(first != null)
        {
            SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();
			
            //loop all tokens on the right
            for(int i = y + 1; i < nateGameManager.gridHeight; i++)
            {
                GameObject other = nateGameManager.gridArray[x, i];

                if(other != null)
                {
                    SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

                    if(sr1.sprite == sr2.sprite)
                    {
                        //if the one on the right has the same color
                        //add the 1 to the length
                        matchLength++;
                    } 
                    else 
                    {
                        //break the loop if not in the same color
                        break;
                    }
                } 
                else 
                {
                    //break the loop if the right one is empty
                    break;
                }
            }
        }
		
        //get the length of the same color length horizontally
        return matchLength;
    }
    
    public override int RemoveMatches()
    {
        int numRemoved = 0;

        //loop through every token
        for(int x = 0; x < nateGameManager.gridWidth; x++)
        {
            for(int y = 0; y < nateGameManager.gridHeight ; y++)
            {
                //Question: Should we add a null check here?
                //if (gameManager.gridArray[x, y] == null) continue;
				
                //check how many tokens on the right have the same color with the current looping one
                int horizonMatchLength = GetHorizontalMatchLengthNate(x, y);
                int verticalMatchLength = GetVerticalMatchLength(x, y);

                //if Length is more than 2, then destroy every token horizontally  
                if(horizonMatchLength > 2)
                {
                    for(int i = x; i < x + horizonMatchLength; i++)
                    {
                        GameObject token = nateGameManager.gridArray[i, y]; 
                        
                        Destroy(token);
                        OnDestroyToken(token);
                        nateGameManager.gridArray[i, y] = null;
                        numRemoved++;
                    }
                }
                if (verticalMatchLength > 2)
                {
                    for(int i = y; i < y + verticalMatchLength; i++)
                    {
							
                        GameObject token = nateGameManager.gridArray[x, i];
                        if (token != null)
                        {
                            Destroy(token);
                            OnDestroyToken(token);
                        }
                        nateGameManager.gridArray[x, i] = null;
                        numRemoved++;
                    }
                }
            }
        }
		
        return numRemoved;
    }
    
    public void OnDestroyToken(GameObject token)
    {
        if (token.GetComponent<SpriteRenderer>().sprite.name == "Blue")
        {
            Debug.Log("Gain Mana");
            if (nateGameManager.currentMana < nateGameManager.maxMana)
            {
                nateGameManager.currentMana++;
                nateUIManager.txt_Mp.text = "Mana: " + nateGameManager.currentMana + "/" + nateGameManager.maxMana;
            }
        }
        
        if (token.GetComponent<SpriteRenderer>().sprite.name == "Yellow")
        {
            Debug.Log("Gain Exp");
            if (nateGameManager.exp < nateGameManager.maxExp - 1)
            {
                nateGameManager.exp++;
            }
            else
            {
                nateGameManager.LevelUp();
                nateUIManager.txt_Level.text = "Lv. " + nateGameManager.currentLevel;
            }
            nateUIManager.txt_Exp.text = "Exp: " + nateGameManager.exp + "/" + nateGameManager.maxExp;
        }
        
        if (token.GetComponent<SpriteRenderer>().sprite.name == "Red")
        {
            Debug.Log("Deal Damage");
            nateGameManager.enemyHealth--;
            nateUIManager.txt_EnemyHp.text = "HP: " + nateGameManager.enemyHealth;
        }
        
        if (token.GetComponent<SpriteRenderer>().sprite.name == "purple")
        {
            Debug.Log("Gain Armor");
            if (nateGameManager.currentArmor < nateGameManager.maxArmor)
            {
                nateGameManager.currentArmor++;
                nateUIManager.txt_Armor.text = "Armor: " + nateGameManager.currentArmor + "/" + nateGameManager.maxArmor;
            }
        }
        
        if (token.GetComponent<SpriteRenderer>().sprite.name == "Green")
        {
            Debug.Log("Heal");
            if (nateGameManager.currentHealth < nateGameManager.maxHealth)
            {
                nateGameManager.currentHealth++;
                nateUIManager.txt_Hp.text = "HP: " + nateGameManager.currentHealth + "/" + nateGameManager.maxHealth;
            }
        }
    }
}
