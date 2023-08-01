using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class Safe_System : MonoBehaviour
{

    [SerializeField] GameObject keyToSpawn;
    [SerializeField] Vector3 keySpawnPos;


    public Button NextNumDown_1st_Digit;
    [SerializeField] public Button NextNumDown_2nd_Digit;
    [SerializeField] public Button NextNumDown_3rd_Digit;

    [SerializeField] public Button NextNumUp_1st_Digit;
    [SerializeField] public Button NextNumUp_2nd_Digit;
    [SerializeField] public Button NextNumUp_3rd_Digit;

     public TextMeshProUGUI First_Digit_Text;
    [SerializeField] public TextMeshProUGUI Second_Digit_Text;
    [SerializeField] public TextMeshProUGUI Third_Digit_Text;

    [SerializeField] public GameObject UnlockedText;
    [SerializeField] public GameObject Safe_Panel;
    [SerializeField] public GameObject Safe_Guide_Text;


    public GameObject Safe_Gameobject;
    public GameObject SafeCam;
    public GameObject DefaultCam;



     public int First_Digit_Amount;
    [SerializeField] public int Second_Digit_Amount;
    [SerializeField] public int Third_Digit_Amount;

    [SerializeField] public int Safe_Code = 234;

    private int minDigit = 0;
    private int maxDigit = 99;

    StarterAssets.FirstPersonController cont;





    private void Start()
    {
        //Safe_Code = Random.Range(100, 900);

        First_Digit_Amount = Random.Range(minDigit, maxDigit);
        Second_Digit_Amount = Random.Range(minDigit, maxDigit);
        Third_Digit_Amount = Random.Range(minDigit, maxDigit);

        First_Digit_Text.text =  First_Digit_Amount.ToString();
        Second_Digit_Text.text = Second_Digit_Amount.ToString();
        Third_Digit_Text.text = Third_Digit_Amount.ToString();

        UnlockedText.SetActive(false);

        Safe_Gameobject.tag = "Safe";

    }


    private void Update()
    {
        if(Safe_Panel.activeInHierarchy == true)
        {
            Safe_Guide_Text.SetActive(true);
        }else if(Safe_Panel.activeInHierarchy == false)
        {
            Safe_Guide_Text.SetActive(false);
        }



        if(First_Digit_Amount > 99)
        {
            First_Digit_Text.text = "0";
            First_Digit_Amount = 0;
        }
        if(First_Digit_Amount < 0)
        {
            First_Digit_Text.text = "99";
            First_Digit_Amount = 99;
        }



        if (Second_Digit_Amount > 99)
        {   
            Second_Digit_Text.text = "0";
            Second_Digit_Amount = 0;
        }   
        if (Second_Digit_Amount < 0)
        {   
            Second_Digit_Text.text = "99";
            Second_Digit_Amount = 99;
        }



        if (Third_Digit_Amount > 99)
        {   
            Third_Digit_Text.text = "0";
            Third_Digit_Amount = 0;
        }   
        if (Third_Digit_Amount < 0)
        {   
            Third_Digit_Text.text = "99";
            Third_Digit_Amount = 99;
        }

        //--------------------Code Combination--------------------------//

        if(First_Digit_Amount == 2 && Second_Digit_Amount == 3 && Third_Digit_Amount == 4 && Safe_Gameobject.tag == "Safe")
        {
            UnlockedText.SetActive(true);
            Safe_Panel.SetActive(false);

            Safe_Gameobject.tag = "Untagged";

            StartCoroutine(UnlockedSafe());
        }
        //--------------------Code Combination--------------------------//

    }


    public void NextNumber1stD() 
    {
        First_Digit_Amount += 1;
        First_Digit_Text.text = First_Digit_Amount.ToString();
    }

    public void NextNumber2stD()
    {
        Second_Digit_Amount += 1;
        Second_Digit_Text.text = Second_Digit_Amount.ToString();
    }

    public void NextNumber3stD()
    {

        Third_Digit_Amount += 1;
        Third_Digit_Text.text = Third_Digit_Amount.ToString();
    }






    public void PrevNumber1stD()
    {
        First_Digit_Amount -= 1;
        First_Digit_Text.text = First_Digit_Amount.ToString();
    }
    public void PrevNumber2stD()
    {
        Second_Digit_Amount -= 1;
        Second_Digit_Text.text = Second_Digit_Amount.ToString();
    }
    public void PrevNumber3stD()
    {
        Third_Digit_Amount -= 1;
        Third_Digit_Text.text = Third_Digit_Amount.ToString();
    }

    

    IEnumerator UnlockedSafe()
    {
        yield return new WaitForSeconds(2);

        Cursor.lockState = CursorLockMode.Locked;

        PhoneManager.Instance.mouseShouldBeUseable = false;
        PhoneManager.Instance.phoneIsUseable = true;
        WatchManager.watchShouldBeUseable = true;


        Instantiate(keyToSpawn, keySpawnPos, Quaternion.identity);

        Destroy(UnlockedText);

        SafeCam.SetActive(false);
        DefaultCam.SetActive(true);
        SafeInteractable.safeHasBeenCracked = true;
    }

}
