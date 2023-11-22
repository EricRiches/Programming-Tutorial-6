using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private Projectile myBullet;
    [SerializeField] private Projectile myBullet2;
    public int MagSize = 10;
    public int CurrentAmmo = 10;

    public TMP_Text ammoText;

    public Player player;

    public AudioSource mAudio;

    public void UpdateAmmoCount()
    {
        ammoText.text = CurrentAmmo + " / " + MagSize;
    }

    protected override void Attack(float percent)
    {
        if (CurrentAmmo > 0)
        {
            

            print("My weapon attacked " + percent);
            Ray camRay = InputManager.GetCameraRay();
            Projectile rb = Instantiate(percent > 0.5f ? myBullet2 : myBullet, camRay.origin, transform.rotation);
            rb.Init(percent, camRay.direction);
            if (((int)player.PowerUp & (int)Player.EActivePowerUp.InfiniteAmmo) != (int)Player.EActivePowerUp.InfiniteAmmo)
            {
                CurrentAmmo--;
                UpdateAmmoCount();
            }
            mAudio.Play();
            Destroy(rb.gameObject, 5);
        }
    }

    
}
