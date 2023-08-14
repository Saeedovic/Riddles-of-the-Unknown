using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutEffect : MonoBehaviour
{
    Image imageToFade;
    [SerializeField] float fadingSpeed = 4f;

    float transparencyLevel = 0f;

    public bool screenIsBlack { get; private set; }
    public bool faderRunning { get; private set; }

    private void OnEnable()
    {
        imageToFade = GetComponent<Image>();
        imageToFade.color = new Color(0, 0, 0, 0);
        screenIsBlack = false;
    }

    public IEnumerator FadeInToBlack()
    {
        faderRunning = true;
        PhoneManager.Instance.fullscreenIsSettable = false;


        transparencyLevel += fadingSpeed * Time.deltaTime;
        imageToFade.color = new Color(0, 0, 0, transparencyLevel);

        if (imageToFade.color.a >= 1)
        {
            yield return new WaitForEndOfFrame();

            transparencyLevel = 1;

            screenIsBlack = true;
            faderRunning = false;
            PhoneManager.Instance.fullscreenIsSettable = true;

            yield break;
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(FadeInToBlack());
    }

    public IEnumerator FadeOutOfBlack()
    {
        faderRunning = true;
        PhoneManager.Instance.fullscreenIsSettable = false;

        transparencyLevel -= fadingSpeed * Time.deltaTime;
        imageToFade.color = new Color(0, 0, 0, transparencyLevel);

        if (imageToFade.color.a <= 0)
        {
            yield return new WaitForEndOfFrame();

            transparencyLevel = 0;

            screenIsBlack = false;
            faderRunning = false;
            PhoneManager.Instance.fullscreenIsSettable = true;

            yield break;
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(FadeOutOfBlack());
    }
}
