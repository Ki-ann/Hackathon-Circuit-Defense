using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILauncher {
	GameObject ProjPrefab { get; }
	void Launch(Turret turret);
}
