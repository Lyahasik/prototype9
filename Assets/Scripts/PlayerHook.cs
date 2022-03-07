using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    public GameManager GameManager;

    public GameObject Camera;
    public GameObject DoorFinish;
    public GameObject DoorLaser;

    private bool _key = false;
    private bool _laserOn = true;
    
    void Update()
    {
        if (!GameManager.Fail && !GameManager.Success)
        {
            InputKey();
        }
    }

    void InputKey()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits = Physics.RaycastAll(Camera.transform.position, Camera.transform.forward, 1.0f);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("fan"))
                {
                    hit.collider.GetComponent<Fan>().OnSmoke();
                    return;
                }
                if (hit.collider.CompareTag("key"))
                {
                    Destroy(hit.collider.gameObject);
                    _key = true;
                    return;
                }
                if (_key
                    && hit.collider.CompareTag("lock"))
                {
                    hit.collider.GetComponent<AudioSource>().Play();
                    DoorFinish.GetComponent<AudioSource>().Play();
                    Invoke("DestroyDoor", 3.0f);
                    _key = false;
                    return;
                }
                if (_laserOn
                    && hit.collider.CompareTag("keyLaser"))
                {
                    _laserOn = false;
                    hit.collider.GetComponent<AudioSource>().Play();
                    DoorLaser.GetComponent<DoorLaser>().Off();
                    Invoke("DestroyDoorLaser", 2.0f);
                    return;
                }
                if (hit.collider.CompareTag("target"))
                {
                    GameManager.GameSuccess();
                    return;
                }
            }
        }
    }

    void DestroyDoor()
    {
        Destroy(DoorFinish);
    }

    void DestroyDoorLaser()
    {
        Destroy(DoorLaser);
    }
}
