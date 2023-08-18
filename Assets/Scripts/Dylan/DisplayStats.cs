using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayStats : MonoBehaviour
{
    public PlayerStatsData stat;

    [SerializeField] bool beginDelay;

    [SerializeField] PlayerCon playerCon;


    WatchManager watchManager;
    [SerializeField] GameObject WatchObjRef;
    [SerializeField] GameObject DrinkWaterWarning;
    [SerializeField] GameObject EatFoodWarning;

    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject In_Game_UI;

    public GameObject playAgain;
    public GameObject exit;


    float delay;


    #region Text Holders
    [Header("[Health & Stamina Text Holders]")]
    [Space(5)]

    //public float currentHealth;
    public float currentStamina;
    public float currentHunger;
    public float currentThirst;

    //public float maxHealth;
    public float maxStamina;
    public float maxHunger;
    public float maxThirst;


    public float currentPlayerStatPoint;


    public TextMeshProUGUI health;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI hunger;
    public TextMeshProUGUI thrist;

    bool hungerWarningHasBeenMade;
    bool thirstWarningHasBeenMade;

    public AudioSource playerAudio;

    public AudioClip voiceOverHungerWarning;

    public AudioClip voiceOverThirstWarning;

    #endregion
    private void Awake()
    {
 
       // currentHealth = stat.PlayerHealth;
        currentStamina = stat.PlayerStamina;
        currentHunger = stat.PlayerHunger;
        currentThirst = stat.PlayerThrist;

        //maxHealth

        DrinkWaterWarning.SetActive(false);
        EatFoodWarning.SetActive(false);
        GameOverPanel.SetActive(false);

    }
    public void Start()
    {
        watchManager = WatchObjRef.GetComponent<WatchManager>();
        playerCon = GetComponent<PlayerCon>();

        #region Stats Equal Max-Values
       currentStamina = 100;
      // currentHealth = 100;
        currentHunger = 100;
        currentThirst = 100;


        #endregion

        #region Bool Equals-start

        stat.death = false;

        #endregion

        #region Bar Max Value-start

      //  health.text = currentHealth.ToString("0");
        stamina.text = currentStamina.ToString("0");
        hunger.text = currentHunger.ToString("0");
        thrist.text = currentThirst.ToString("0");


        #endregion
        UpdateUI();
    }

    public void Update()
    {
        maxStamina = stat.MaxPlayerStamina;
        maxHunger = stat.MaxPlayerHunger;
        maxThirst = stat.MaxPlayerThrist;


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
        else if (currentStamina <= maxStamina)
        {
            
                //Debug.Log("regeneration");
                //GetComponent<Animator>().speed = 1;
                currentStamina += stat.staminaUpRate * Time.deltaTime;
            //currentStamina = Mathf.Clamp(currentStamina, 0, stat.PlayerStamina);
        }
        if(currentStamina <= 0.5f)
        {
           playerCon.runMultiplier = 1;
        }
        else if(currentStamina >= 0.5f)
        {
           playerCon.runMultiplier = 2;
        }


        #endregion

        #region If Alive
        if (currentHunger >= 0 || currentThirst >= 0)
        {
            currentHunger -= stat.hungerDownRate * Time.deltaTime;
            currentThirst -= stat.thristDownRate * Time.deltaTime;



           // if (!beginDelay)
           // {
                //currentStamina -= stat.sprintStamina * Time.deltaTime;
                //SDebug.Log("IM SPEEEDINGNGGNGGNG");
                //GetComponent<Animator>().speed = 3;
           // }
        }


        #endregion

        #region If Health Is Less Or Equal To 0 
        if (currentHunger <= 0 || currentThirst <= 0) //Needs to be Revised accoding to new values
        {
            Die();
        }
        #endregion


        #region WARNING CHECKS: If Thirst OR Hunger is LESS than 20%
        if (currentHunger <= 20 && hungerWarningHasBeenMade == false) //Needs to be Revised accoding to new values
        {
            //Activate Watch and Tell the Player to Drink Water OR they will die!
            watchManager.SetWatchState(false);
            EatFoodWarning.SetActive(true);

            playerAudio.PlayOneShot(voiceOverHungerWarning);

            StartCoroutine(TutorialManager.DisplaySubs("Maaaaan I'm hungry. Let me see if I got anything in the inventory app", 4.5f));

            hungerWarningHasBeenMade = true;

            if (Input.GetKeyDown(KeyCode.J))
            {
                watchManager.SetWatchState(true);
                EatFoodWarning.SetActive(false);
            }
        }
        if(currentThirst <= 20 && thirstWarningHasBeenMade == false)
        {
            //Activate Watch and Tell the Player to Eat Food OR they will die!
            watchManager.SetWatchState(false);
            DrinkWaterWarning.SetActive(true);

            playerAudio.PlayOneShot(voiceOverThirstWarning);

            StartCoroutine(TutorialManager.DisplaySubs("Got to stay hydrated or I might pass out!", 2.5f));

            thirstWarningHasBeenMade = true;

            if (Input.GetKeyDown(KeyCode.J))
            {
                watchManager.SetWatchState(true);
                DrinkWaterWarning.SetActive(false);
            }
        }
        #endregion

        #region WARNING CHECKS: If Thirst OR Hunger is MORE than 20%
        if (EatFoodWarning.activeInHierarchy == true && currentHunger >= 20) //Needs to be Revised accoding to new values
        {
            EatFoodWarning.SetActive(false);
            hungerWarningHasBeenMade = false;
        }
        if (DrinkWaterWarning.activeInHierarchy == true && currentThirst >= 20)
        {
            DrinkWaterWarning.SetActive(false);
            thirstWarningHasBeenMade = false;
        }
        #endregion




        UpdateUI();

    }

    public void UpdateUI()
    {

        #region Math Clamps
       // currentHealth = Mathf.Clamp(currentHealth, 0, 190);
        currentStamina = Mathf.Clamp(currentStamina, 0, 190);
        currentHunger = Mathf.Clamp(currentHunger, 0, 190);
        currentThirst = Mathf.Clamp(currentThirst, 0, 190);


        #endregion

        #region Health & Stamina Values

       // health.text = currentHealth.ToString("0");
        stamina.text = currentStamina.ToString("0");
        hunger.text = currentHunger.ToString("0");
        thrist.text = currentThirst.ToString("0");




        #endregion
    }

    public void GetDamaged(int amount) //take dmg and specify amt
    {
      //  currentHealth -= amount;
        UpdateUI();
    }

    public void HungerWarning()
    {
        if (hungerWarningHasBeenMade == false)
        {
            StartCoroutine(TutorialManager.DisplaySubs("I'm starving, I need to eat Something Asap", 2f));

            hungerWarningHasBeenMade = true;
        }
    }


    public void Die()
    {
        stat.death = true;
        Debug.Log("you have died");

        In_Game_UI.SetActive(false);
        GameOverPanel.SetActive(true);
        playerCon.enabled = false;
        if (EventSystem.current.currentSelectedGameObject != playAgain && EventSystem.current.currentSelectedGameObject != exit)
        {

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(playAgain);

        }
    }



 
}
