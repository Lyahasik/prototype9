using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CameraView[] CameresView;
    public PlayerInvise Player;

    public GameObject CameraPlayer;
    public GameObject CameraEnd;
    public GameObject MenuGame;
    public GameObject MenuFail;
    public GameObject MenuSuccess;
    public Text TextHelp;
    
    public AudioSource AudioGame;
    public AudioSource AudioSirena;
    public AudioSource AudioFail;
    public AudioSource AudioSucces;

    public bool SirenaActive = false;
    public bool Fail = false;
    public bool Success = false;
    private bool _hideHelp = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        InputKey();
        CheckVisible();
        CheckHelp();
        HideHelp();
    }

    void CheckHelp()
    {
        RaycastHit[] hits = Physics.RaycastAll(CameraPlayer.transform.position, CameraPlayer.transform.forward, 1.0f);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("fan"))
            {
                SwitchHelp(true);
                return;
            }
            if (hit.collider.CompareTag("key"))
            {
                SwitchHelp(true);
                return;
            }
            if (hit.collider.CompareTag("lock"))
            {
                SwitchHelp(true);
                return;
            }
            if (hit.collider.CompareTag("target"))
            {
                SwitchHelp(true);
                return;
            }
            if (hit.collider.CompareTag("keyLaser"))
            {
                SwitchHelp(true);
                return;
            }
        }
        
        SwitchHelp(false);
    }
    
    void SwitchHelp(bool value)
    {
        if (!_hideHelp && value)
        {
            _hideHelp = true;
        }
        else if (_hideHelp && !value)
        {
            _hideHelp = false;
        }
    }
    
    void HideHelp()
    {
        if (!_hideHelp
            && TextHelp.color.a > 0.0f)
        {
            TextHelp.color = new Color(TextHelp.color.r, TextHelp.color.g, TextHelp.color.b, TextHelp.color.a - 2.0f * Time.deltaTime);
        }
        else if (_hideHelp
                 && TextHelp.color.a < 1.0f)
        {
            TextHelp.color = new Color(TextHelp.color.r, TextHelp.color.g, TextHelp.color.b, TextHelp.color.a + 3.0f * Time.deltaTime);
        }
    }

    void InputKey()
    {
        if (Fail || Success)
        {
            if (Input.GetKeyDown("return"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void CheckVisible()
    {
        foreach (CameraView cameraView in CameresView)
        {
            if (cameraView.IsVisible())
            {
                Player.OnVisiblePlayer();
                return;
            }
        }
        
        Player.OffVisiblePlayer();
    }

    public void OnSirena()
    {
        SirenaActive = true;
        AudioSirena.Play();
    }
    
    public void OffSirena()
    {
        SirenaActive = false;
        AudioSirena.Stop();
    }

    public void GameFail()
    {
        Fail = true;
        
        AudioGame.Stop();
        AudioFail.Play();
        MenuGame.SetActive(false);
        CameraPlayer.SetActive(false);
        CameraEnd.SetActive(true);
        TextHelp.enabled = false;
        
        MenuFail.SetActive(true);
    }

    public void GameSuccess()
    {
        Success = true;
        
        AudioGame.Stop();
        AudioSucces.Play();
        MenuGame.SetActive(false);
        CameraPlayer.SetActive(false);
        CameraEnd.SetActive(true);
        TextHelp.enabled = false;
        
        MenuSuccess.SetActive(true);
    }
    
    
}
