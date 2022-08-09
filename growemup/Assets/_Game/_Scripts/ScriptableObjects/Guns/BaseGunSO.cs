using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "baseGun", menuName = "ScriptableObjects/Guns/BaseGun")]
public class BaseGunSO : ScriptableObject
{
    public float reloadTime = 2f;
    public int ammoAmount = 8;
    public BaseBullet bulletPrefab;
    public float recoilForce = 5;
}
