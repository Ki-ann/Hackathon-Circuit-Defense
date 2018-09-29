﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour, ILauncher {
	public GameObject ProjPrefab { get { return projPrefab; } }
	[SerializeField] private GameObject projPrefab;

	[SerializeField] private float projSpeed; 
	private Rigidbody rBody;

	public void Launch (Turret turret) {
		GameObject missile = Instantiate (projPrefab, turret.FirePoint.transform.position, Quaternion.Euler(90, 0, 0));
		missile.transform.eulerAngles += turret.FirePoint.transform.rotation.eulerAngles;
		missile.GetComponent<MissileController>().m_Turret = turret;
		rBody = missile.GetComponent<Rigidbody>();
		rBody.velocity = turret.FirePoint.transform.forward * projSpeed;
	}
}