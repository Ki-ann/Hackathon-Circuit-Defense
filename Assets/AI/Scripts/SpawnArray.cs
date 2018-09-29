using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArray : Singleton<SpawnArray>
{
    public List<GameObject> spawnPointObjList;

    private void OnDisable()
    {
        spawnPointObjList.Clear();
    }
}
