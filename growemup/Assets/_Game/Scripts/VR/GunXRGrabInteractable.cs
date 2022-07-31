using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunXRGrabInteractable : XRGrabInteractable
{
    private BaseGun _baseGun;

    protected override void Awake()
    {
        base.Awake();
        
        _baseGun = GetComponent<BaseGun>();
    }

    protected override void Grab()
    {
        base.Grab();
        _baseGun.GunGrabbed();
    }

    protected override void Drop()
    {
        base.Drop();
        _baseGun.GunDropped();
    }
}
