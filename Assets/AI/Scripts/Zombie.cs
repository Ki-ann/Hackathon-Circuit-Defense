using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : EnemyAI {

	// Use this for initialization
	void Start () {
        base.GetNavAgent();
    }
	
	// Update is called once per frame
	void Update () {
        if (destinationObj)
            agent.SetDestination(destination);
    }
//    public override void GetNavAgent()
//    {
        
//    }
}
