using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour, IDamagable
{
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] private float speed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(Vector3.forward * speed);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    

}
