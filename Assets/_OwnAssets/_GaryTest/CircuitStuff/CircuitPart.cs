using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CircuitPart : MonoBehaviour, ITakeDamage, ICircuitNeighbour, IBattery {
	public GridSystem m_gridSystem { get; private set; }
	public Battery connectedBattery { get; set; }

	[HideInInspector] public bool isPlaced = false;
	public List<CircuitPart> _To = new List<CircuitPart> ();
	public List<CircuitPart> _From = new List<CircuitPart> ();
	[HideInInspector] public Vector3 snapArea;

	[Header ("Circuit Stuff")]
	//Starting and Ending of part for electricity visuals
	//Flows from Positive => Negative
	[SerializeField] private Transform positive;
	[SerializeField] private Transform negative;
	public Transform Positive {
		get { return positive; }
	}

	public Transform Negative {
		get { return negative; }
	}

	[HideInInspector] public bool isConnected;
	public CircuitPart PositivePart;
	public CircuitPart NegativePart;

	public enum poles { UP, DOWN, LEFT, RIGHT, NONE };
 public poles _Positive;
 public poles _Negative;

 [System.Serializable]
 public struct ChargeLevel {
 [SerializeField] private float maxCharge;
 public float MaxCharge {
			get { return maxCharge; }
		}

		[HideInInspector] private float currentCharge;

		public float CurrentCharge {
			get { return currentCharge; }
		}
		public void ChargeLevelChange (float amount) {
			currentCharge += amount;
			if (currentCharge > maxCharge) {
				currentCharge = maxCharge;
			}

			if (currentCharge < 0) {
				currentCharge = 0;
			}
		}

		public float ChargePercentage () {
			return CurrentCharge / MaxCharge;
		}
	}
	public ChargeLevel Charge;

	[Header ("Grid Stuff")]
	public GameObject prefab;

	[SerializeField] private GameObject m_Visual;
	public GameObject visual {
		get { return m_Visual; }
		set { m_Visual = value; }
	}

	[SerializeField] private GameObject m_Target;
	public GameObject target {
		get { return m_Target; }
	}

	[Header ("InGame Turret Stuff")]
	[SerializeField] private float maxHP;
	public float MaxHP {
		get { return maxHP; }
	}

	[HideInInspector] public float currentHP;

	[SerializeField] private int cost;
	public int Cost {
		get { return cost; }
	}

	public virtual void Start () {
		currentHP = maxHP;
		m_gridSystem = FindObjectOfType<GridSystem> ();
	}

	public virtual void Update () {
		snapArea = m_gridSystem.GetNearestPosition (target.transform.position);
		visual.transform.position = snapArea;
	}

	public virtual void TakeDamage (float amount) {
		currentHP -= amount;
		if (currentHP <= 0)
			Die();
	}

	public virtual void Die() {
		RemoveSelfFromGridSystem();
		GetNeighboursToDoAction();
		Destroy(gameObject, 0f);
	}
	public void UpdateTargetPosition (Vector3 position) {
		target.transform.position = position;
		//Update position in the grid dictionary
	}

	public virtual void AddSelfToGridSystem () {
		if (m_gridSystem == null)
			m_gridSystem = FindObjectOfType<GridSystem> ();

		if (m_gridSystem.CheckFreeSpace (snapArea))
			m_gridSystem.AddToGridSystem (snapArea, this.gameObject);

		LinkToNextNode ();
	}

	// Should be called when the part gets destroyed
	public virtual void RemoveSelfFromGridSystem () {
		m_gridSystem.RemoveFromGridSystem (this.gameObject);
	}

	[ContextMenu ("a")]
	public virtual void LinkToNextNode () {
		CircuitPart[] neighbouringParts = this.m_gridSystem.GetNeighbouringParts (this.snapArea).Where (x => x != null).ToArray ();
		var iBatteries = neighbouringParts.Where (x => x.GetComponent<IBattery> () != null && x.GetComponent<IBattery> ().connectedBattery != null).Select (x => x.GetComponent<IBattery> ()).FirstOrDefault ();
		//Debug.Log(iBatteries);
		var ibat = this.GetComponent<IBattery> ();

		if (iBatteries != null && iBatteries.connectedBattery && ibat != null && ibat.connectedBattery) {
			foreach (CircuitPart part in neighbouringParts) {
				if (!_From.Contains (part)) {
					if (!_To.Contains (part))
						_To.Add (part);
				}
			}

		} else if (iBatteries != null && iBatteries.connectedBattery) {
			if (!_From.Contains ((CircuitPart) iBatteries)) {
				if (!_To.Contains ((CircuitPart) iBatteries))
					_From.Add ((CircuitPart) iBatteries);
			}

			if (ibat != null)
				ibat.connectedBattery = iBatteries.connectedBattery;

			foreach (CircuitPart part in neighbouringParts) {
				if (!_From.Contains (part)) {
					if (!_To.Contains (part))
						_To.Add (part);
				}
			}
		}

	}

	public virtual void ClearLinks () {
		_From = new List<CircuitPart> ();
		_To = new List<CircuitPart> ();
	}

	public virtual void GetNeighboursToDoAction () {
		CircuitPart[] neighbouringParts = this.m_gridSystem.GetNeighbouringParts (this.snapArea).Where (x => x != null).ToArray ();
		for (int i = 0; i < neighbouringParts.Length; i++) {
			ICircuitNeighbour neighbour = neighbouringParts[i].GetComponent<ICircuitNeighbour> ();
			if (neighbour != null)
				neighbour.NeighbourAction ();
		}
	}

	public virtual void NeighbourAction () {

	}
	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos () {
		if (isConnected) {
			Gizmos.color = Color.green;
		} else {
			Gizmos.color = Color.red;
		}
		Gizmos.DrawSphere (this.visual.transform.position, 0.2f);
	}
}