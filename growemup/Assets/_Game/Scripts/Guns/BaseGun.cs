using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [SerializeField] protected BaseGunSO baseGunSO;
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected GameEvent gunTriggeredEvent;
    [SerializeField] protected GameEvent gunTriggerReleasedEvent;

    protected int _currentAmmo;
    protected bool _isShooting;

    private void Awake()
    {
        _currentAmmo = baseGunSO.ammoAmount;
    }

    public void GunGrabbed()
    {
        gunTriggeredEvent.AddListener(Shoot);
        gunTriggerReleasedEvent.AddListener(ReleaseTrigger);
    }

    public void GunDropped()
    {
        gunTriggeredEvent.RemoveListener(Shoot);
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
    }
    
    private void ReleaseTrigger()
    {
        _isShooting = false;
    }
}
