using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Turret : Buildings, IHaveAttack, ICircuitNeighbour ,IBattery{
	public Battery connectedBattery {get;set;}
	[SerializeField] private GameObject barrel;
	public GameObject Barrel {
		get { return barrel; }
	}

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

	public override void Update () {
		base.Update ();
		if (isPlaced) {
			//Test
			if (Input.GetKeyDown (KeyCode.Z)) {
				Charge.ChargeLevelChange (5);
			}

			if (Charge.CurrentCharge == 0)
				return;

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
	    private void CheckNeighboursForConnection () {
        CircuitPart[] neighbouringParts = this.m_gridSystem.GetNeighbouringParts (this.snapArea);
        int partIndex = -1;
        
        switch (_Positive) {
            case poles.NONE:
                partIndex = -1;
                PositivePart = null;
                break;
            case poles.UP:
                if (neighbouringParts[0]) {
                    partIndex = 0;
                }
                break;
            case poles.RIGHT:
                if (neighbouringParts[1]) {
                    partIndex = 1;
                }
                break;
            case poles.DOWN:
                if (neighbouringParts[2]) {
                    partIndex = 2;
                }
                break;
            case poles.LEFT:
                if (neighbouringParts[3]) {
                    partIndex = 3;
                }
                break;

        }
        if (partIndex >= 0) {
            PositivePart = neighbouringParts[partIndex];
        } else {
            PositivePart = null;
        }

        partIndex = -1;
        switch (_Negative) {
            case poles.NONE:
                partIndex = -1;
                NegativePart = null;
                break;
            case poles.UP:
                if (neighbouringParts[0]) {
                    partIndex = 0;
                }
                break;
            case poles.RIGHT:
                if (neighbouringParts[1]) {
                    partIndex = 1;
                }
                break;
            case poles.DOWN:
                if (neighbouringParts[2]) {
                    partIndex = 2;
                }
                break;
            case poles.LEFT:
                if (neighbouringParts[3]) {
                    partIndex = 3;
                }
                break;
        }
        if (partIndex >= 0) {
            NegativePart = neighbouringParts[partIndex];
        } else {
            NegativePart = null;
        }
    }

    public override void NeighbourAction () {
        CheckNeighboursForConnection ();
        SetupConnections ();
    }

    private void SetupConnections () {
        if (PositivePart && NegativePart) {
            _From.Add (NegativePart);
            _To.Add (PositivePart);

            this.LinkToNextNode ();
        }
    }

    public override void LinkToNextNode () {
        CheckNeighboursForConnection ();
        if (PositivePart != null) {
            if (PositivePart.GetComponent<Wire> ()) {
                if (!this._To.Contains (PositivePart)) this._To.Add (PositivePart);
            }
        }
        if (NegativePart != null) {
            if (NegativePart.GetComponent<Wire> ()) {
                //flip negative side hack
                //if (!this._From.Contains (NegativePart)) this._From.Add (NegativePart);
                NegativePart._From = new List<CircuitPart> (NegativePart._To);
                NegativePart._To.Clear ();
                NegativePart._To.Add (this);
            }
        }
	}
	void CalculateAttackSpeed () {
		float PercentageCharge = Charge.ChargePercentage ();
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
		barrel.transform.LookAt (targetToAttack.transform);
		barrel.transform.eulerAngles = new Vector3 (0, barrel.transform.eulerAngles.y, 0);

	}
	public virtual IEnumerator TryAttack () { yield return new WaitForSeconds (0); }

	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (visual.transform.position, attackRadius);
	}
}