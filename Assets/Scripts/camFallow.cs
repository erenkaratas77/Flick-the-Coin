using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class camFallow : MonoBehaviour
{
    public static camFallow instance;
    bool PlayerOnScoreLine;
    CinemachineTransposer transposer;
    CinemachineVirtualCamera vcam;
    public Vector3 start, end;
    Vector3 newOffset;
    void Start()
    {
        instance = this;
        vcam = transform.GetComponentInChildren<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = start;

    }
    private void Update()
    {
        if (PlayerOnScoreLine)
        {

           newOffset = Vector3.Lerp(newOffset, end, Time.deltaTime);
           transposer.m_FollowOffset = newOffset;
           vcam.transform.rotation = Quaternion.RotateTowards(vcam.transform.rotation, Quaternion.Euler(15, -8, 0), 5 * Time.deltaTime);


        }
    }
    // Update is called once per frame
    public void scoreLineCamAngle()
    {
        PlayerOnScoreLine = true;
        newOffset = start;

    }

    
}
