using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buildings : CircuitPart {
	private poles NearestLookDirection;
	public override void Update () {
		base.Update ();
		if (!isPlaced) {
			RotateVisualLookAt ();
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
		float rotateAngle = Mathf.Round(visual.transform.eulerAngles.y / 90) * 90;
		visual.transform.eulerAngles = new Vector3 (0, rotateAngle, 0);
	}
}