using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{
    [field: SerializeField]  public float Health { get; set; }
    [SerializeField] private GameObject AmmoBox;
    [SerializeField] private GameObject Spawner;
    private bool isDead;


    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        Instantiate(AmmoBox, Spawner.transform.position, transform.rotation);
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
        }

        GetComponent<Animator>().enabled = false;
    }

    
}

