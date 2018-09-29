using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIArray : Singleton<AIArray> {

    public List<EnemyAI> enemyList;

    private void OnEnable()
    {
        enemyList.Clear();
    }
}
