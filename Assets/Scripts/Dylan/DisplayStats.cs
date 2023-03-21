using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{
    public PlayerStatsData stat;

    [SerializeField] bool beginDelay;
    float delay;



    #region Text Holders
    [Header("[Health & Stamina Text Holders]")]
    [Space(5)]

    public float currentHealth;
    public float currentStamina;
    public float currentHunger;
    public float currentThrist;


    public float currentPlayerStatPoint;


    public TextMeshProUGUI health;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI hunger;
    public TextMeshProUGUI thrist;


    #endregion
    private void Awake()
    {
 
        currentHealth = stat.PlayerHealth;
        currentStamina = stat.PlayerStamina;
        currentHunger = stat.PlayerHunger;
        currentThrist = stat.PlayerThrist;


    }
    public void Start()
    {
        #region Stats Equal Max-Values
       currentStamina = 100;
       currentHealth = 100;
        currentHunger = 100;
        currentThrist = 100;


        #endregion

        #region Bool Equals-start

        stat.death = false;

        #endregion

        #region Bar Max Value-start

        health.text = currentHealth.ToString("0");
        stamina.text = currentStamina.ToString("0");
        hunger.text = currentHunger.ToString("0");
        thrist.text = currentThrist.ToString("0");


        #endregion
        UpdateUI();
    }

    public void Update()
    {



        #region If Running
        if (currentStamina > 0 && Input.GetKey("left shift"))
        {
            if (!beginDelay)
            {
                currentStamina -= stat.sprintStamina * Time.deltaTime;
                //SDebug.Log("IM SPEEEDINGNGGNGGNG");
                //GetComponent<Animator>().speed = 3;
            }
        }
        else if (currentStamina <= 100 && !beginDelay)
        {
            
                //Debug.Log("regeneration");
                //GetComponent<Animator>().speed = 1;
                currentStamina += stat.sprintStamina * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, stat.PlayerStamina);
        }
        if(currentStamina <= 0.5f)
        {
            beginDelay = true;
        }

        if(beginDelay)
        {
            delay += Time.deltaTime;
            if(delay >= 1)
            {
                beginDelay = false;
                delay = 0;
            }
        }
        #endregion

        #region If Alive
        if (currentHunger >= 0 || currentThrist >= 0)
        {
            currentHunger -= stat.hungerDownRate * Time.deltaTime;
            currentThrist -= stat.thristDownRate * Time.deltaTime;



           // if (!beginDelay)
           // {
                //currentStamina -= stat.sprintStamina * Time.deltaTime;
                //SDebug.Log("IM SPEEEDINGNGGNGGNG");
                //GetComponent<Animator>().speed = 3;
           // }
        }


        #endregion

        #region If Health Is Less Or Equal To 0 
        if (currentHealth <= 0 || currentHunger <= 0 || currentThrist <= 0)
        {
            Die();
        }
        #endregion

        UpdateUI();

    }

    public void UpdateUI()
    {

        #region Math Clamps
        currentHealth = Mathf.Clamp(currentHealth, 0, 190);
        currentStamina = Mathf.Clamp(currentStamina, 0, 190);
        currentHunger = Mathf.Clamp(currentHunger, 0, 190);
        currentThrist = Mathf.Clamp(currentThrist, 0, 190);


        #endregion

        #region Health & Stamina Values

        health.text = currentHealth.ToString("0");
        stamina.text = currentStamina.ToString("0");
        hunger.text = currentHunger.ToString("0");
        thrist.text = currentThrist.ToString("0");



        #endregion
    }

    public void GetDamaged(int amount) //take dmg and specify amt
    {
        currentHealth -= amount;
        UpdateUI();
    }


    public void Die()
    {
        stat.death = true;
        Debug.Log("you have died");
    }

 
}
