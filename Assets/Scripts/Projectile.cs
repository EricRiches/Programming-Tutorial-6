using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float shootForce;
    [SerializeField] private Rigidbody rb;

    private float trueDamage;

    [SerializeField] private Player player;
    private GameObject playerObject;


        
    public void Init(float chargePercent, Vector3 fireDirection)
    {
        rb.AddForce(shootForce * chargePercent * fireDirection, ForceMode.Impulse);
        trueDamage = chargePercent * damage;
    }
    private void OnCollisionEnter(Collision other)
    {
        player = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        if (other.gameObject.transform.tag != "bullet")
        {
            if (other.transform.root.TryGetComponent(out IDamagable damagable))
            {
                switch (other.transform.tag)
                {
                    
                    case "Head":
                        if (((int)player.PowerUp & (int)Player.EActivePowerUp.Damage) == (int)Player.EActivePowerUp.Damage)
                {
                            trueDamage *= 2f * 2;
                        }
                        else
                        {
                            trueDamage *= 2f;
                        }
                           
                        break;
                    case "Limb":
                        if (((int)player.PowerUp & (int)Player.EActivePowerUp.Damage) == (int)Player.EActivePowerUp.Damage)
                {
                            trueDamage *= 0.8f * 2;
                        }
                        else
                        {
                            trueDamage *= 0.8f;
                        }
                        break;
                    

                }

                print(trueDamage);
                damagable.TakeDamage(trueDamage);
            }
            Destroy(gameObject);
        }
        
    }
}
