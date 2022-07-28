using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterGunManager : MonoBehaviour
{
    [SerializeField] private InputActionReference rightHandTrigger;
    [SerializeField] private InputActionReference leftHandTrigger;

    
    
    private void OnEnable()
    {
        rightHandTrigger.action.performed += RightHandTriggerPerformed;
        leftHandTrigger.action.performed += LeftHandTriggerPerformed;
    }
    private void OnDisable()
    {
        rightHandTrigger.action.performed -= RightHandTriggerPerformed;
        leftHandTrigger.action.performed -= LeftHandTriggerPerformed;
    }

    public void GunSelected(SelectEnterEventArgs args)
    {
        Debug.Log(args.interactableObject.transform.gameObject.name);
    }
    

    private void RightHandTriggerPerformed(InputAction.CallbackContext obj)
    {
        var val = obj.ReadValue<float>();
    }

    private void LeftHandTriggerPerformed(InputAction.CallbackContext obj)
    {
        var val = obj.ReadValue<float>();
    }

    
    bool IsTriggerPressed(float value)
    {
        return value > .9f;
    }
}
