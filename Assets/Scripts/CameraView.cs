using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public GameObject Player;
    public Light SpotLight;
    public float MinDist = 3.0f;
    public float ValueAngle = 27.5f;

    private Vector3 _direction;
    private bool _isVisible = false;

    void Update()
    {
        _direction = Player.transform.position - transform.position;
        
        Debug.DrawLine(transform.position, Player.transform.position);

        if (Vector3.Magnitude(Player.transform.position - transform.position) > MinDist
            && Vector3.Magnitude(Player.transform.position - transform.position) < SpotLight.range
            && Vector3.Dot(Vector3.Normalize(_direction), transform.forward) > 1.0f - ValueAngle / 180.0f)
        {
            CheckCastPlayer();
        }
        else
        {
            _isVisible = false;
        }
    }

    void CheckCastPlayer()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, _direction);
        float minDist = 1000.0f;
        GameObject minDistObj1 = null;
        GameObject minDistObj2 = null;

        foreach (RaycastHit hit in hits)
        {
            float dist = Vector3.Magnitude(transform.position - hit.transform.position);
            
            if (dist > MinDist
                && dist < minDist)
            {
                minDist = dist;
                
                minDistObj1 = hit.collider.gameObject;
            }
        }
        
        minDist = 1000.0f;
        
        foreach (RaycastHit hit in hits)
        {
            float dist = Vector3.Magnitude(transform.position - hit.transform.position);
            
            if (dist > MinDist
                && dist < minDist
                && hit.collider.gameObject != minDistObj1)
            {
                minDist = dist;
                minDistObj2 = hit.collider.gameObject;
            }
        }
        
        if (minDistObj1
            && minDistObj1.CompareTag("Player"))
        {
            Player.GetComponent<PlayerInvise>().Smoke = 1.0f;
            _isVisible = true;
        }
        else if (minDistObj2
                 && minDistObj2.CompareTag("Player")
                 && minDistObj1.CompareTag("smoke"))
        {
            Player.GetComponent<PlayerInvise>().Smoke = 0.3f;
            _isVisible = true;
        }
        else
        {
            _isVisible = false;
        }
    }

    public bool IsVisible()
    {
        return _isVisible;
    }
}
