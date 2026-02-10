using Fusion;
using UnityEngine;
using UnityEngine.Windows;

public class WeaponHandler : NetworkBehaviour
{
    public Weapons weaponInHand;

    private InputInfo input;

    public void ShootWeapon()
    {
        if (input.isFirePressed)
        {
            ShootType();
        }
    }


    public void ShootType()
    {
        switch (weaponInHand._mode._type)
        {
            case ShootMode.ShootType.Raycast:
                weaponInHand.RaycastShoot();
                break;
            case ShootMode.ShootType.Fisico:
                weaponInHand.PhysyicalShoot(); 
                break;
            default:
                Debug.Log("Sin tippo asignado");
                break;
        }
    }

    public void ReloadWeapon()
    {

    }

    public override void FixedUpdateNetwork()
    {
        if (HasInputAuthority && GetInput(out input))
        {
            ShootWeapon();
        }
    }
}
