using Unity.VisualScripting;
using UnityEngine;

public class NateGameManager : GameManagerScript
{
    protected NateUIManager nateUIManager;

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

    public void LevelUp()
    {
        currentLevel++;
        exp = 0;
        switch (currentLevel)
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    public void EnemySelectAction()
    {
        ActionIndex = Random.Range(0, 3);
        switch (ActionIndex)
        {
            case 0:
                nateUIManager.txt_EnemyAction.text = "Heal " + enemyActionModifier;
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

}
