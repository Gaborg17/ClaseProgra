using System;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    
    public ShootMode _mode;

    public int actualAmmo;
    public int maxAmmo;
    public float shootCooldown;
    public float rechargeTime;
    public bool canShoot;
    public bool isRecharging;
    public Transform bulletOrigin;
    public int inventoryAmmo;


    [Header("Raycast Mode")]
    public float weaponRange;
    [SerializeField] private bool drawRay;
    public LayerMask damageableMask;
    public float bulletDamage;

    [Header("Physical Mode")]
    public GameObject bulletPrefab;
    public float bulletSpeed;



    public virtual void RaycastShoot()
    {
        Debug.Log("Disparando con Raycast");
        RaycastHit hit;

        if(Physics.Raycast(bulletOrigin.position, bulletOrigin.forward,out hit, weaponRange, damageableMask))
        {
            Debug.Log($"Disparando a {hit.collider.name}" );
        }
    }


    public virtual void PhysyicalShoot()
    {
        Debug.Log("Disparando objeto");
        GameObject bullet = Instantiate(bulletPrefab, bulletOrigin);

        Rigidbody rbB = bullet.GetComponent<Rigidbody>();

        rbB.AddForce(bulletOrigin.forward * bulletSpeed, ForceMode.VelocityChange);
    }

    public abstract void Reload();


    private void OnDrawGizmos()
    {
        if(_mode._type == ShootMode.ShootType.Raycast && drawRay == true)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(bulletOrigin.position, bulletOrigin.forward * weaponRange);
        }
    }
}
