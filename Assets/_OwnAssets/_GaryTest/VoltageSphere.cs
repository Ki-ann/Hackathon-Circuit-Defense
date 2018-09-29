using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoltageSphere : MonoBehaviour {
	public CircuitPart.ChargeLevel charge;
	public int DebugInt;
	public CircuitPart previousPart;
	public CircuitPart nextPart;
	public VoltageSphere masterSphere;
	public List<VoltageSphere> childSpheres = new List<VoltageSphere> ();

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter (Collider other) {
		Buildings building = other.GetComponent<Buildings> ();
		if (building) {
			building.Charge.ChargeLevelChange (charge.CurrentCharge);
		}
	}

	void Update () {
		//if haven't split
		if (!childSpheres.Any ()) {
			// go to next
			if (nextPart != null && previousPart != null) {
				if (Vector3.Distance (transform.position, nextPart.visual.transform.position) <= 0.1f) {
					DebugInt = nextPart._To.Count;
					switch (nextPart._To.Count) {
						case 0:
							GoToNext (null);
							break;
						case 1:
							GoToNext (nextPart._To.FirstOrDefault ());
							break;
						case 2:
						case 3:
						case 4:
							SplitBall (nextPart._To.Count);
							break;
					}

				} //we hit an open circuit, return the voltage to the split spheres
				transform.position = Vector3.Lerp (transform.position, nextPart.visual.transform.position, Time.deltaTime * 2f );
			} else if (nextPart == null && previousPart != null) {

			}
		}
	}
	public void GoToNext (CircuitPart next) {
		previousPart = nextPart;
		nextPart = next;
	}

	public void SplitBall (int size) {
		for (int i = 0; i < size; i++) {
			VoltageSphere mini = Instantiate (this.gameObject, this.transform.position, this.transform.rotation).GetComponent<VoltageSphere> ();
			mini.previousPart = previousPart;
			mini.GoToNext (nextPart._To[i]);
			mini.charge.ChargeLevelChange (this.charge.CurrentCharge / (float) size);
			mini.masterSphere = this;
			mini.transform.localScale *= 0.8f;
			mini.childSpheres.Clear();
			childSpheres.Add (mini);
		}
		this.GetComponent<MeshRenderer> ().enabled = false;
	}

}