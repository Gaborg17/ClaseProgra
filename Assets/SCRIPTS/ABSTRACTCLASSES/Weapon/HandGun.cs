using Fusion;
using UnityEngine;
using static Unity.Collections.Unicode;

public class HandGun : Weapons
{
    private InputInfo info;



    public override void RaycastShoot()
    {
        RPC_RaycastShoot();
        
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_RaycastShoot(RpcInfo info = default)
    {

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, weaponRange, damageableMask))
        {
            Debug.Log($"Disparando a {hit.collider.name}");
            if(hit.collider.TryGetComponent(out Vida vida))
            {
                vida.RPC_TakeDamage(bulletDamage, info.Source);
            }
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

        NetworkObject bulllet = Runner.Spawn(bulletPrefab, pos, rotation, Object.InputAuthority);
        bulllet.GetComponent<Projectile>().SetData(info.Source,bulletDamage);

    }
}
