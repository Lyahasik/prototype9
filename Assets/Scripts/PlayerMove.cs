using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject Camera;
    public Text TextBegin;
    
    public float Speed = 10.0f;
    public float SpeedRotate = 10.0f;

    private CharacterController _characterController;
    private Vector3 _vectorMove;
    
    private bool _move;
    private bool _hideBegin = false;
    
    void Start()
    {
        _vectorMove = Vector3.zero;
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!GameManager.Fail && !GameManager.Success)
        {
            InputKey();
            Move();
            HideBegin();
        }
    }

    void HideBegin()
    {
        if (_hideBegin
            && TextBegin.color.a > 0.0f)
        {
            TextBegin.color = new Color(TextBegin.color.r, TextBegin.color.g, TextBegin.color.b, TextBegin.color.a - 0.4f * Time.deltaTime);
        }
    }

    void Sound(bool value)
    {
        if (!_move && value)
        {
            _move = true;
            GetComponent<AudioSource>().Play();
        }
        else if (_move && !value)
        {
            _move = false;
            GetComponent<AudioSource>().Stop();
        }
    }

    void InputKey()
    {
        _vectorMove = transform.forward * Input.GetAxis("Vertical");
        _vectorMove += transform.right * Input.GetAxis("Horizontal");
    }

    void Move()
    {
        if (_vectorMove != Vector3.zero)
        {
            Sound(true);
            _hideBegin = true;
        }
        else
        {
            Sound(false);
        }
        
        RotationAxis();
        _characterController.Move(_vectorMove * Speed * Time.deltaTime);
    }

    void RotationAxis()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * SpeedRotate, Space.World);
        Camera.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * -SpeedRotate);
    }
}
