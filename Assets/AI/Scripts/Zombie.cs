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
    public void TypeOfZombie(int number)
    {
        switch (number)
        {
            case 1: //normal zombie
                SetDamage(20);
                SetSpeed(agent.speed);
                SetHealth(30);
                break;
            case 2: //fast zombie
                SetDamage(10);
                SetSpeed(agent.speed * 1.5f);
                SetHealth(15);
                break;
            case 3: //tanky zombie
                SetDamage(30);
                SetSpeed(agent.speed / 1.5f);
                SetHealth(50);
                break;
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
