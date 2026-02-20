using Fusion;
using System;
using UnityEngine;

public abstract class Weapons : NetworkBehaviour
{
    
    public ShootMode _mode;

    public int actualAmmo;
    public int maxAmmo;
    public float fireRate;
    public float reloadTime;
    public bool canShoot;
    public bool isRecharging;
    public Transform bulletOrigin;
    public int inventoryAmmo;


    [Header("Raycast Mode")]
    public float weaponRange;
    protected bool drawRay;
    public LayerMask damageableMask;
    public float bulletDamage;
    [SerializeField] protected Camera playerCamera;

    [Header("Physical Mode")]
    public NetworkPrefabRef bulletPrefab;



    public abstract void RaycastShoot();



    public abstract void PhysyicalShoot();


    public virtual void Reload()
    {
        if(inventoryAmmo <= 0)
        {
            Debug.Log("No ammo left");
            return;
        }
    }



}
