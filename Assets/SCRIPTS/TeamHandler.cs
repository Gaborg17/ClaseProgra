using Fusion;
using UnityEngine;

public enum Team { Red, Blue }

public class TeamHandler : NetworkBehaviour
{
    [Networked] public Team team { get; set; }
    [Networked] public float timeInZone {  get; set; }

    
}
