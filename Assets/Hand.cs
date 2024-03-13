using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour

{

    public GameObject piippu;
    public float speed;
    public Rigidbody luoti;

    Animator animator;
    SkinnedMeshRenderer mesh;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;

   
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";



  
    
    public GameObject followObject;
    public float followSpeed = 45f;
    public float rotateSpeed = 45f;

    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    private Transform _followTarget;
    private Rigidbody _body;



    void Start()
    {
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        _followTarget = followObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 40f;

        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;
    }

    void Update()
    {
        AnimateHand();
        
      
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        var positionWithOffset = _followTarget.position + positionOffset;

        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);
        
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);

        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);

    }



    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }


    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
            
        }

        if (triggerCurrent != triggerTarget)
            {
                triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
                animator.SetFloat(animatorTriggerParam, triggerCurrent);
                
            }

       

        if ((gripCurrent==1) && (gripTarget==1) && (triggerTarget==1) && (triggerTarget==1) )
        {
            
            Rigidbody luoti2;
            luoti2 = Instantiate(luoti, piippu.transform.position, Quaternion.identity);
            luoti2.velocity = transform.TransformDirection(Vector3.left * 30);

        }

            
            
    }

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;
    }
}