using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : CircuitPart {
	[Tooltip ("Attack Radius of tower")]
	[SerializeField] private float attackRadius; 
	[SerializeField] private float attackSpeed;
	[SerializeField] private float attackDamage;
	private GameObject targetToAttack;
	public override void Update() {
		base.Update();
		//Turret Behvaiours
		if (targetToAttack != null)
			Attack();
	}

	void CheckRadius () 
	{
		Collider[] colInRadius = Physics.OverlapSphere (gameObject.transform.position, attackRadius);

		foreach (Collider col in colInRadius)
		{
			//check if col is enemy, if yes than target and attack
		}
	}

	void Attack() 
	{

	}

	void OnDrawGizmos()
	{
		
	}
}
