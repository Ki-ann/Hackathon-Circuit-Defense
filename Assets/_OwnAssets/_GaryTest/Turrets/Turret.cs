﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : CircuitPart {
	[Header ("Attack Stats")]
	[Tooltip ("Attack Radius of tower")]
	[SerializeField] private float attackRadius;
	[SerializeField] private float attackSpeed;
	[SerializeField] private float attackDamage;
	private GameObject targetToAttack;
	public override void Update () {
		base.Update ();
		if (isPlaced) 
		{
			//Turret Behvaiours
			if (targetToAttack == null)
				CheckRadius ();

			if (targetToAttack != null)
				Attack ();

		}
	}

	void CheckRadius () {
		Collider[] colInRadius = Physics.OverlapSphere (visual.transform.position, attackRadius);

		foreach (Collider col in colInRadius) {
			//check if col is enemy, if yes than target and attack
			if (col.GetComponent<EnemyAI> () != null) {
				targetToAttack = col.gameObject;
				return;
			}
		}
	}

	void Attack () {

	}

	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (visual.transform.position, attackRadius);
	}
}