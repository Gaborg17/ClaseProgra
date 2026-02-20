using Fusion;
using UnityEngine;
using static Unity.Collections.Unicode;

public class HandGun : Weapons
{
    private InputInfo info;



    public override void RaycastShoot()
    {

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, weaponRange, damageableMask))
        {
            Debug.Log($"Disparando a {hit.collider.name}");
        }
    }

    public override void PhysyicalShoot()
    {
        RPC_PhysicalShoot(bulletOrigin.transform.position, bulletOrigin.transform.rotation);

    }

    public override void Reload()
    {
        Debug.Log("Reloading");
    }

    private void OnDrawGizmos()
    {
        if (_mode._type == ShootMode.ShootType.Raycast && drawRay == true)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(bulletOrigin.position, bulletOrigin.forward * weaponRange);
        }
    }

    [Rpc(RpcSources.InputAuthority,RpcTargets.StateAuthority)]
    private void RPC_PhysicalShoot(Vector3 pos, Quaternion rotation,RpcInfo info = default)
    {

        Debug.Log("Disparando objeto");
        if (Runner.IsServer)
        {
            Runner.Spawn(bulletPrefab,pos,rotation,Object.InputAuthority);
        }

    }
}
