using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTurret : Buildings {
    [SerializeField] private float maxRechargeSpeed;
    [SerializeField] private float damagedCooldownTime;

    //#7EBDFF blue  Healthy
    //#CB2E26 red   Damaged
    //#4BFF00 green Recharging
    [SerializeField] private GameObject shield;
    [SerializeField] private Material shieldMaterial;
    private Material m_Material;
    private float maxPossibleHP;
    private float rechargeSpeed;
    private bool isRecentlyDamaged;
    private float damagedTimer;
    private Color Healthy, Damaged, Recharging;

    public override void Start () {
        base.Start ();

        if (isPlaced) {
            CalculateShieldHealth ();
            currentHP = maxPossibleHP;
            m_Material = new Material (shieldMaterial);
            shield.GetComponent<Renderer> ().material = m_Material;
            ColorUtility.TryParseHtmlString ("#7EBDFF", out Healthy);
            ColorUtility.TryParseHtmlString ("#CB2E26", out Damaged);
            ColorUtility.TryParseHtmlString ("#4BFF00", out Recharging);
        }
    }

    public override void Update () {
        base.Update ();

        if (isPlaced) {
            //Test
            // Debug.Log ("max possible hp " + maxPossibleHP);
            // Debug.Log ("current hp " + currentHP);

            if (Input.GetKeyDown (KeyCode.X)) {
                TakeDamage (10);
            }

            if (Charge.CurrentCharge == 0)
                return;

            CalculateShieldHealth ();
            CalculateRechargeSpeed ();

            if (!isRecentlyDamaged) {
                //Recharge shield health till max
                if (currentHP != maxPossibleHP)
                    ShieldRecharge ();
            } else {
                damagedTimer -= Time.deltaTime;
                if (damagedTimer <= 0)
                    isRecentlyDamaged = false;
            }
        }
    }

    void ShieldRecharge () {
        if (currentHP == maxPossibleHP) {
            m_Material.color = Healthy;
            Debug.Log ("Healthy == ");
            return;
        }

        if (currentHP > maxPossibleHP) {
            currentHP = maxPossibleHP;
            m_Material.color = Healthy;
            Debug.Log ("Healthy >");
            return;
        }

        if (currentHP < maxPossibleHP) {
            currentHP += maxRechargeSpeed * Time.deltaTime;
            m_Material.color = Recharging;
            Debug.Log ("recharging");
        }
    }

    void TriggerCooldown () {
        isRecentlyDamaged = true;
        damagedTimer = damagedCooldownTime;
        m_Material.color = Damaged;
        Debug.Log ("damaged");
    }

    public override void TakeDamage (float amount) {
        Debug.Log ("Damaged!");
        currentHP -= amount;
        if (currentHP <= 0) {
            Die ();
            return;
        }
        TriggerCooldown ();
    }

    public override void Die () {
        base.Die ();
    }

    void CalculateShieldHealth () {
        maxPossibleHP = MaxHP * Charge.ChargePercentage ();
    }

    void CalculateRechargeSpeed () {
        rechargeSpeed = maxRechargeSpeed;
    }
}