using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : EnemyAI {
    [SerializeField] private float attackCooldown;
    private float cooldownAttackTimer;
    private bool isOnCoolDown = false;
    // Use this for initialization
    public override void Start () {
        base.Start ();
        base.GetNavAgent ();
    }

    // Update is called once per frame
    public override void Update () {
        base.Update ();
        if (destinationObj) {
            if (agent.destination != destination)
                agent.SetDestination (destination);
        }

        if (!this.agent)
            this.agent = GetComponent<NavMeshAgent> ();

        if (!anim)
            anim = GetComponent<Animator> ();

        if (Vector3.Distance (transform.position, destination) > agent.remainingDistance)
            SetAnimations (true);
        else
            SetAnimations (false);

        Timer ();
    }
    void OnCollisionStay (Collision other) {
        if (!isOnCoolDown) {
            if (other.gameObject.GetComponent<Core> ()) {
                Core core = other.gameObject.GetComponent<Core> ();
                core.TakeDamage (damage);
                cooldownAttackTimer = attackCooldown;
                isOnCoolDown = true;

            } else if (other.gameObject.transform.parent != null) {
                if (other.gameObject.transform.parent.GetComponent<CircuitPart> ()) {
                    other.gameObject.transform.parent.GetComponent<CircuitPart> ().TakeDamage (damage);
                    cooldownAttackTimer = attackCooldown;
                    isOnCoolDown = true;
                }
            }
        }
    }
    public void SetDamage (float _damage) {
        damage = _damage;
    }
    public void SetSpeed (float _speed) {
        speed = _speed;
    }
    public void SetHealth (float _health) {
        health = _health;
    }
    public void SetAnimations (bool isMoving) {
        anim.SetBool ("Move", isMoving);
    }
    void Timer () {
        if (isOnCoolDown) {
            cooldownAttackTimer -= Time.deltaTime;

            if (cooldownAttackTimer <= 0) {
                isOnCoolDown = false;
            }
        }
    }
}