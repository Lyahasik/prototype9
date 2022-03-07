using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInvise : MonoBehaviour
{
    public GameManager GameManager;
    
    public Image ImageStels;
    public float SpeedUpStels = 0.8f;
    public float SpeedDownStels = 0.2f;

    private bool _visible = false;
    public float Smoke = 1.0f;

    void Update()
    {
        if (!GameManager.Fail && !GameManager.Success)
        {
            UpdateStels();
            CheckStels();
        }
    }

    void UpdateStels()
    {
        if (_visible)
        {
            ImageStels.fillAmount += SpeedUpStels * Smoke * Time.deltaTime;
        }
        else if (ImageStels.fillAmount > 0.0f)
        {
            ImageStels.fillAmount -= SpeedDownStels * Time.deltaTime;
        }
    }

    void CheckStels()
    {
        if (ImageStels.fillAmount >= 1.0f)
        {
            GameManager.GameFail();
        }
        else if (ImageStels.fillAmount >= 0.75f)
        {
            if (!GameManager.SirenaActive)
            {
                GameManager.OnSirena();
            }
        }
        else
        {
            if (GameManager.SirenaActive)
            {
                GameManager.OffSirena();
            }
        }
    }

    public void OnVisiblePlayer()
    {
        _visible = true;
    }

    public void OffVisiblePlayer()
    {
        _visible = false;
    }
}
