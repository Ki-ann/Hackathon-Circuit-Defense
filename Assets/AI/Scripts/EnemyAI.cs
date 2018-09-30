using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, ITakeDamage, IEnemy
{

    [SerializeField] GameObject HpBarPrefab;
    HpBar hpBar;
    public float speed { get; set; }
    public float health { get; set; }
    public float damage { get; set; }
    public float maxHealth { get; set; }

    public Core core;
    public GameObject destinationObj;
    public GameObject startPositionObj;

    protected NavMeshAgent agent;
    protected Vector3 destination;
    protected Vector3 startPosition;
    [SerializeField] private IntVariable playerMoney;
    private int reward;
    public int Reward
    {
        set { reward = value; }
    }

    [SerializeField]
    protected Animator anim;
    // Use this for initialization
    public virtual void Start()
    {
        GetNavAgent();
        startPosition = startPositionObj.transform.position;
        speed = agent.speed;
        health = 100;
        damage = 50;
        maxHealth = health;
        hpBar = Instantiate(HpBarPrefab, transform).GetComponent<HpBar>();
        hpBar.UpdateHPBar(health, maxHealth);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (destinationObj != null)
            destination = destinationObj.transform.position;
        if (destinationObj == null)
        {
            if (!core)
            {
                core = FindObjectOfType<Core>();
            }
            else
            {
                destinationObj = core.gameObject;
            }
        }
    }

    public virtual void GetNavAgent()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        hpBar.UpdateHPBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        playerMoney.Value += reward;
        Destroy(this.gameObject, 0f);
    }
}