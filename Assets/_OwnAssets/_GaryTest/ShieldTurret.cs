using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTurret : Buildings {
    [SerializeField] private float maxRechargeSpeed;
    [SerializeField] private float damagedCooldownTime;

    //#7EBDFF blue  Healthy
    //#CB2E26 red   Damaged
    //#4BFF00 green Recharging
    [SerializeField] private GameObject shieldBarrier;
    private Material shieldMaterial;
    private float maxPossibleHP;
    private float rechargeSpeed;
    private bool isRecentlyDamaged;
    private float damagedTimer;
    private Color Healthy, Damaged, Recharging;

    public override void Start () {
        base.Start ();

        CalculateShieldHealth ();
        currentHP = maxPossibleHP;
        shieldMaterial = shieldBarrier.GetComponent<Material>();
        ColorUtility.TryParseHtmlString ("#7EBDFF", out Healthy);
        ColorUtility.TryParseHtmlString ("#CB2E26", out Damaged);
        ColorUtility.TryParseHtmlString ("#4BFF00", out Recharging);
    }

    public override void Update () {
        base.Update ();

        if (isPlaced) {
            //Test
            Debug.Log("max possible hp " + maxPossibleHP);
            Debug.Log("current hp " + currentHP);

            if (Input.GetKeyDown (KeyCode.X)) {
                TakeDamage (10);
            }

            if (Charge.CurrentCharge == 0) 
                return;

            CalculateShieldHealth ();
            CalculateRechargeSpeed ();

            if (!isRecentlyDamaged) {
                //Recharge shield health till max
                ShieldRecharge ();
            } else {
                damagedTimer -= Time.deltaTime;
                if (damagedTimer <= 0) {
                    isRecentlyDamaged = false;
                }
            }
        }
    }

    void ShieldRecharge () {
        if (currentHP == maxPossibleHP) {
            shieldMaterial.color = Healthy;
            return;
        }

        if (currentHP > maxPossibleHP)
        {
            currentHP = maxPossibleHP;
            shieldMaterial.color = Healthy;
            return;
        }

        if (currentHP < maxPossibleHP) {
            currentHP += maxRechargeSpeed * Time.deltaTime;
            shieldMaterial.color = Recharging;
        }
    }

    void TriggerCooldown () {
        isRecentlyDamaged = true;
        damagedTimer = damagedCooldownTime;
        shieldMaterial.color = Damaged;
    }

    public override void TakeDamage (float amount) {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
            return;
        }
        TriggerCooldown ();
    }

    public override void Die()
    {
        base.Die();
    }

    void CalculateShieldHealth () {
        maxPossibleHP = MaxHP * Charge.ChargePercentage ();
    }

    void CalculateRechargeSpeed () {
        rechargeSpeed = maxRechargeSpeed;
    }
}