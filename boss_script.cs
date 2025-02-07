using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class boss_script : MonoBehaviour
{
    private NavMeshAgent agent; // AI navigation agent
    public float attackRange = 6f; // Range at which the boss attacks
    public float Health = 7000f; // Boss health
    private float dist; // Distance to player
    public float damage = 20f; // Damage dealt to player
    private bool isAttackingPlayer = false; // Tracks if the boss is attacking
    private float attackTime = 3f; // Time between attacks
    private Animator Animator; // Animator for boss animations
    private float deathTime = 4f; // Time before boss disappears after death
    public bool isAlive = true; // Tracks if the boss is alive

    public float spawnRadius = 3f; // Radius in which minions spawn
    public bool isSpawning = false; // Tracks if the boss is spawning minions
    public float spawningPrepTime = 15f; // Time before spawning minions
    public float spawnTime = 2f; // Time taken to spawn minions
    public bool readyToSpawn = false; // If the boss is ready to spawn minions

    void Start()
    {
        // Get references to components
        agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the boss is active
        if (Zombie_Handler.Instance.BossIsActive)
        {
            // Check if the boss has been defeated
            if (Health <= 0)
            {
                if (isAlive)
                {
                    // Stop movement and trigger death animation
                    if (!agent.isStopped)
                    {
                        agent.isStopped = true;
                    }
                    isAlive = false;
                    Animator.SetTrigger("isDying");
                    Invoke("death", deathTime);
                }
            }
            else
            {
                // Update boss health display
                Zombie_Handler.Instance.bossHealthDisplay.text = $"Boss Health: {Health}/7000";

                // Handle zombie minion spawning
                if (readyToSpawn)
                {
                    readyToSpawn = false;
                    Animator.SetTrigger("isTaunting");
                    Invoke("spawnZombs", spawnTime);
                    isSpawning = false;
                }
                else if (!isSpawning)
                {
                    isSpawning = true;
                    Zombie_Handler.Instance.dangerRound = !Zombie_Handler.Instance.dangerRound;
                    Zombie_Handler.Instance.dangerLevel += 1f;
                    Invoke("spawnPrep", spawningPrepTime);
                }

                // Adjust movement based on danger round
                if (Zombie_Handler.Instance.dangerRound)
                {
                    agent.speed = Zombie_Handler.Instance.dangerLevel;
                    agent.angularSpeed = 360f;
                }
                else
                {
                    agent.speed = 5f;
                    agent.angularSpeed = 120f;
                }

                // Get distance to player
                dist = Vector3.Distance(Player_Stats.Instance.Player.position, gameObject.transform.position);

                // Handle attack behavior
                if (dist <= attackRange)
                {
                    if (!agent.isStopped)
                    {
                        agent.isStopped = true;
                    }
                    if (!isAttackingPlayer)
                    {
                        Animator.SetTrigger("isAttacking");
                        isAttackingPlayer = true;
                        Invoke("attackPrep", attackTime);
                    }
                }
                else
                {
                    // Move towards player
                    agent.isStopped = false;
                    agent.SetDestination(Player_Stats.Instance.Player.position);
                }
            }
        }
    }

    // Handles boss death
    private void death()
    {
        Player_Stats.Instance.money += 30f;
        Player_Stats.Instance.coinsDisplay.text = $"Coins: {Player_Stats.Instance.money}";
        Zombie_Handler.Instance.numOfZombies -= 1;
        Zombie_Handler.Instance.winningScreen.SetActive(true);
        Destroy(gameObject);
    }

    // Handles attack preparation
    private void attackPrep()
    {
        isAttackingPlayer = false;
        Animator.SetTrigger("isRunning");
        Player_Stats.Instance.Health -= damage;
        Player_Stats.Instance.healthDisplay.text = $"Health: {Player_Stats.Instance.Health}/100";
    }

    // Prepares for minion spawning
    private void spawnPrep()
    {
        readyToSpawn = true;
    }

    // Spawns zombie minions
    private void spawnZombs()
    {
        agent.isStopped = true;
        for (int i = 0; i < 5; i++)
        {
            float x = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
            float z = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
            Vector3 spawnLocation = gameObject.transform.position + new Vector3(x, 0, z);

            GameObject zomb = Instantiate(Zombie_Handler.Instance.ZombiePrefab, spawnLocation, Quaternion.identity);
            print("Spawned zombie at: " + spawnLocation);
        }
        Animator.SetTrigger("isRunning");
    }
}
