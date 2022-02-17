using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject deathParticle;
    private float currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        int appliedDamage = 0;
        if (other.CompareTag("Flame"))        appliedDamage = 1;
        else if(other.CompareTag("Cannon"))    appliedDamage = 10;


        currentHealth -= appliedDamage;
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
        GameObject deathVFX=PoolManager.Instance.ReuseObject(deathParticle, transform.position, Quaternion.identity);
        deathVFX.SetActive(true);
        gameObject.SetActive(false);
    }
}
