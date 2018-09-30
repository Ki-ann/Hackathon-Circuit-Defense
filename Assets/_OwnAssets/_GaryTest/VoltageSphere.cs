using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoltageSphere : MonoBehaviour {
	public Battery originalBattery;
	public CircuitPart.ChargeLevel charge;
	public CircuitPart previousPart;
	public CircuitPart nextPart;
	public VoltageSphere masterSphere;
	public List<VoltageSphere> childSpheres = new List<VoltageSphere> ();
	public List<CircuitPart> cachePath = new List<CircuitPart> ();

	bool consol = false;
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

	void Start()
	{
		StartCoroutine(timeout());
	}

	void Update () {
		if (consol == false) {
			if (transform.localScale.x <= 0.35f) {
				consol = true;
				Consolidation ();
			}
		}
		if(consol == true){
			return;
		}
		//if haven't split
		if (!childSpheres.Any ()) {
			// go to next
			if (nextPart != null && previousPart != null) {
				if (nextPart == originalBattery) {
					if (masterSphere) masterSphere.Consolidation ();
					else Consolidation ();
				}
				if (Vector3.Distance (transform.position, nextPart.visual.transform.position) <= 0.1f) {
					switch (nextPart._To.Count) {
						case 0:
							GoToNext (null);
							return;
						case 1:

							GoToNext (nextPart._To.FirstOrDefault ());
							break;
						case 2:
						case 3:
						case 4:
							SplitBall (nextPart._To.Count);
							break;
					}
				}
				transform.position = Vector3.Lerp (transform.position, nextPart.visual.transform.position, Time.deltaTime * 5f);
				//we hit an open circuit, return the voltage to the split spheres
			} else if (nextPart == null && previousPart != null) {
				ReturnBallToMaster ();
			}
		}
	}
	public void GoToNext (CircuitPart next) {
		previousPart = nextPart;
		cachePath.Add (previousPart);
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
			mini.childSpheres.Clear ();
			childSpheres.Add (mini);
		}
		this.GetComponent<MeshRenderer> ().enabled = false;
	}

	public void ReturnBallToMaster () {
		if (masterSphere) {
			masterSphere.childSpheres.Remove (this);
			foreach (VoltageSphere mini in masterSphere.childSpheres) {
				mini.charge.ChargeLevelChange (this.charge.CurrentCharge / (int) masterSphere.childSpheres.Count ());
				mini.transform.localScale *= 1.2f;
			}
		}
		Destroy (this.gameObject);
	}

	public void Consolidation () {
		for (int i = 0; i < childSpheres.Count; i++) {
			if (childSpheres[i].childSpheres.Any ()) {
				childSpheres[i].Consolidation ();
			}
		}
		StartCoroutine (consolidate ());
	}

	IEnumerator consolidate () {
		List<VoltageSphere> nonMasterBalls = new List<VoltageSphere> (childSpheres).Where (x => !x.childSpheres.Any ()).ToList ();
		while (nonMasterBalls.Count > 1) {
			for (int i = 0; i < nonMasterBalls.Count; i++) {
				if (nonMasterBalls[i].nextPart == originalBattery) {
					nonMasterBalls[i].ReturnBallToMaster ();
				}
			}
			yield return null;
		}
		Battery.VoltagePath path = new Battery.VoltagePath ();
		path.path = cachePath;
		path.charge = charge.CurrentCharge;
		originalBattery.goodPaths.Add (path);

		Destroy (this.gameObject);
	}

	IEnumerator timeout(){
		yield return new WaitForSeconds(30f);
		StartCoroutine(consolidate());
	}
}