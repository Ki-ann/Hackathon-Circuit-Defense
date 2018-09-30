using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buildings : CircuitPart {
	public poles NearestLookDirection;
	[SerializeField] GameObject HpBarPrefab;
	HpBar hpBar;

	public override void Start () {
		base.Start ();
		visual.GetComponent<Collider> ().enabled = true;
		hpBar = Instantiate (HpBarPrefab, visual.transform).GetComponent<HpBar> ();
		hpBar.transform.localPosition = new Vector3 (0, 1.7f, 0);
		hpBar.UpdateHPBar (currentHP, MaxHP);
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

	public override void TakeDamage (float amount) {
		base.TakeDamage (amount);
		hpBar.UpdateHPBar (currentHP, MaxHP);
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