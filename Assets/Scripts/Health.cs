using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {

        currentHealth -= 10;
        print(other.name);
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
        print(other.name);

    }
}
