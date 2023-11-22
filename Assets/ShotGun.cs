using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotGun : WeaponBase
{
    [SerializeField] private Projectile myBullet;
    
    public int MagSize = 10;
    public int CurrentAmmo = 10;
    public int Pellets = 10;

    public TMP_Text ammoText;

    public Player player;

    public string name = "ShotGun";

    public bool isPickedUp = false;

    public AudioSource mAudio;


    public void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag == "Player1")
        {
            if (isPickedUp == false)
            {
                player = other.gameObject.GetComponent<Player>();
                player.pickUp(name);
                isPickedUp = true;
            }
            
            //Destroy(gameObject);
        }
    }

    public void UpdateAmmoCount()
    {
        ammoText.text = CurrentAmmo + " / " + MagSize;
    }

    protected override void Attack(float percent)
    {

        if (CurrentAmmo > 0)
        {
            
            if (((int)player.PowerUp & (int)Player.EActivePowerUp.InfiniteAmmo) != (int)Player.EActivePowerUp.InfiniteAmmo)
            {
                CurrentAmmo--;
            }
            
            for (int i = 0; i < Pellets; i++)
            {
                print("My weapon attacked " + percent);
                Ray camRay = InputManager.GetCameraRay();
                Projectile rb = Instantiate(myBullet, camRay.origin, transform.rotation);

                rb.Init(percent, camRay.direction);
                mAudio.Play();
                if (((int)player.PowerUp & (int)Player.EActivePowerUp.InfiniteAmmo) != (int)Player.EActivePowerUp.InfiniteAmmo)
                {
                    UpdateAmmoCount();
                }


                Destroy(rb.gameObject, 1);
            }
            
            
        }
    }
}
