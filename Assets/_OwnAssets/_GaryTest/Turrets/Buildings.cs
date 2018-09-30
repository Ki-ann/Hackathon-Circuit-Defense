using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buildings : CircuitPart {
	public poles NearestLookDirection;

	public override void Start () {
		base.Start ();
		visual.GetComponent<Collider> ().enabled = true;
	}

	public override void Update () {
		base.Update ();
		if (!isPlaced) {
			//RotateVisualLookAt ();
		}

		if (isPlaced) {
			//Test
			if (Input.GetKeyDown (KeyCode.Z)) {
				Charge.ChargeLevelChange (5);
			}

			//Turret Behvaiours
		}
	}

	void RotateVisualLookAt () {
		visual.transform.LookAt (target.transform);
		float rotateAngle = Mathf.Round (visual.transform.eulerAngles.y / 90) * 90;
		visual.transform.eulerAngles = new Vector3 (0, rotateAngle, 0);

		switch ((int) (rotateAngle)) {
			case 360:
			case 0:
				NearestLookDirection = poles.UP;
				break;
			case 90:
				NearestLookDirection = poles.RIGHT;
				break;
			case 180:
				NearestLookDirection = poles.DOWN;
				break;
			case 270:
				NearestLookDirection = poles.LEFT;
				break;
		}
	}
}