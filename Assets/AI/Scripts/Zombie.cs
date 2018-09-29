using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : EnemyAI {

	// Use this for initialization
	public override void Start () {
        base.Start();
        base.GetNavAgent();
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        if (destinationObj)
        {
            if(agent.destination != destination)
                agent.SetDestination(destination);
            //Debug.Log(destination);
            //Debug.Log(destinationObj);
            //Debug.Log(true);
        }

        if (!this.agent)
            this.agent = GetComponent<NavMeshAgent>();
            
    }
//    public override void GetNavAgent()
//    {
        
//    }
}
