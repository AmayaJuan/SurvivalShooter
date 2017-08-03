﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Sprite icon;
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    bool isDead;
    bool isSinking;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        currentHealth = startingHealth;
    }

    public int GetHealthPercentage()
    {
        return 100 * currentHealth / startingHealth;
    }

    void Update ()
    {
        if(isSinking)
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
    }

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;
        enemyAudio.Play();
        currentHealth -= amount;
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        if (currentHealth <= 0)
            Death();
    }

    void Death ()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger ("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
