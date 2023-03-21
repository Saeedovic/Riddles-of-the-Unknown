using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class XPManager : MonoBehaviour
{
    public PlayerStatsData cS;
    public TextMeshProUGUI currentXpText, requiredXpText,currentHealthText,currentStaminaText,currentHungerText,currentThristText, levelText, currentPlayerStatPointText;
    public TextMeshProUGUI maxHealthText, maxstaminaText,maxHungerText,maxThristText;

    public float currentXp, requiredXp, level, maxHealth, maxStamina, maxHunger, maxThrist, maxlevel;
  

    //public bool statHealth;


    DisplayStats stat;

    private void Awake()
    {
        if(cS.PlayerLevel == 1)
        {
            requiredXp = 100;
            cS.PlayerXP = 0;
            cS.PlayerStatPoint = 0;
            cS.PlayerHealth = 100;
            cS.PlayerStamina = 100;
            cS.PlayerHunger = 100;
            cS.PlayerThrist = 100;





            cS.MaxPlayerHealth = 100;
            cS.MaxPlayerStamina = 100;
            cS.MaxPlayerHunger = 100;
            cS.MaxPlayerThrist = 100;

        }

        //statHealth = false;
    }

    private void Start()
    {
        stat = GetComponent<DisplayStats>();
        currentXpText.text = currentXp.ToString("0");

        requiredXpText.text = requiredXp.ToString("0");

        maxHealthText.text= cS.MaxPlayerHealth.ToString("0");
        maxstaminaText.text = cS.MaxPlayerStamina.ToString("0");
        maxHungerText.text= cS.MaxPlayerHunger.ToString("0");
        maxThristText.text= cS.MaxPlayerThrist.ToString("0");




        stat.currentPlayerStatPoint = cS.PlayerStatPoint;
        currentPlayerStatPointText.text = stat.currentPlayerStatPoint.ToString("0");

        levelText.text = cS.PlayerLevel.ToString("0");

        level = cS.PlayerLevel;
        currentXp = cS.PlayerXP;


    }

    private void Update()
    {
        cS.PlayerHealth = stat.currentHealth;
        cS.PlayerStamina = stat.currentStamina;
        cS.PlayerHunger = stat.currentHunger;
        cS.PlayerThrist = stat.currentThrist;


        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
    }


    public void AddXp(int xp)
    {
        currentXp += xp * Time.deltaTime;

        while(currentXp >= requiredXp) 
        {
            currentXp = currentXp - requiredXp;
            cS.PlayerLevel++;


            //increase player stat point
            stat.currentPlayerStatPoint += 5;
            cS.PlayerStatPoint = stat.currentPlayerStatPoint;

            //Formula Round to Nearest 10th
            requiredXp = (requiredXp + 1) * cS.xpMultiplier;
            requiredXp = requiredXp / 10;
            requiredXp = Mathf.Round(requiredXp);
            requiredXp = requiredXp * 10;

            //Level increased , REWARD PLAYER WITH 10+ ON THEIR STATS
            stat.currentHealth += 10;
            stat.currentStamina += 10;

            if(cS.PlayerLevel == 2)
            {
                stat.currentHunger += 20;
                stat.currentThrist += 20;


                cS.MaxPlayerHealth += 50;
                cS.MaxPlayerStamina += 50;

                cS.MaxPlayerHunger += 50;
                cS.MaxPlayerThrist += 50;
            }
            

            stat.health.text = stat.currentHealth.ToString("0");
            stat.stamina.text = stat.currentStamina.ToString("0");
            stat.hunger.text = stat.currentHunger.ToString("0");
            stat.thrist.text= stat.currentThrist.ToString("0");

            levelText.text = cS.PlayerLevel.ToString("0");
            requiredXpText.text = requiredXp.ToString("0");

            maxHealthText.text = cS.MaxPlayerHealth.ToString("0");
            maxstaminaText.text = cS.MaxPlayerStamina.ToString("0");
            maxHungerText.text = cS.MaxPlayerHunger.ToString("0");
            maxThristText.text = cS.MaxPlayerThrist.ToString("0");

            currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");

        }

        cS.PlayerXP = currentXp;
        currentXpText.text = currentXp.ToString("0");
    }


    public void InceaseStatHealth()
    {
        Debug.Log("dog");
        stat.currentPlayerStatPoint -= 1;
        stat.currentHealth += cS.healthStatPointInc;
        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
        currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");
    }


    public void InceaseStatStamina()
    {
        stat.currentPlayerStatPoint -= 1;
        stat.currentStamina += cS.staminaStatPointInc;
        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
        currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");
    }

    public void InceaseStatHunger()
    {
        stat.currentPlayerStatPoint -= 1;
        stat.currentHunger += cS.hungerStatPointInc;
        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
        currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");
    }


    public void InceaseStatThrist()
    {
        stat.currentPlayerStatPoint -= 1;
        stat.currentThrist += cS.thristStatPointInc;
        cS.PlayerStatPoint = stat.currentPlayerStatPoint;
        currentPlayerStatPointText.text = cS.PlayerStatPoint.ToString("0");
    }




}
