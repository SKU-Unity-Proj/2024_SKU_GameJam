using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerGrid : MonoBehaviour
{
    public Image imageGrid;
    public GameObject toastPanal;
    public IEnumerator EMarkerGrid()
    {
        int count = 0;
        this.gameObject.SetActive(true);

        while (count < 100)
        {
            this.imageGrid.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            this.imageGrid.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            count++;
        }
    }
}