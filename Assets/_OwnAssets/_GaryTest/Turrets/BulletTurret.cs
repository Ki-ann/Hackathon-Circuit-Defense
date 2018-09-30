﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : Turret {
    private LineRenderer bulletPath;
    private AudioSource shootSound;

    public override void Start () {
        base.Start ();
        bulletPath = GetComponent<LineRenderer> ();
        shootSound = GetComponent<AudioSource> ();
    }
    public override IEnumerator TryAttack () {
        isAttacking = true;
        RaycastHit hit;
        if (Physics.Raycast (FirePoint.transform.position, Barrel.transform.forward, out hit, AttackRadius)) {
            if (hit.collider.GetComponent<EnemyAI> () != null) {
                //Attack 
                hit.collider.GetComponent<EnemyAI> ().TakeDamage (AttackDamage);
                shootSound.Play ();
                StartCoroutine (DrawLine (hit.point));
            }
        } else {
            shootSound.Play ();
            StartCoroutine (DrawLine (FirePoint.transform.forward * AttackRadius));
        }
        yield return new WaitForSeconds (AttackSpeed);
        isAttacking = false;
    }

    IEnumerator DrawLine (Vector3 endPosition) {
        Debug.DrawRay (FirePoint.transform.position, Barrel.transform.forward * AttackRadius, Color.red);
        bulletPath.enabled = true;
        Vector3[] lineStartEnd = { FirePoint.transform.position, endPosition };
        bulletPath.SetPositions (lineStartEnd);
        yield return new WaitForSeconds (0.1f);
        bulletPath.enabled = false;
    }
}