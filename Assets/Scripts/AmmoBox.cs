using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public Player player;

    
    public void OnCollisionEnter(Collision other)
    {
        
        if (other.transform.tag == "Player1")
        {
            player = other.gameObject.GetComponent<Player>();
            if (player.AmmoBag < player.AmmoBagCap)
            {
                player.IncreaseAmmo();
                Destroy(gameObject);
            }
            
        }
    }
}

