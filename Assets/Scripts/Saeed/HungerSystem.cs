using System.Collections;
using UnityEngine;

public class HungerSystem : MonoBehaviour
{
    public DisplayStats stats;

    [SerializeField] private float decreaseRate = 10f;
    [SerializeField] private float increaseAmount = 100f;

    private bool isInteracting = false;

    private void Start()
    {
        StartCoroutine(DecreaseHunger());
    }

    private IEnumerator DecreaseHunger()
    {
        while (true)
        {
            if (!isInteracting)
            {
                stats.currentHunger -= decreaseRate;
                stats.UpdateUI();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            isInteracting = true;
            stats.currentHunger = increaseAmount;
            stats.UpdateUI();

            StartCoroutine(ResetInteracting());
        }
    }

    private IEnumerator ResetInteracting()
    {
        yield return new WaitForSeconds(1f);
        isInteracting = false;
    }
}