using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRGrabInteractable : XRGrabInteractable
{
    protected override void Awake()
    {
        interactionManager = FindObjectOfType<XRInteractionManager>();
        base.Awake();
    }
}
