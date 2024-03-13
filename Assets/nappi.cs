using System;
using UnityEngine;
using UnityEngine.Events;

public class nappi : MonoBehaviour
{
    [SerializeField] private float threshold =0.01f;
    [SerializeField] private float deadZone = 0.01f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint; 

    public UnityEngine.Events.UnityEvent onPressed, onReleased;
    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }


    void Update()
    {
        if (!_isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        
        if (!_isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }


    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;

            if (Mathf.Abs(value) < deadZone)
            {
                value = 0;
            }
            
        
        return Mathf.Clamp(value, -1f, 1f); 

    }


    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        Debug.Log("pressed");
    }

    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("released");
    }
}
