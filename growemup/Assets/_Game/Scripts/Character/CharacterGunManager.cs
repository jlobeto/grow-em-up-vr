using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterGunManager : MonoBehaviour
{
    [SerializeField] private InputActionReference rightHandTrigger;
    [SerializeField] private InputActionReference leftHandTrigger;
    [SerializeField] private GameEvent gunTriggeredEvent;
    [SerializeField] private GameEvent gunReleasedTriggerEvent;

    public void GunTook(SelectEnterEventArgs args)
    {
        if (IsInteractingWithGun(args))
        {
            rightHandTrigger.action.performed += RightHandTriggerPerformed;
            leftHandTrigger.action.performed += LeftHandTriggerPerformed;
        }
    }

    public void GunDropped(SelectExitEventArgs args)
    {
        if (IsInteractingWithGun(args))
        {
            rightHandTrigger.action.performed -= RightHandTriggerPerformed;
            leftHandTrigger.action.performed -= LeftHandTriggerPerformed;
        }
    }

    private void RightHandTriggerPerformed(InputAction.CallbackContext obj)
    {
        var val = obj.ReadValue<float>();
        TryShoot(val);
        TryRelease(val);
    }

    private void LeftHandTriggerPerformed(InputAction.CallbackContext obj)
    {
        var val = obj.ReadValue<float>();
        TryShoot(val);
        TryRelease(val);
    }

    void TryShoot(float triggerValue)
    {
        if(!IsTriggerPressed(triggerValue))
            return;
        
        gunTriggeredEvent.Raise();
    }

    private void TryRelease(float val)
    {
        if (IsTriggerReleased(val))
        {
            gunReleasedTriggerEvent.Raise();
        }
    }

    bool IsInteractingWithGun(BaseInteractionEventArgs args)
    {
        BaseGun gun;
        return args.interactableObject.transform.TryGetComponent(out gun);
    }
    
    bool IsTriggerPressed(float value)
    {
        return value > .9f;
    }

    bool IsTriggerReleased(float value)
    {
        return value < .1f;
    }
}
