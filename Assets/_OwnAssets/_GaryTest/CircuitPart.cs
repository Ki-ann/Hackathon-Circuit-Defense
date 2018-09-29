using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CircuitPart : MonoBehaviour {
	private GridSystem m_gridSystem;
	public GridSystem m_GridSystem { get { return m_gridSystem; } }

	private CircuitPart _To;
	private CircuitPart _From;

	private int cost;
	public int Cost 
	{
		get { return cost; }
	}
	public struct ChargeLevel
	{
		private float maxCharge, minCharge;
		public float MaxCharge 
		{
			get { return maxCharge; }
		}

		public float MinCharge 
		{
			get { return minCharge; }
		}

		public float CurrentCharge;
	}
	public ChargeLevel Charge;
	//If this part is connected to a battery
	//Or another part that can pass it a charge from a battery
	[HideInInspector] public bool isConnected = false;

	[HideInInspector] public Vector3 snapArea;

	//Starting and Ending of part for electricity visuals
	//Flows from Positive => Negative
	public Transform Positive, Negative;
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
		m_gridSystem.AddToGridSystem (snapArea, this.gameObject);
	}

	// Should be called when the part gets destroyed
	public void RemoveSelfFromGridSystem () {
		m_gridSystem.RemoveFromGridSystem (this.gameObject);
	}
}