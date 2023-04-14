using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcopointScan : MonoBehaviour
{
    [SerializeField] float scanRadius = 50f;
    [SerializeField] float activeScanTime = 7f;

    [SerializeField] Color scanColor = Color.cyan;

    bool scanIsActive = false;

    List<PointOfInterest> ecopoints;


    void Start()
    {
        ecopoints = new List<PointOfInterest>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !scanIsActive)
        {
            ecopoints = new List<PointOfInterest>();
            GetPointsInVicinity();
            StartCoroutine(DisplayScanEffect());
        }
    }

    void GetPointsInVicinity()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, scanRadius);

        foreach (Collider collider in colliders)
        {
            PointOfInterest newEcopoint = collider.GetComponent<PointOfInterest>();

            if (newEcopoint != null)
            {
                ecopoints.Add(newEcopoint);
            }
        }
    }

    // add outline, wait, then remove outline.
    IEnumerator DisplayScanEffect()
    {
        scanIsActive = true;
     


        foreach (PointOfInterest pointOfInterest in ecopoints)
        {
            Outline outline = pointOfInterest.gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineColor = scanColor;
            outline.OutlineWidth = 5f;
        }

        yield return new WaitForSeconds(activeScanTime);

        foreach (PointOfInterest pointOfInterest in ecopoints)
        {
            Destroy(pointOfInterest.gameObject.GetComponent<Outline>());
        }

        scanIsActive = false;
    }
}
