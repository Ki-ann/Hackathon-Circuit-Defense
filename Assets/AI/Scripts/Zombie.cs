using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : EnemyAI {
    [SerializeField] private float attackCooldown;
    private float cooldownAttackTimer;
    private bool isOnCoolDown = false;
    [SerializeField] private AudioSource impact;
    [SerializeField] private AudioSource groan;
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
        StartCoroutine (PlayGroan ());
    }
    void OnCollisionStay (Collision other) {
        if (!isOnCoolDown) {
            if (other.gameObject.GetComponent<Core> ()) {
                Core core = other.gameObject.GetComponent<Core> ();
                core.TakeDamage (damage);
                impact.Play ();
                cooldownAttackTimer = attackCooldown;
                isOnCoolDown = true;

            } else if (other.gameObject.transform.parent != null) {

                if (other.gameObject.transform.parent.GetComponent<CircuitPart> ()) {
                    //change current position to turret's postion
                    destinationObj = other.gameObject;
                    other.gameObject.transform.parent.GetComponent<CircuitPart> ().TakeDamage (damage);
                    impact.Play ();
                    cooldownAttackTimer = attackCooldown;
                    isOnCoolDown = true;
                }
            }
        }
    }

    IEnumerator PlayGroan () {
        yield return new WaitForSeconds (Random.Range (2.0f, 5.0f));
        if (!groan.isPlaying)
            groan.Play ();
    }
    public void TypeOfZombie(int number)
    {
        switch (number)
        {
            case 1://normal zombie
                SetDamage(75);
                SetSpeed(agent.speed);
                SetHealth(30);
                Reward = 50;
                break;
            case 2://fast zombie
                SetDamage(75);
                SetSpeed(agent.speed * 1.5f);
                SetHealth(20);
                Reward = 100;
                break;
            case 3://tanky zombie
                SetDamage(200);
                SetSpeed(agent.speed/1.5f);
                SetHealth(100);
                Reward = 150;
                break;
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