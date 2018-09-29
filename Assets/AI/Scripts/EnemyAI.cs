using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, ITakeDamage, IEnemy {

    public float speed { get; set; }
    public float health { get; set; }
    public float damage { get; set; }

    public GameObject destinationObj;
    public GameObject startPositionObj;

    protected NavMeshAgent agent;
    protected Vector3 destination;
    protected Vector3 startPosition;
    [SerializeField] protected Animator anim;
    // Use this for initialization
    public virtual void Start () {
        GetNavAgent ();
        startPosition = startPositionObj.transform.position;
        speed = agent.speed;
        health = 100;
        damage = 50;
    }

    // Update is called once per frame
    public virtual void Update () {
        if (destinationObj != null)
            destination = destinationObj.transform.position;
    }

    public virtual void GetNavAgent () {
        agent = GetComponent<NavMeshAgent> ();
    }

    public virtual void TakeDamage (float damage) {
        health -= damage;

        if (health <= 0) {
            Die ();
        }
    }

    public virtual void Die () {
        Destroy (this.gameObject, 0f);
    }
}