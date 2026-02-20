using Fusion;
using System;
using UnityEngine;
using UnityEngine.Windows;

public class WeaponHandler : NetworkBehaviour
{
    public Weapons weaponInHand;

    private InputInfo input;

    Action Shoot;

    public void ShootWeapon()
    {
        if (input.isFirePressed)
        {
            Shoot();
        }
    }
    private void Start()
    {
        switch (weaponInHand._mode._type)
        {
            case ShootMode.ShootType.Raycast:
                Shoot = weaponInHand.RaycastShoot;
                break;
            case ShootMode.ShootType.Fisico:
                Shoot = weaponInHand.PhysyicalShoot;
                break;
            default:
                Debug.Log("Sin tippo asignado");
                break;
        }
    }


    public void ReloadWeapon()
    {
        if (input.isReloadPressed)
        {
            weaponInHand.Reload();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (HasInputAuthority && GetInput(out input))
        {
            ShootWeapon();
            ReloadWeapon();
        }
    }
}
