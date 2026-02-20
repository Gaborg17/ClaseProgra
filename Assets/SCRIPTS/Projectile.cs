using Fusion;
using System.Threading.Tasks;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask mask;


    private Rigidbody rb;


    public override void Spawned()
    {
        rb = GetComponent<Rigidbody>();
        AddForce();
    }



    private void AddForce()
    {
        if (rb != null)
        {
            rb.AddForce(transform.forward * speed, ForceMode.Force);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Damageable"))
        {
            RPC_DestroyOnCollision(this.Object);
        }

    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_DestroyOnCollision(NetworkId networkId)
    {
        if (Runner.IsServer)
        {
            if (Runner.TryFindObject(networkId, out NetworkObject networkObject))
            {
                Runner.Despawn(networkObject);
            }
        }
    }
    
}
