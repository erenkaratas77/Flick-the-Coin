using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    public float operationValue;
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshPro>().text = operationValue + "$";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
