using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Coin : MonoBehaviour
{
    public float operationValue;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponentInChildren<TextMeshPro>().text = operationValue + "$";
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
