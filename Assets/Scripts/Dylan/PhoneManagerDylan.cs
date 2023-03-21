using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManagerDylan : MonoBehaviour
{
    private FirstPersonController sP;

    public GameObject _App1;
    public GameObject _App2;
    public GameObject _App3;
    public GameObject _App4;
    public GameObject _App5;
    public GameObject _App6;

    public GameObject backButton;


    // Start is called before the first frame update
    void Start()
    {
        if (sP._mobilePhone.activeInHierarchy == true)
        {
            _App1.SetActive(false);
            _App2.SetActive(false);
            _App3.SetActive(false);
            _App4.SetActive(false);
            _App5.SetActive(false);
            _App6.SetActive(false);
        }

        if (sP._mobilePhone.activeInHierarchy == false)
        {
            _App1.SetActive(false);
            _App2.SetActive(false);
            _App3.SetActive(false);
            _App4.SetActive(false);
            _App5.SetActive(false);
            _App6.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_App1.activeInHierarchy == false && _App2.activeInHierarchy == false && _App3.activeInHierarchy == false && _App4.activeInHierarchy == false && _App5.activeInHierarchy == false && _App6.activeInHierarchy == false)
        {
            backButton.SetActive(false);
        }
    }


    public void App1()
    {
        _App1.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void App2()
    {
        _App2.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

    }

    public void App3()
    {
        _App3.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

    }

    public void App4()
    {
        _App4.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

    }

    public void App5()
    {
        _App5.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

    }

    public void App6()
    {
        _App6.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

    }
    

    public void BackButton()
    {
        _App1.SetActive(false);
        _App2.SetActive(false);
        _App3.SetActive(false);
        _App4.SetActive(false);
        _App5.SetActive(false);
        _App6.SetActive(false);
    }

    
    public void TurnOffPhone()
    {
      //sP._mobilePhone.SetActive(false);
    }

}
