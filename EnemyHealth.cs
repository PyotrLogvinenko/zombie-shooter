using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] AudioClip enemyDeathSFX;
    [SerializeField] AudioClip enemyHitSFX;

    AudioSource myAudioSource;
    bool isDead = false;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        myAudioSource.PlayOneShot(enemyHitSFX);
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        
        GetComponent<Animator>().SetTrigger("die");
        myAudioSource.PlayOneShot(enemyDeathSFX);
    }
}
