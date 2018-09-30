using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTurret : Turret {
    private ILauncher proj;
    private AudioSource rocketFire;

    public override void Start () {
        base.Start ();
        proj = GetComponent<ILauncher> ();
        rocketFire = GetComponent<AudioSource>();
    }

    public override IEnumerator TryAttack () {
        isAttacking = true;
        proj.Launch (this);
        rocketFire.Play();
        yield return new WaitForSeconds (AttackSpeed);
        isAttacking = false;
    }
    
}