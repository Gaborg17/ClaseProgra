using Fusion;
using Fusion.Addons.SimpleKCC;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Vida : NetworkBehaviour
{
    [SerializeField] private byte currentHealth;
    [Networked]private byte _currentHealth { get; set;}
    [SerializeField]private byte maxhealth;
    [SerializeField]private Image healthBar;

    [SerializeField] private UnityEvent OnDeath;

    private SimpleKCC simpleKCC;

    

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(byte damage, PlayerRef shooter)
    {
        currentHealth -= damage;

        //healthBar.fillAmount = (float)currentHealth / maxhealth;

        if(currentHealth <= 0)
        {
            OnDeath?.Invoke();
            Object.gameObject.SetActive(false);
            simpleKCC = GetComponent<SimpleKCC>();
            simpleKCC.SetPosition(new Vector3(10,1,10));
        }
    }


}
