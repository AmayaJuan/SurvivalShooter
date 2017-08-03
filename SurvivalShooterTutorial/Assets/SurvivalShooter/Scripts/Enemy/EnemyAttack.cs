using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = .5f;
    public int attackDamage = 10;

    bool playerInRange;
    float timer;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

    void Update ()
    {
        timer += Time.deltaTime;
        if(timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)
            Attack ();
        if(playerHealth.currentHealth <= 0)
            anim.SetTrigger ("PlayerDead");
    }

    void Attack ()
    {
        timer = 0f;
        if(playerHealth.currentHealth > 0)
            playerHealth.TakeDamage (attackDamage);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInRange = false;
    }
}
