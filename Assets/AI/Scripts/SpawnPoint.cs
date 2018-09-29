using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    private void Awake()
    {
        if (!SpawnArray.Instance.spawnPointObjList.Contains(this.gameObject))
            SpawnArray.Instance.spawnPointObjList.Add(this.gameObject);
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
