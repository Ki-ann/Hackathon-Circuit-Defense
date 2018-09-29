using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage {
    void TakeDamage (float amount);
}

public interface IDealDamage {
    void DealDamage (float amount);
}