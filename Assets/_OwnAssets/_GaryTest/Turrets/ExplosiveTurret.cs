using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTurret : Turret {
    private ILauncher proj;

    public override void Start () {
        base.Start ();
        proj = GetComponent<ILauncher> ();
    }

    public override IEnumerator TryAttack () {
        isAttacking = true;
        proj.Launch (this);
        yield return new WaitForSeconds (AttackSpeed);
        isAttacking = false;
    }
    
}