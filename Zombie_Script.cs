using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_Script : MonoBehaviour
{
    private NavMeshAgent agent; // Reference to the NavMeshAgent for zombie movement
    public float attackRange = 4f; // The range within which the zombie will attack the player
    public float Health = 100f; // Health of the zombie
    private float dist; // Distance between the zombie and the player
    public float damage = 5f; // Damage the zombie deals when attacking
    private bool isAttackingPlayer = false; // A flag to track if the zombie is attacking the player
    private float attackTime = 1f; // Time between attacks
    private Animator Animator; // Reference to the Animator for zombie animations
    private float deathTime = 4f; // Time before the zombie's death animation finishes
    public bool isAlive = true; // A flag to check if the zombie is alive

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component for movement
        Animator = GetComponent<Animator>(); // Get the Animator component for animation control
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            if (isAlive) // Check if the zombie is alive
            {
                if (agent.isStopped == false)
                {
                    agent.isStopped = true; // Stop the zombie's movement when it's dead
                }
                isAlive = false;
                Animator.SetTrigger("isDying"); // Trigger death animation
                Invoke("death", deathTime); // Invoke death method after the specified death time
            }
        }
        else
        {
            // Adjust movement speed and angular speed based on whether it's a danger round
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

            dist = Vector3.Distance(Player_Stats.Instance.Player.position, gameObject.transform.position); // Calculate distance to the player
            if (dist <= attackRange) // If within attack range, stop moving and attack
            {
                if (agent.isStopped == false)
                {
                    agent.isStopped = true;
                }
                if (!isAttackingPlayer) // If not already attacking, initiate attack
                {
                    Animator.SetTrigger("isAttacking");
                    isAttackingPlayer = true;
                    Invoke("attackPrep", attackTime); // Prepare the attack after the specified attack time
                }
            }
            else // If out of attack range, continue moving towards the player
            {
                agent.isStopped = false;
                agent.SetDestination(Player_Stats.Instance.Player.position); // Move the zombie towards the player
            }
        }
    }

    private void death()
    {
        // Increase the player's money upon the zombie's death
        Player_Stats.Instance.money += 30f;
        Player_Stats.Instance.coinsDisplay.text = $"Coins: {Player_Stats.Instance.money}";
        Zombie_Handler.Instance.numOfZombies -= 1; // Decrease the total number of zombies
        Destroy(gameObject); // Destroy the zombie object
    }

    private void attackPrep()
    {
        isAttackingPlayer = false; // Reset the attack flag
        Animator.SetTrigger("isRunning"); // Set the zombie to its running animation
        Player_Stats.Instance.Health -= damage; // Apply damage to the player
        Player_Stats.Instance.healthDisplay.text = $"Health: {Player_Stats.Instance.Health}/100"; // Update player's health display
    }
}
