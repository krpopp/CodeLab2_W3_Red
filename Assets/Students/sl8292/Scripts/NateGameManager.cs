using Unity.VisualScripting;
using UnityEngine;

public class NateGameManager : GameManagerScript
{
    public NateUIManager nateUIManager;

    [Header("Nate Features")]
    public int currentLevel;
    public int exp;
    public int maxExp;
    
    public int currentArmor;
    public int maxArmor;
    
    public int currentHealth;
    public int maxHealth;

    public int attackModifier;

    public int currentMana;
    public int maxMana;

    public int enemyHealth;
    public int enemyActionModifier;
    private int ActionIndex;

    public override void Start()
    {
        base.Start();
        EnemySelectAction();
    }
    public void LevelUp()
    {
        currentLevel++;
        exp = 0;
        switch (currentLevel)
        {
            case 2:
                maxHealth += 10;
                attackModifier += 1;
                break;
            case 3:
                maxHealth += 20;
                maxArmor += 10;
                break;
            case 4:
                maxHealth += 30;
                maxMana += 20;
                attackModifier += 1;
                break;
            default:
                break;
        }
        nateUIManager.txt_Hp.text = "HP: " + currentHealth + "/" + maxHealth;
        nateUIManager.txt_Mp.text = "Mana: " + currentMana + "/" + maxMana;
        nateUIManager.txt_Armor.text = "Armor: " + currentArmor + "/" + maxArmor;
        nateUIManager.txt_Attack.text = "ATK: " + attackModifier;
    }

    public void EnemySelectAction()
    {
        ActionIndex = Random.Range(0, 3);
        switch (ActionIndex)
        {
            case 0:
                nateUIManager.txt_EnemyAction.text = "Heal " + enemyActionModifier / 2;
                break;
            case 1:
                nateUIManager.txt_EnemyAction.text = "Hit " + enemyActionModifier;
                break;
            case 2:
                nateUIManager.txt_EnemyAction.text = "Mana Drain " + enemyActionModifier + " then Heal " + enemyActionModifier/2;
                break;
            case 3:
                nateUIManager.txt_Level.text = "Melt Armor" + enemyActionModifier;
                break;
        }
    }
    
    public void EnemyAction()
    {
        switch (ActionIndex)
        {
            case 0:
                enemyHealth += enemyActionModifier /2;
                nateUIManager.txt_EnemyHp.text = "HP: " + enemyHealth;
                Debug.Log("enemy heal");
                break;
            case 1:
                currentHealth -= enemyActionModifier;
                nateUIManager.txt_Hp.text = "HP: " + currentHealth + "/" + maxHealth;
                Debug.Log("enemy hit");
                break;
            case 2:
                if (currentMana < enemyActionModifier)
                {
                    currentMana = 0;
                }
                else
                {
                    currentMana -= enemyActionModifier;
                    enemyHealth += enemyActionModifier / 2;
                }
                nateUIManager.txt_Mp.text = "Mana: " + currentMana + "/" + maxMana;
                nateUIManager.txt_EnemyHp.text = "HP: " + enemyHealth;
                Debug.Log("enemy mana drain");
                break;
            case 3:
                currentArmor = 0;
                nateUIManager.txt_Armor.text = "Armor: " + currentArmor + "/" + maxArmor;
                Debug.Log("enemy melt armor");
                break;
        }
        enemyActionModifier += Random.Range(0, 5);
    }

    public void Skill1()
    {
        if (currentMana >= 10)
        {
            currentMana -= 10;
        }
        currentHealth += 10;
        nateUIManager.txt_Hp.text = "HP: " + currentHealth + "/" + maxHealth;
        nateUIManager.txt_Mp.text = "Mana: " + currentMana + "/" + maxMana;
    }
    
    public void Skill2()
    {
        if (currentMana >= 10)
        {
            currentMana -= 10;
        }
        enemyHealth -= 10;
        nateUIManager.txt_EnemyHp.text = "HP: " + enemyHealth;
        nateUIManager.txt_Mp.text = "Mana: " + currentMana + "/" + maxMana;
    }

}
