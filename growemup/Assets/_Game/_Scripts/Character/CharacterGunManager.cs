using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterGunManager : MonoBehaviour
{
    [SerializeField] private InputActionReference rightHandTrigger;
    [SerializeField] private InputActionReference leftHandTrigger;
    [SerializeField] private RecoilSystem[] recoilSystems;
    [SerializeField] private GameEvent joystickTriggerPressedEvent;
    [SerializeField] private GameEvent joystickTriggerReleasedTriggerEvent;
    [SerializeField] private FloatGameEvent gunTriggeredEvent;
    
    bool _holdingWithRightHand;

    public void GunTook(SelectEnterEventArgs args)
    {
        if (IsInteractingWithGun(args))
        {
            rightHandTrigger.action.performed += RightHandTriggerPerformed;
            leftHandTrigger.action.performed += LeftHandTriggerPerformed;
            gunTriggeredEvent.AddListener(GunTriggeredEvent);
        }
    }

    public void GunDropped(SelectExitEventArgs args)
    {
        if (IsInteractingWithGun(args))
        {
            rightHandTrigger.action.performed -= RightHandTriggerPerformed;
            leftHandTrigger.action.performed -= LeftHandTriggerPerformed;
            gunTriggeredEvent.RemoveListener(GunTriggeredEvent);

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
        
        joystickTriggerPressedEvent.Raise();
    }

    private void TryRelease(float val)
    {
        if (IsTriggerReleased(val))
        {
            joystickTriggerReleasedTriggerEvent.Raise();
        }
    }

    bool IsInteractingWithGun(BaseInteractionEventArgs args)
    {
        BaseGun gun;
        var exists = args.interactableObject.transform.TryGetComponent(out gun);
        
        if (exists)
            _holdingWithRightHand = args.interactorObject.transform.gameObject.CompareTag("rightController"); 
        

        return exists;
    }
    
    bool IsTriggerPressed(float value)
    {
        return value > .9f;
    }

    bool IsTriggerReleased(float value)
    {
        return value < .3f;
    }
    
    private void GunTriggeredEvent(float recoilForce)
    {
        var recoil = recoilSystems.FirstOrDefault(i => i.IsRightHand == _holdingWithRightHand || !i.IsRightHand && !_holdingWithRightHand);
        recoil.Recoil(recoilForce);
    }
}
