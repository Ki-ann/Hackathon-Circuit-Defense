﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Turret : CircuitPart {
	[Header ("Attack Stats")]
	[Tooltip ("Attack Radius of tower")]
	[SerializeField] private GameObject firePoint;
	public GameObject FirePoint { get { return firePoint; } }

	[SerializeField] private float attackRadius;
	public float AttackRadius { get { return attackRadius; } }

	private float attackSpeed;
	[SerializeField] private float minAttackSpeed;
	[SerializeField] private float attackSpeedDiff;
	public float AttackSpeed { get { return attackSpeed; } }

	[SerializeField] private float attackDamage;
	public float AttackDamage { get { return attackDamage; } }
	private GameObject targetToAttack;
	private float distanceToTarget;
	[HideInInspector] public bool isAttacking = false;

	public Battery connectedBattery;
	public override void Update () {
		base.Update ();
		if (isPlaced) {
			//Test
			if (Input.GetKeyDown (KeyCode.Z)) {
				Charge.ChargeLevelChange (5);
			}

			//Turret Behvaiours
			CalculateAttackSpeed ();
			CheckRadius ();
			if (targetToAttack != null) {
				LookAtTarget ();
				if (!isAttacking)
					StartCoroutine (TryAttack ());
			}
		}
	}

	void CalculateAttackSpeed () {
		float PercentageCharge = Charge.CurrentCharge / Charge.MaxCharge;
		attackSpeed = minAttackSpeed - attackSpeedDiff * PercentageCharge;
	}

	void CheckRadius () {
		List<Collider> collidersInRadius = Physics.OverlapSphere (visual.transform.position, attackRadius).ToList ();

		if (targetToAttack != null) {
			if (!collidersInRadius.Contains (targetToAttack.GetComponent<Collider> ())) {
				targetToAttack = null;
				isAttacking = false;
			}
		}

		foreach (Collider col in collidersInRadius) {
			//check if col is enemy, if yes than target and attack
			if (col.GetComponent<EnemyAI> () != null) {
				if (targetToAttack == null) {
					SetNewTarget (col.gameObject);
					return;
				}

				float distanceToNewEnemy = Vector3.Distance (visual.transform.position, col.transform.position);
				if (distanceToTarget > distanceToNewEnemy) {
					SetNewTarget (col.gameObject);
				}
			}
		}
	}

	void SetNewTarget (GameObject target) {
		targetToAttack = target.gameObject;
		distanceToTarget = Vector3.Distance (visual.transform.position, targetToAttack.transform.position);
	}

	void LookAtTarget () {
		visual.transform.LookAt (targetToAttack.transform);
	}
	public abstract IEnumerator TryAttack ();

	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (visual.transform.position, attackRadius);
	}
}