using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] private BaseBulletSO baseBulletSO;

    protected bool isMoving;
    
    void Start()
    {
        isMoving = true;
    }

    void Update()
    {
        if(isMoving)
            transform.position += transform.forward * (Time.deltaTime * baseBulletSO.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"HIT: layer:{LayerMask.LayerToName(other.gameObject.layer)}");
        if(other.gameObject.layer != LayerMask.NameToLayer("gun"))
            isMoving = false;
    }
}
