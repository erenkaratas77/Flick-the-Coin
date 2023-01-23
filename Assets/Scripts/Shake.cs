using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        StartCoroutine(shakeStart());
    }

    
    public float shakeAmount = 5f;
    public float shakeDuration = 5f;

    private IEnumerator shake()
    {
        Vector2 originalPosition = transform.position;

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            transform.position = new Vector2(originalPosition.x + x, originalPosition.y + y);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
    }

    IEnumerator shakeStart()
    {
        while (true)
        {
            StartCoroutine(shake());
            yield return new WaitForSeconds(shakeDuration);
        }
    }

}
