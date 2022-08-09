using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [SerializeField] protected BaseGunSO baseGunSO;
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected GameEvent triggerPressedEvent;
    [SerializeField] protected GameEvent gunTriggerReleasedEvent;
    [SerializeField] protected FloatGameEvent gunTriggeredEvent;

    protected int _currentAmmo;
    protected bool _isShooting;

    private void Awake()
    {
        _currentAmmo = baseGunSO.ammoAmount;
    }

    public void GunGrabbed()
    {
        triggerPressedEvent.AddListener(Shoot);
        gunTriggerReleasedEvent.AddListener(ReleaseTrigger);
    }

    public void GunDropped()
    {
        triggerPressedEvent.RemoveListener(Shoot);
        gunTriggerReleasedEvent.RemoveListener(ReleaseTrigger);
    }


    protected void Shoot()
    {
        if (_isShooting)
            return;

        _isShooting = true;
        
        if (_currentAmmo == 0)
        {
            //Debug.LogError("not enough ammo");
            return;
        }
        
        _currentAmmo--;
        
        Instantiate(baseGunSO.bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        gunTriggeredEvent.Raise(baseGunSO.recoilForce);
    }
    
    private void ReleaseTrigger()
    {
        _isShooting = false;
    }
}