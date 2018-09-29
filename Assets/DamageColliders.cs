using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColliders : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    private void OnCollisionEnter(Collision collision)
    {
        ITakeDamage damagable = collision.gameObject.GetComponent<ITakeDamage>();

        if (damagable != null)
        {
            damagable.TakeDamage(damageAmount);
        }
    }
}