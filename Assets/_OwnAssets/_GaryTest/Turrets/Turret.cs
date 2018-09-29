using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Turret : CircuitPart {
	[Header ("Attack Stats")]
	[Tooltip ("Attack Radius of tower")]
	[SerializeField] private GameObject firePoint;
	[SerializeField] private float attackRadius;
	[SerializeField] private float attackSpeed;
	[SerializeField] private float attackDamage;
	private GameObject targetToAttack;
	private float distanceToTarget;

	public override void Update () {
		base.Update ();
		if (isPlaced) {
			//Turret Behvaiours
			CheckRadius ();
			if (targetToAttack != null)
				TryAttack ();

		}
	}

	void CheckRadius () {
		List<Collider> collidersInRadius = Physics.OverlapSphere (visual.transform.position, attackRadius).ToList ();

		if (targetToAttack != null) {
			Debug.Log (collidersInRadius.Contains (targetToAttack.GetComponent<Collider> ()));
			if (!collidersInRadius.Contains (targetToAttack.GetComponent<Collider> ())) {
				targetToAttack = null;
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