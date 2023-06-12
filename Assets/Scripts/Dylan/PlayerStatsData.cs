using UnityEngine;

[CreateAssetMenu(fileName = "New Stat File", menuName = "Stat/New Player Stat or Enemy Stat")]
public class PlayerStatsData : ScriptableObject
{

    
    [TextArea(5, 20)] public new string name;
    [TextArea(10, 20)] public string description;
    #region Values And Bools

    #region Player Stats
    [Header("[Player Stats]")]
    [Space(5)]
  
  //  public float PlayerHealth = 100;
 
    public float PlayerStamina = 100;

    public float PlayerHunger = 100;

    public float PlayerThrist = 100;


    //public int PlayerMeleeAtk = 10;

    // public int PlayerRangedAtk = 10;

    public float PlayerLevel = 1;

    public float PlayerXP;

    public float PlayerStatPoint; // where to store this


    #endregion

    #region Max Player Stats
    [Header("[Max Player Stats]")]
    [Space(5)]

   // public float MaxPlayerHealth = 100;

    public float MaxPlayerStamina = 100;

    public float MaxPlayerHunger = 100;

    public float MaxPlayerThrist = 100;

    public float MaxPlayerLevel = 10;

    //public float PlayerXP;

    //public float PlayerStatPoint; // where to store this


    #endregion


    #region Recovery Rates
    [Header("[Recovery Rates]")]
    [Space(5)]
    [Range(0, 100)]
    public float staminaUpRate;

    public float healthUpRate;

    #endregion


    #region Stamina Consumption/Hunger & Thrist Drain Rates
    [Header("[Stamina Consumption/Hunger & Thrist Drain Rates]")]
    [Space(5)]
    [Range(0, 100)]
    public float sprintStamina;
    [Range(0, 100)]

    public float hungerDownRate;
    [Range(0, 100)]

    public float thristDownRate;


    #endregion

    #region Stat Point Variables - Increase
    [Header("[Stat Point Variables - Increase]")]
    [Space(5)]

    public float healthStatPointInc;

    public float staminaStatPointInc;

    public float hungerStatPointInc;

    public float thristStatPointInc;

    #endregion


    #region Multiplier
    [Header("[Multipliers]")]
    [Space(5)]

    public float xpMultiplier = 1.25f;

    #endregion


    #region Bools
    public bool death;
    #endregion

    #endregion




}