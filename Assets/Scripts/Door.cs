using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public enum doorTypes { add, subtract, times, divide};

public class Door : MonoBehaviour
{
    [SerializeField] private doorTypes doorType;
    [SerializeField] float operationValue;
    public GameObject deActivateDoor;
    // Start is called before the first frame update
    void Start()
    {
        switch (doorType)
        {
            case doorTypes.add:
                transform.GetComponentInChildren<TextMeshPro>().text = "+" + operationValue;
                break;
            case doorTypes.subtract:
                transform.GetComponentInChildren<TextMeshPro>().text = "-" + operationValue;
                break;
            case doorTypes.times:
                transform.GetComponentInChildren<TextMeshPro>().text = "x" + operationValue;
                break;
            case doorTypes.divide:
                transform.GetComponentInChildren<TextMeshPro>().text = "1/" + operationValue;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void Operation(float money)
    {
        switch (doorType)
        {
            case doorTypes.add:
                Player.instance.addMoney(operationValue);
                break;
            case doorTypes.subtract:
                Player.instance.subMoney(operationValue);
                break;
            case doorTypes.times:
                Player.instance.addMoney(money*(operationValue-1));
                break;
            case doorTypes.divide:
                Player.instance.subMoney((int)(money/operationValue));
                break;

        }
    }
}
