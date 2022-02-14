using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject deathParticle;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHealth -= 10;
        // Play hit particle
        hitParticle.Play();

        // Check for death
        if (currentHealth < 0)
        {
            Death();
        }

    }

    private void Death()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
