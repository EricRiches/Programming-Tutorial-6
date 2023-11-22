using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "MyScriptableObjects/WeaponStats", order = 1)]

public class WeaponSO : ScriptableObject
{
    [Header("Weapon Base Stats")]
    [SerializeField]  private float TimeBetweenAttacks;
    [field: SerializeField] public float ChargeUpTime { get; private set; }
    [field: SerializeField, Range(0, 1)] public float MinChargePercent { get; private set; }
    [field: SerializeField] public bool IsFullyAuto { get; private set; }
    public WaitForSeconds CoolDownWait { get; private set; }

    private void OnEnable()
    {
        CoolDownWait = new WaitForSeconds(TimeBetweenAttacks);
    }
}
