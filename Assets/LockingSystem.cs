using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LockingSystem : MonoBehaviour
{
    public LayerMask targetMask;
    GameObject _enemyTargetLocked;
    float lockTime;
    float _readyToFireTime = 3f;
    public AudioSource lockingBeep;
    public AudioSource targetLocked;
    public AudioClip targetLockedClip;
    public GameObject roket;
    public GameObject launchPoint;
    public ParticleSystem expoParticle;
    public Image lockCursor;
    float tempTime;

    void Update()
    {
        
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.SphereCast(ray, 8f, out hit, Mathf.Infinity, targetMask))
        {
            if(_enemyTargetLocked!=null && (_enemyTargetLocked.GetInstanceID()==hit.transform.gameObject.GetInstanceID()))
            {
                LockTime();
            }
            else
            {
                ResetLockTime();
            }
                _enemyTargetLocked = hit.transform.gameObject;
            
            
            
        }
        else
        {
            ResetLockTime();

        }
        

    }
    void LockTime()
    {
        lockTime += Time.deltaTime;
        tempTime += Time.deltaTime;
        if (tempTime >= 0.1f && tempTime<=0.5f)
        {
            lockCursor.color = Color.red;
            if (lockTime >= _readyToFireTime)
            {
                lockCursor.color = Color.red;
                tempTime = 1f;
            }
            if (tempTime >= 0.2f && tempTime<=0.4f)
            {
                tempTime = 0f;
            }
        }
        else if(tempTime<=0.1f)
        {
            lockCursor.color = Color.white;
        }

        if (!lockingBeep.isPlaying)
            lockingBeep.Play();
        //else
        //    lockingBeep.pitch += Time.deltaTime *0.1f;

        
        if (lockTime >= _readyToFireTime)
        {
            lockingBeep.Stop();
            lockingBeep.pitch = 1f;
            if(!targetLocked.isPlaying)
            {
                targetLocked.Play();
            }
            if(lockTime>= _readyToFireTime+ targetLockedClip.length)
            {
                targetLocked.Stop();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                expoParticle.Play();
                GameObject tempRocket= Instantiate(roket, launchPoint.transform.position, launchPoint.transform.rotation);
                tempRocket.GetComponent<Rocket>().target = _enemyTargetLocked.transform;
            }
            
            
        }    
        

    }
    void ResetLockTime()
    {
        if (lockingBeep.isPlaying)
        {
            lockingBeep.Stop();
            lockingBeep.pitch = 1f;
        }
        if(targetLocked.isPlaying)
        {
            targetLocked.Stop();
        }
        lockCursor.color = Color.white;

        lockTime = 0f;
        tempTime = 0f;
    }

}
