using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemy {
    float health { get; set; }
    float speed { get; set; }
    float damage { get; set; }

    void GetNavAgent();
}
