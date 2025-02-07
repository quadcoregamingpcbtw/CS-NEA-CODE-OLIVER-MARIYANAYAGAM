using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // This method is called when the bullet collides with another collider
    private void OnCollisionEnter(Collision objectHit)
    {
        // Check if the object the bullet collided with has the "Target" tag
        if (objectHit.gameObject.CompareTag("Target"))
        {
            // Print a message indicating the bullet hit a target
            print("hit " + objectHit.gameObject.name + " !");

            Target_Reset targetReset = objectHit.gameObject.GetComponent<Target_Reset>();
            if (targetReset != null)
            {
                targetReset.Res();
            }

            // Create an impact effect and destroy the bullet
            Destroy(gameObject);
        }

        // Check if the object the bullet collided with has the "Wall" tag
        if (objectHit.gameObject.CompareTag("Wall"))
        {
            // Print a message indicating the bullet hit a wall
            print("hit a wall");

            // Create an impact effect and destroy the bullet
            CreateBulletImpact(objectHit);
            Destroy(gameObject);
        }

        if (objectHit.gameObject.CompareTag("Zombie"))
        {
            // Print a message indicating the bullet hit a zombie
            print("hit a zombie");

            objectHit.gameObject.GetComponent<Zombie_Script>().Health -= Player_Stats.Instance.damage;
            // Create an impact effect and destroy the bullet
            Destroy(gameObject);
        }

        if (objectHit.gameObject.CompareTag("Boss"))
        {
            // Print a message indicating the bullet hit the boss
            print("hit the boss");

            objectHit.gameObject.GetComponent<boss_script>().Health -= Player_Stats.Instance.damage;
            // Create an impact effect and destroy the bullet
            Destroy(gameObject);
        }
    }

    // Creates an impact effect at the point of collision
    void CreateBulletImpact(Collision objectHit)
    {
        // Get the contact point of the collision
        ContactPoint contact = objectHit.contacts[0];

        // Instantiate the bullet impact effect at the contact point
        GameObject hole = Instantiate(
            Global_Referencing.Instance.bulletImpactPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        // Set the instantiated effect as a child of the object hit
        hole.transform.SetParent(objectHit.gameObject.transform);
    }

}
