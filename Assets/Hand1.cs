using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Hand1 : MonoBehaviour

{

    public ActionBasedController controller;
    public float followSpeed = 45f;
    public float rotateSpeed = 45f;

    public Vector3 positionOffset; 
    public Vector3 rotationOffset;

    public Transform palm;
    public float reachDistance = 0.1f, joinDistance = 0.05f;
    public LayerMask grabbableLayer;

    private Transform _followTarget;
    private Rigidbody _body;

    private bool _grabbed;
    private GameObject _grabObject;
    private Transform _grabPoint;
    private FixedJoint _nivel1, _nivel2;



    void Start()
    {
        
        

        _followTarget = controller.gameObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate; 
        _body.mass = 40f;
        _body.maxAngularVelocity = 20f;

        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Release;

        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;
    }

    void Update()
    {
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        var positionWithOffset = _followTarget.TransformPoint(positionOffset);

        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);
        
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);

        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);

    }

    private void Grab(InputAction.CallbackContext context)
    {

        if (_grabbed || _grabObject ) return;
       
        Collider[] grabbableColliders = Physics.OverlapSphere(palm.position, reachDistance, grabbableLayer);
        if (grabbableColliders.Length < 1) return;

        var objectToGrab = grabbableColliders[0].transform.gameObject;
        var objectBody = objectToGrab.GetComponent<Rigidbody>();

        if (objectBody != null)
        {
            _grabObject = objectBody.gameObject;
        }

        else
        {
            objectBody = objectToGrab.GetComponentInParent<Rigidbody>();
            
            if (objectBody != null)
            {

                _grabObject = objectBody.gameObject;

            }

            else
            {
                return;
            }
        }

        StartCoroutine(GrabObject(grabbableColliders[0], objectBody));

    }


    private IEnumerator GrabObject(Collider collider, Rigidbody targetBody)
    {
        _grabbed = true;
        
        _grabPoint = new GameObject().transform;
        _grabPoint.position = collider.ClosestPoint(palm.position);
        _grabPoint.parent = _grabObject.transform;

        _followTarget = _grabPoint;

        while (_grabPoint !=null && Vector3.Distance(_grabPoint.position, palm.position)> joinDistance && _grabbed)
        {
            yield return new WaitForEndOfFrame();
        }

        _body.velocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;
        targetBody.velocity = Vector3.zero;
        targetBody.angularVelocity = Vector3.zero;

        targetBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        targetBody.interpolation = RigidbodyInterpolation.Interpolate;

        _nivel1 = gameObject.AddComponent<FixedJoint>();
        _nivel1.connectedBody = targetBody;
        _nivel1.connectedMassScale = 1;
        _nivel1.massScale  = 1;
        _nivel1.enableCollision = false;
        _nivel1.enablePreprocessing = false;

        _nivel2 = gameObject.AddComponent<FixedJoint>();
        _nivel2.connectedBody = targetBody;
        _nivel2.connectedMassScale = 1;
        _nivel2.massScale  = 1;
        _nivel2.enableCollision = false;
        _nivel2.enablePreprocessing = false;

        _followTarget = controller.gameObject.transform;
        
    }

    private void Release(InputAction.CallbackContext context)
    {
        if (_nivel1 != null)
        {
            Destroy (_nivel1);
        }
            

        if (_nivel1 != null)
        {
            Destroy (_nivel2);
        }

        if (_grabPoint != null)
        {
            Destroy (_grabPoint.gameObject);
        }

        if (_grabObject != null)
        {
            var targetBody = _grabObject.GetComponent<Rigidbody>();
            targetBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            targetBody.interpolation = RigidbodyInterpolation.None;
            _grabObject = null;
        }

        _grabbed = false;
        _followTarget = controller.gameObject.transform;
    }

}