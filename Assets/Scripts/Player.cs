using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public enum PlayerStates { idle, moving, fail, scoreLine, end, highScoreEnd};

public class Player : MonoBehaviour
{
    public PlayerStates playerState;
    public static Player instance;
    public GameObject MeshesParent;
    public float myTotalMoney;
    public float moveSpeed, boundary;
    public bool isfinished;
    float backForce,rotAngle, distToX;
    public ParticleSystem[] particles;

    int lastMeshIndex;
    float timer, waitSecond;
    private void Awake()
    {
        
        lastMeshIndex = 0;
        waitSecond = 4;
        playerState = PlayerStates.idle;
        instance = this;
        myTotalMoney = 1;
    }
    void Start()
    {
    }
    
    void Update()
    {
        GetComponentInChildren<TextMeshPro>().transform.parent.transform.LookAt(Camera.main.transform);
        DetermineState();
    }
    void movement()
    {
        rotAngle += InputPanel.valX * 50000 * Time.deltaTime;
        rotAngle = Mathf.Lerp(rotAngle, 0, 6*Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rotAngle, 0), 150 * Time.deltaTime);

        backForce = Mathf.Clamp(backForce + Time.deltaTime * 2, -1, 1);
        
        transform.position += new Vector3(1, 0, 0) * InputPanel.valX * Screen.width * Time.deltaTime * 2;


        transform.position += new Vector3(0, 0, 1) * Time.deltaTime * moveSpeed * backForce;

        MeshesParent.transform.Rotate(new Vector3(backForce*Time.deltaTime*1000, 0, 0));

