using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    

    public Player player;

    public Player.EActivePowerUp powerUp;

    public void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player1")
        {
            player = other.gameObject.GetComponent<Player>();
            player.SwitchPowerUp(powerUp);
            Destroy(gameObject);

        }

    }
}
