using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CircuitPart : MonoBehaviour, ITakeDamage {
	public GridSystem m_gridSystem { get; private set; }

	[HideInInspector] public bool isPlaced = false;
	[HideInInspector] public List<CircuitPart> _To = new List<CircuitPart> ();
	[HideInInspector] public List<CircuitPart> _From = new List<CircuitPart> ();
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
		m_gridSystem = FindObjectOfType<GridSystem> ();
	}

	public virtual void Update () {
		snapArea = m_gridSystem.GetNearestPosition (target.transform.position);
		visual.transform.position = snapArea;
	}

	public virtual void TakeDamage (float amount) {
		currentHP -= amount;
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
	}

	// Should be called when the part gets destroyed
	public virtual void RemoveSelfFromGridSystem () {
		m_gridSystem.RemoveFromGridSystem (this.gameObject);
	}

	public virtual void LinkToNextNode () {
		CircuitPart[] neighbouringParts = this.m_gridSystem.GetNeighbouringParts (this.snapArea).Where (x => x != null).ToArray ();
		for (int i = 0; i < neighbouringParts.Length; i++) {
			if (!_From.Contains (neighbouringParts[i])) {
				_To.Add (neighbouringParts[i]);
				neighbouringParts[i]._From.Add (this);
			}
			//Stop linking at the end
			if (!neighbouringParts[i]._To.Any ())
				neighbouringParts[i].LinkToNextNode ();
		}

	}

	public void ClearLinks () {
		_From = new List<CircuitPart> ();
		_To = new List<CircuitPart> ();
	}
}