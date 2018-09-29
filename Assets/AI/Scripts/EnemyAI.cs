﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, ITakeDamage, IDealDamage, IEnemy {

    public float speed { get; set; }
    public float health { get; set; }
    public float damage { get; set; }

    public GameObject destinationObj;
    public GameObject startPositionObj;

    protected NavMeshAgent agent;
    protected Vector3 destination;
    protected Vector3 startPosition;
    protected Animator anim;
    // Use this for initialization
    public virtual void Start()
    {
        GetNavAgent();
        startPosition = startPositionObj.transform.position;
        speed = agent.speed;
        health = 100;
        damage = 50;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        destination = destinationObj.transform.position;
    }

    public virtual void GetNavAgent()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    
    public void DealDamage(float damage)
    {

    }
}