        //Clampler
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, ClampAngle(transform.eulerAngles.y, -45, 45), transform.rotation.eulerAngles.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundary - 0.4f, boundary + 0.2f), transform.position.y, transform.position.z);
        
    }
    void idle()
    {
        timer += Time.deltaTime;
        if (timer >= waitSecond)
        {
            timer = 0;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 250, 0));
            transform.DOLocalRotate(new Vector3(0,30,0), .5f).SetLoops(2,LoopType.Yoyo);

        }

    }
    void fail()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        FailPanel.instance.transform.DOScale(1, .2f);
        backForce = Mathf.Clamp(backForce + Time.deltaTime * 2, -1, 0);
        transform.position += new Vector3(0, 0, 1) * Time.deltaTime * moveSpeed * backForce;
        

    }
    void scoreLine()
    {
        rotAngle += distToX;
        distToX = 0;
        rotAngle = Mathf.Lerp(rotAngle, 0, 3 * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rotAngle, 0), 150 * Time.deltaTime);


        MeshesParent.transform.Rotate(new Vector3(backForce, 0, 0));
        backForce = Mathf.Clamp(backForce + Time.deltaTime * 2, -1, 1);
        transform.position += new Vector3(0, 0, 1) * Time.deltaTime * moveSpeed * backForce * 1.6f;


    }
    void end()
    {
        backForce = Mathf.Clamp(backForce + Time.deltaTime * 2, -1, 0);
        transform.position += new Vector3(0, 0, 1) * Time.deltaTime * moveSpeed * backForce;
        if (backForce == 0)
        {
            FinishPanel.instance.getLevelEndMoney(myTotalMoney);
            this.enabled = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ramp")
        {
            transform.DORotate(new Vector3(0, 360, 0), .5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(4, LoopType.Restart);

            transform.DOBlendableMoveBy(new Vector3(0, 8, 0), 1).SetLoops(2, LoopType.Yoyo);
        }
        if (other.gameObject.tag == "Enemy" || (other.gameObject.tag=="Coin" && other.GetComponent<Coin>().operationValue>myTotalMoney)) //düşmansa ve coin benden büyükse 
        {
            backForce = -1f;
            particles[0].Play();
            UpperPanel.instance.endCombo();
            subMoney(3);

        }
        if (other.gameObject.tag == "Obstacle")
        {
            backForce = -1f;
            particles[0].Play();
            UpperPanel.instance.endCombo();
            subMoney(2);
        }
        if (other.gameObject.tag == "Coin" && other.GetComponent<Coin>().operationValue <= myTotalMoney)
        {
            addMoney(other.GetComponent<Coin>().operationValue);
            other.gameObject.SetActive(false);
            UpperPanel.instance.makeCombo();
        }
        if (other.gameObject.tag == "Door")
        {
            if (other.GetComponent<Door>().deActivateDoor != null)
            {
                other.GetComponent<Door>().deActivateDoor.GetComponent<Collider>().enabled=false;
            }
            other.GetComponent<Door>().Operation(myTotalMoney);
            other.GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
            other.GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);
            transform.GetComponentInChildren<TextMeshPro>().text = myTotalMoney + "$";

        }
        if (other.gameObject.tag == "Finish")
        {
            playerState = PlayerStates.scoreLine;
            transform.DOMoveX(0, .5f);
            distToX = -transform.position.x*10;
            camFallow.instance.scoreLineCamAngle();
        }
        if (other.gameObject.tag == "LevelEndCoin")
        {
            HapticsManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.RigidImpact);
            if (other.GetComponent<Coin>().operationValue > myTotalMoney)
            {
                backForce = -1;
                playerState = PlayerStates.end;
            }
            else
            {
                other.gameObject.SetActive(false);
                MeshesParent.transform.DOLocalRotate(new Vector3(0, 360, 0), .5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
                transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), .5f, 10, 1f);
            }
            
        }
        if (other.gameObject.tag == "ScoreLine")
        {
            other.GetComponent<Emmision>().IncreaseEmmission();
        }
        if (other.gameObject.tag == "FinishParticle")
        {
            other.GetComponent<ParticleSystem>().Play();
        }
        if (other.gameObject.tag == "HighScore")
        {
            playerState = PlayerStates.highScoreEnd;
            MeshesParent.transform.DOLocalRotate(new Vector3(0, 360, 0), .8f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(4, LoopType.Restart).OnComplete(
            () =>FinishPanel.instance.getLevelEndMoney(myTotalMoney));
            transform.DOMove(new Vector3(0, 3.75f, 562), .5f).SetEase(Ease.Linear).OnComplete(() => particles[1].Play());
        }
        LevelManager.instance.checkCoinsState();

    }
    float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
    public void addMoney(float addValue)
    {
        HapticsManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.SoftImpact);
        myTotalMoney += addValue;
        transform.GetComponentInChildren<TextMeshPro>().text = myTotalMoney + "$";
        transform.DORotate(new Vector3(0, 360, 0), .5f).SetRelative(true).SetEase(Ease.Linear);
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), .5f, 10, 1f);
        stateControl();

    }
    public void subMoney(float subValue)
    {
        HapticsManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.RigidImpact);
        myTotalMoney -= subValue;
        if (myTotalMoney <= 0)
        {
            myTotalMoney = 0;
            playerState = PlayerStates.fail;
        }
        transform.GetComponentInChildren<TextMeshPro>().text = myTotalMoney + "$";
        transform.DORotate(new Vector3(0, 360, 0), .5f).SetRelative(true).SetEase(Ease.Linear);
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), .5f, 10, 1f);
        stateControl();

    }

    void DetermineState()
    {
        switch (playerState)
        {
            case PlayerStates.idle:
                idle();
                break;
            case PlayerStates.moving:
                movement();
                break;
            case PlayerStates.fail:
                fail();
                transform.DORotate(new Vector3(0, 0, 90), 1f);
                break;
            case PlayerStates.scoreLine:
                scoreLine();
                break;
            case PlayerStates.end:
                end();
                break;
            case PlayerStates.highScoreEnd:
                
                break;
        }
    }

    void stateControl()
    {
        if (myTotalMoney < 20) changeMesh(0);
        else if (myTotalMoney < 50) changeMesh(1);
        else if (myTotalMoney < 110) changeMesh(2);
        else if (myTotalMoney < 165) changeMesh(3);
        else if (myTotalMoney < 385) changeMesh(4);
        else if (myTotalMoney < 572) changeMesh(5);
        else if (myTotalMoney < 1024) changeMesh(6);


    }
    void changeMesh(int meshIndex)
    {
        if (lastMeshIndex != meshIndex)
        {
            particles[2].Play();
            particles[3].Play();
            lastMeshIndex = meshIndex;
            for (int i = 0; i < MeshesParent.transform.childCount; ++i)
            {
                MeshesParent.transform.GetChild(i).gameObject.SetActive(false);
            }
            MeshesParent.transform.GetChild(meshIndex).gameObject.SetActive(true);
        }
        

    }
}
