using Fusion;
using Fusion.Addons.SimpleKCC;
using UnityEngine;

public enum Team { Red, Blue }

public class TeamHandler : NetworkBehaviour
{
    [Networked] public Team team { get; set; }
    [Networked] public float timeInZone {  get; set; }

    private SpawnPointManager spawnPointManager;
    private SimpleKCC simpleKCC;

    public override void Spawned()
    {
        spawnPointManager = FindAnyObjectByType<SpawnPointManager>();
        simpleKCC = GetComponent<SimpleKCC>();

        if (Runner.IsClient)
        {
            team = Team.Red;
            MoveToSpawn();
            
        }
        else if (Runner.IsServer)
        {
            team = Team.Blue;
            MoveToSpawn();
        }
    }


    public void MoveToSpawn()
    {
        simpleKCC.SetPosition(spawnPointManager.SetSpawn(team));
    }
}
