using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : CircuitPart {
	[Header ("Attack Stats")]
	[Tooltip ("Attack Radius of tower")]
	[SerializeField] private GameObject firePoint;
	[SerializeField] private float attackRadius;
	[SerializeField] private float attackSpeed;
	[SerializeField] private float attackDamage;
	private GameObject targetToAttack;

	public override void Update () {
		base.Update ();
		if (isPlaced) {
			//Turret Behvaiours
			if (targetToAttack == null)
				CheckRadius ();

			if (targetToAttack != null)
				TryAttack ();

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

	void TryAttack () {
		LookAtTarget ();
		RaycastHit hit;
		if (Physics.Raycast (firePoint.transform.position, visual.transform.forward, out hit, attackRadius)) {
			Debug.DrawRay (firePoint.transform.position, visual.transform.forward * attackRadius, Color.red);
			if (hit.collider.GetComponent<EnemyAI> () != null) {
				//Attack 
				Debug.Log ("Die fiend");
			}
		}
	}

	void LookAtTarget () {
		visual.transform.LookAt (targetToAttack.transform);
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (visual.transform.position, attackRadius);
	}
}