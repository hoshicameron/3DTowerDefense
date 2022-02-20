using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private ParticleSystem[] hitParticle;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private int deathScore;
    [SerializeField] private SoundName explosionSoundEffect = SoundName.None;


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
        else if(other.CompareTag("Balista"))    appliedDamage = 30;


        currentHealth -= appliedDamage;

        // Play hit particles
        PlayHitParticle();

        // Play hit audio
        AudioManager.Instance.PlaySound(SoundName.Effect_EnemyHit);

        // Todo add healthbar UI

        // Check for death
        if (currentHealth < 0)
        {
            Death();
        }

    }

    private void PlayHitParticle()
    {
        // Play all particles
        foreach (ParticleSystem hitParticle in hitParticle)
        {
            hitParticle.Play();
        }
    }

    private void Death()
    {
        // Spawn explosion particle
        GameObject deathVFX=PoolManager.Instance.ReuseObject(deathParticle, transform.position, Quaternion.identity);
        deathVFX.SetActive(true);

        // Spawn explosion sound
        AudioManager.Instance.PlaySound(explosionSoundEffect);

        // Update Game Manager
        GameManager.Instance.UpdateScore(deathScore);

        // Disable game object
        gameObject.SetActive(false);
    }
}
