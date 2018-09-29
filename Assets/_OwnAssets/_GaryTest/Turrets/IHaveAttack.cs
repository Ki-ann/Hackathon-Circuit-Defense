using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveAttack {
	GameObject FirePoint { get; }
	float AttackRadius { get; }
	float AttackSpeed { get; }
	float AttackDamage { get; }
}