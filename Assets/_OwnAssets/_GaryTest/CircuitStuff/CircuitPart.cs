using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CircuitPart : MonoBehaviour {
	public GridSystem m_gridSystem { get; private set; }

	[HideInInspector] public bool isPlaced = false;
	private CircuitPart _To;
	private CircuitPart _From;
	//If this part is connected to a battery
	//Or another part that can pass it a charge from a battery
	[HideInInspector] public bool isConnected = false;

	[HideInInspector] public Vector3 snapArea;

	[Header ("Circuit Stuff")]
	//Starting and Ending of part for electricity visuals
	//Flows from Positive => Negative
	[SerializeField] private Transform positive;
	[SerializeField] private Transform negative;
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
	}
	public ChargeLevel Charge;

	public Transform Positive {
		get { return positive; }
	}

	public Transform Negative {
		get { return negative; }
	}

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
	[SerializeField] private float hP;
	public float HP {
		get { return hP; }
	}

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
	public void RemoveSelfFromGridSystem () {
		m_gridSystem.RemoveFromGridSystem (this.gameObject);
	}
}