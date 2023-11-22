using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BurstWeapon : WeaponBase
{
    [SerializeField] private Projectile myBullet;
    [SerializeField] private float force = 50;
    [SerializeField] private float burstDelay = 1f;
    [SerializeField] private int burstCount = 3;
    public int MagSize = 10;
    public int CurrentAmmo = 10;

    public TMP_Text ammoText;

    public Player player;
    
    public string name = "BurstWeapon";

    public AudioSource mAudio;


    public void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag == "Player1")
        {
            player = other.gameObject.GetComponent<Player>();
            player.pickUp(name);
            //Destroy(gameObject);
        }
    }

    public void UpdateAmmoCount()
    {
        ammoText.text = CurrentAmmo + " / " + MagSize;
    }

    protected override void Attack(float percent)
    {
        StartCoroutine(BurstAttack(percent));
    }

    private IEnumerator BurstAttack(float percent)
    {
        for (int i = 0; i < burstCount; i++)
        {
            if (CurrentAmmo > 0)
            {
                

                print("My weapon attacked " + percent);
                Ray camRay = InputManager.GetCameraRay();
                Projectile rb = Instantiate(myBullet, camRay.origin, transform.rotation);
                rb.Init(percent, camRay.direction);
                if (((int)player.PowerUp & (int)Player.EActivePowerUp.InfiniteAmmo) != (int)Player.EActivePowerUp.InfiniteAmmo)
                {
                    CurrentAmmo--;
                    UpdateAmmoCount();
                }
                mAudio.Play();

                Destroy(rb.gameObject, 5);
                
                yield return new WaitForSeconds(burstDelay);
            }
        }
    }
    
}
