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
        }

        if (!this.agent)
            this.agent = GetComponent<NavMeshAgent>();
            
        if(!anim)
            anim = GetComponent<Animator>();

        if (Vector3.Distance(transform.position, destination) > agent.remainingDistance)
            SetAnimations(true);
        else
            SetAnimations(false);
    }
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Core>()) 
        {
            //add core stuff here
        }
        else if (other.gameObject.transform.parent.GetComponent<CircuitPart>())
        {
            other.gameObject.transform.parent.GetComponent<CircuitPart>().TakeDamage(damage);
            //Debug.Log("fk u turret");
        }
    }
    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
    public void SetHealth(float _health)
    {
        health = _health;
    }
    public void SetAnimations(bool isMoving)
    {
        anim.SetBool("Move", isMoving);
    }
}
