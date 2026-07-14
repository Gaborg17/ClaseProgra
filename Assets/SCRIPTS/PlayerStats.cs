using UnityEngine;
using Fusion;

public class PlayerStats : NetworkBehaviour
{
    [Networked] public int Kills { get; set; }
    [Networked] public int Deaths { get; set; }

    public void AddKill()
    {
        Kills++;
    }

    public void AddDeath()
    {
        Deaths++;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddKill()
    {
        if (Object.HasStateAuthority)
        {
            AddKill();
        }
    }
}
