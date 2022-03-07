using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameObject Smoke;

    public void OnSmoke()
    {
        Smoke.SetActive(true);
    }
}
