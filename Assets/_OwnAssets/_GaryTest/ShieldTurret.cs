﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTurret : Buildings {
    [SerializeField] private float maxRechargeSpeed;
    [SerializeField] private float damagedCooldownTime;

    //#7EBDFF blue  Healthy
    //#CB2E26 red   Damaged
    //#4BFF00 green Recharging
    [SerializeField] private Material shieldMaterial;
    private float maxPossibleHP;
    private float rechargeSpeed;
    private bool isRecentlyDamaged;
    private float damagedTimer;
    private Color Healthy, Damaged, Recharging;

    public override void Start () {
        base.Start ();

        CalculateShieldHealth ();
        currentHP = maxPossibleHP;
        ColorUtility.TryParseHtmlString ("#7EBDFF", out Healthy);
        ColorUtility.TryParseHtmlString ("#CB2E26", out Damaged);
        ColorUtility.TryParseHtmlString ("#4BFF00", out Recharging);
    }

    public override void Update () {
        base.Update ();
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

    void ShieldRecharge () {
        if (currentHP == maxPossibleHP) {
            shieldMaterial.color = Healthy;
            return;
        }

        if (currentHP != maxPossibleHP) {
            currentHP += maxRechargeSpeed * Time.deltaTime;
            shieldMaterial.color = Recharging;
        }

        if (currentHP > maxPossibleHP) {
            currentHP = maxPossibleHP;
            shieldMaterial.color = Healthy;
        }
    }

    void TriggerCooldown () {
        isRecentlyDamaged = true;
        damagedTimer = damagedCooldownTime;
        shieldMaterial.color = Damaged;
    }

    public override void TakeDamage (float amount) {
        base.TakeDamage (amount);
        TriggerCooldown ();
    }

    void CalculateShieldHealth () {
        maxPossibleHP = MaxHP * Charge.ChargePercentage ();
    }

    void CalculateRechargeSpeed () {
        rechargeSpeed = maxRechargeSpeed;
    }
}