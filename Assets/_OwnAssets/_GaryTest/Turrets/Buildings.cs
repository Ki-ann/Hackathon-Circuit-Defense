using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buildings : CircuitPart {
	public Battery connectedBattery;
	public override void Update () {
		base.Update ();
		if (isPlaced) {
			//Test
			if (Input.GetKeyDown (KeyCode.Z)) {
				Charge.ChargeLevelChange (5);
			}

			//Turret Behvaiours
		}
	}
}