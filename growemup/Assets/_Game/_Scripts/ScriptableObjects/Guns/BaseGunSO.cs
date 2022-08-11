using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "baseGun", menuName = "ScriptableObjects/Guns/BaseGun")]
public class BaseGunSO : ScriptableObject
{
    public int ammoAmount = 8;
    public BaseBullet bulletPrefab;
    
    public float recoilForce = 5;
    public float hapticForce = 5;
    public float hapticDuration = .1f;
}
