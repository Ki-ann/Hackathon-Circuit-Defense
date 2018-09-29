using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTurret : CircuitPart {
    private float maxPossibleHP;
    public override void Start () {
        base.Start ();
    }

    public override void Update () {
        CalculateShieldHealth ();
        base.Update ();
    }

    void CalculateShieldHealth () {
        maxPossibleHP = MaxHP * Charge.ChargePercentage();
    }
}