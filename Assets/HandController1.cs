using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController1 : MonoBehaviour
{
    ActionBasedController controller;
    
    public Hand1 hand;

    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    void Update()
    {
        //hand.SetGrip(controller.selectAction.action.ReadValue<float>());
       // hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
       // hand.SetPrimary(controller.activateAction.action.ReadValue<float>());

        

    

    }
}