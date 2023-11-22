using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [Flags]

    public enum EActivePowerUp
    {
        None,
        Speed,
        Damage,
        InfiniteAmmo
    }

    [SerializeField] private WeaponBase myWeapon1;
    [SerializeField] private WeaponBase myWeapon2;
    [SerializeField] private WeaponBase myWeapon3;
    [SerializeField] float speed = 11f;
    private bool weaponShootToggle;

    private bool _usingWeapon1 = true;
    private bool _usingWeapon2;
    private bool _usingWeapon3;

    private Vector3 _moveDirection;

    public ProjectileWeapon projectileWeapon;
    public BurstWeapon burstWeapon;
    public ShotGun shotGun;

    public int AmmoBagCap = 120;
    public int AmmoBag = 60;

    public TMP_Text AmmoBagText;

    public EActivePowerUp PowerUp;

    private void Start()
    {
        InputManager.Init(this);
        InputManager.EnableInGame();

        UpdateAmmoBag();

        if (_usingWeapon1 == true)
        {
            projectileWeapon.UpdateAmmoCount();
        }

        if (_usingWeapon2 == true)
        {
            burstWeapon.UpdateAmmoCount();
        }

        if (_usingWeapon3 == true)
        {
            shotGun.UpdateAmmoCount();
        }
    }

    public void Update()
    {
        if (((int)PowerUp & (int)EActivePowerUp.Speed) == (int)EActivePowerUp.Speed)
        {
            transform.position += speed * Time.deltaTime * _moveDirection * 2;
        }
        else
        {
            transform.position += speed * Time.deltaTime * _moveDirection;
        }
        
    }

    public void SetMoveDirection(Vector3 currentDirection)
    {
        _moveDirection = currentDirection;
    }

    public void Shoot()
    {
        if (_usingWeapon1 == true)
        {
            print("I shot: " + InputManager.GetCameraRay());
            weaponShootToggle = !weaponShootToggle;
            if (weaponShootToggle) myWeapon1.StartShooting();
            else myWeapon1.StopShooting();
        }

        if (_usingWeapon2 == true)
        {
            print("I shot: " + InputManager.GetCameraRay());
            weaponShootToggle = !weaponShootToggle;
            if (weaponShootToggle) myWeapon2.StartShooting();
            else myWeapon2.StopShooting();
        }

        if (_usingWeapon3 == true)
        {
            print("I shot: " + InputManager.GetCameraRay());
            weaponShootToggle = !weaponShootToggle;
            if (weaponShootToggle) myWeapon3.StartShooting();
            else myWeapon3.StopShooting();
        }



    }

    public void SwitchWeapon1()
    {
        if (!_usingWeapon1  && myWeapon1 != null)
        {
            _usingWeapon1 = true;
            _usingWeapon2 = false;
            _usingWeapon3 = false;
            projectileWeapon.UpdateAmmoCount();
        }
    }
    public void SwitchWeapon2()
    {
        if (!_usingWeapon2  && myWeapon2 != null)
        {
            _usingWeapon1 = false;
            _usingWeapon2 = true;
            _usingWeapon3 = false;
            burstWeapon.UpdateAmmoCount();
        }
    }
    public void SwitchWeapon3()
    {
        if (!_usingWeapon3 && myWeapon3 != null)
        {
            _usingWeapon1 = false;
            _usingWeapon2 = false;
            _usingWeapon3 = true;
            shotGun.UpdateAmmoCount();
        }
    }

    public void SwitchPowerUp(EActivePowerUp powerUp)
    {
        
        PowerUp = powerUp;
        
    }


    public void Reload()
    {
        if (_usingWeapon1 == true)
        {
            if (AmmoBag + projectileWeapon.CurrentAmmo >= projectileWeapon.MagSize)
            {
                AmmoBag = AmmoBag - (projectileWeapon.MagSize - projectileWeapon.CurrentAmmo);
                projectileWeapon.CurrentAmmo = projectileWeapon.MagSize;
                
            }
            else
            {
                projectileWeapon.CurrentAmmo = projectileWeapon.CurrentAmmo + AmmoBag;
                AmmoBag = 0;
            }
            projectileWeapon.UpdateAmmoCount();
        }
        if (_usingWeapon2 == true)
        {
            if (AmmoBag + burstWeapon.CurrentAmmo >= burstWeapon.MagSize)
            {
                AmmoBag = AmmoBag - (burstWeapon.MagSize - burstWeapon.CurrentAmmo);
                burstWeapon.CurrentAmmo = burstWeapon.MagSize;
                
            }
            else
            {
                burstWeapon.CurrentAmmo = burstWeapon.CurrentAmmo + AmmoBag;
                AmmoBag = 0;
            }
            burstWeapon.UpdateAmmoCount();
        }

        if (_usingWeapon3 == true)
        {
            if (AmmoBag + shotGun.CurrentAmmo >= shotGun.MagSize)
            {
                AmmoBag = AmmoBag - (shotGun.MagSize - shotGun.CurrentAmmo);
                shotGun.CurrentAmmo = shotGun.MagSize;
                
            }
            else
            {
                shotGun.CurrentAmmo = shotGun.CurrentAmmo + AmmoBag;
                AmmoBag = 0;
            }
            shotGun.UpdateAmmoCount();
        }
        UpdateAmmoBag();
    }

    public void IncreaseAmmo()
    {
        
        AmmoBag = AmmoBag + 30;
        if (AmmoBag > AmmoBagCap) AmmoBag = AmmoBagCap;
        UpdateAmmoBag();
    }

    public void pickUp(string name)
    {
        if(myWeapon1 == null)
        {
            if (name == "ProjectileWeapon")
            {
                myWeapon1 = projectileWeapon;
            }
            if (name == "BurstWeapon")
            {
                myWeapon1 = burstWeapon;
            }
            if (name == "ShotGun")
            {
                myWeapon1 = shotGun;
            }
        } 
        else if(myWeapon2 == null)
        {
            if (name == "ProjectileWeapon")
            {
                myWeapon2 = projectileWeapon;
            }
            if (name == "BurstWeapon")
            {
                myWeapon2 = burstWeapon;
            }
            if (name == "ShotGun")
            {
                myWeapon2 = shotGun;
            }
        }
        else if(myWeapon3 == null)
        {
            if (name == "ProjectileWeapon")
            {
                myWeapon3 = projectileWeapon;
            }
            if (name == "BurstWeapon")
            {
                myWeapon3 = burstWeapon;
            }
            if (name == "ShotGun")
            {
                myWeapon3 = shotGun;
            }
        }


        
    }

    public void UpdateAmmoBag()
    {
        AmmoBagText.text = AmmoBag + " / " + AmmoBagCap;
    }
}
