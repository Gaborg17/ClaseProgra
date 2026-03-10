using Fusion;
using System.Threading.Tasks;
using UnityEngine;

public class TriggerParticles : NetworkBehaviour
{
    [SerializeField] private NetworkPrefabRef particlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (!Runner.IsServer)
            return;

        if (other.CompareTag("Player"))
        {
            NetworkObject playerNetObj = other.GetComponentInParent<NetworkObject>();


            if (playerNetObj != null)
            {
                NetworkObject particle = Runner.Spawn(particlePrefab);
                NetworkTransform partTr = particle.GetComponent<NetworkTransform>();
                particle.transform.SetParent(playerNetObj.transform, false);
                partTr.SyncParent = true;
                particle.transform.localPosition = Vector3.zero;

                Object.gameObject.SetActive(false);
                DespawnParticle(particle);
            }
        }
        
    }

    private async void DespawnParticle(NetworkObject part)
    {
        await Task.Delay((int)(3 * 1000));
        Runner.Despawn(part);
        Runner.Despawn(Object);

    }

}
