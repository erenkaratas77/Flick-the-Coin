using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emmision : MonoBehaviour
{
    public float emmissionSpeed = 3; 
    private float emmission = 0.0f; // Emmission değeri
    bool increasing = false; // Emmission değeri arttırılıyor mu?

    // Emmission değerini arttırır
    public void IncreaseEmmission()
    {
        increasing = true;
    }

    // Emmission değerini azaltır
    public void DecreaseEmmission()
    {
        increasing = false;
    }

    void Update()
    {
        if (increasing)
        {
            // Emmission değerini arttır
            emmission = Mathf.Lerp(emmission, 1.0f, Time.deltaTime * emmissionSpeed);
        }
        else
        {
            // Emmission değerini azalt
            emmission = Mathf.Lerp(emmission, 0.0f, Time.deltaTime * emmissionSpeed);
        }
        // materialdaki Emmission değerini güncelle
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(emmission, emmission, emmission));
    }
}
