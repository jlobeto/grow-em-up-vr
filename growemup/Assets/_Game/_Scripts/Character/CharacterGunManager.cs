using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterGunManager : MonoBehaviour
{
    [SerializeField] private InputActionReference rightHandTrigger;
    [SerializeField] private InputActionReference leftHandTrigger;
    [SerializeField] private RecoilForceSystem[] recoilSystems;
    [SerializeField] private HapticsManager hapticsManager;
    [SerializeField] private GameEvent joystickTriggerPressedEvent;
    [SerializeField] private GameEvent joystickTriggerReleasedTriggerEvent;
    [SerializeField] private StringGameEvent gunTriggeredEvent;
    
    bool _holdingWithRightHand;

    public void GunTook(SelectEnterEventArgs args)
    {
        if (IsInteractingWithGun(args))
        {
            SetGrabbingHand(args);
            
            if(_holdingWithRightHand)
                rightHandTrigger.action.performed += RightHandTriggerPerformed;
            else
                leftHandTrigger.action.performed += LeftHandTriggerPerformed;
            
            gunTriggeredEvent.AddListener(GunTriggeredEvent);
        }
    }

    public void GunDropped(SelectExitEventArgs args)
    {
        if (IsInteractingWithGun(args))
        {
            if(_holdingWithRightHand)
                rightHandTrigger.action.performed -= RightHandTriggerPerformed;
            else
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

        return exists;
    }

    void SetGrabbingHand(BaseInteractionEventArgs args)
    {
        _holdingWithRightHand = args.interactorObject.transform.gameObject.CompareTag("rightController");
    }
    
    bool IsTriggerPressed(float value)
    {
        return value > .9f;
    }

    bool IsTriggerReleased(float value)
    {
        return value < .3f;
    }
    
    private void GunTriggeredEvent(string obj)
    {
        var deserialized = JObject.Parse(obj);
        
        var recoil = recoilSystems.FirstOrDefault(i => i.IsRightHand == _holdingWithRightHand || !i.IsRightHand && !_holdingWithRightHand);
        recoil.Recoil(float.Parse(deserialized["recoilForce"].ToString()));

        float hapticForce = float.Parse(deserialized["hapticForce"].ToString());
        float hapticDuration = float.Parse(deserialized["hapticDuration"].ToString());
        
        Debug.Log($"haptic force {hapticForce}");
        Debug.Log($"haptic dur {hapticDuration}");
        
        hapticsManager.DoHaptic(_holdingWithRightHand, hapticForce, hapticDuration);
        
    }
}
