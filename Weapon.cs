using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{


    public bool isShooting, readyToShoot;        // Flags to check if the weapon is currently shooting and ready to shoot
    private bool allowReset = true;              // Flag to control reset functionality
    public GameObject bulletPrefab;               // Prefab for the bullet to be instantiated
    public Transform bulletSpawn;                 // Position where the bullet will spawn
    public float bulletVelocity = 240f;             // Velocity of the bullet when fired
    public float bulletLifeTime = 3f;      // Time in seconds before the bullet is destroyed
    public GameObject muzzleFlash;
    private Animator Animator;


    // Enum to represent different shooting modes
    public enum ShootingMode
    {
        Single,  // Single shot mode
        automatic     // automaticmatic fire mode
    }

    public ShootingMode currentShootingMode;     // Currently selected shooting mode

    // Called when the script instance is being loaded
    public void Awake()
    {
        readyToShoot = true;                     // Initialize to allow shooting
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check input based on the current shooting mode
        if (Player_Stats.Instance.mode==0f)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0); // Continuous shooting for automatic mode
        }
        else if (Player_Stats.Instance.mode == 1f)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0); // Single shot on key down
        }

        // If ready and shooting, initiate firing
        if (Player_Stats.Instance.currentAmmo<=0)
        {
            readyToShoot = false;
        }

        if (readyToShoot && isShooting)
        {
            WeaponFire();                        // Call method to fire the weapon
        }


    }

    private void WeaponFire()
    {

        readyToShoot = false; // Set readyToShoot to false to prevent firing again immediately

        Player_Stats.Instance.currentAmmo -= 1;
        Player_Stats.Instance.ammoDisplay.text = $"Ammo: {Player_Stats.Instance.currentAmmo}/{Player_Stats.Instance.ammo}";

        muzzleFlash.GetComponent<ParticleSystem>().Play();
        Animator.SetTrigger("RECOILED");
        SoundHandler.Instance.PistolShootSound.Play();

        // Calculate the direction and spread for the bullet
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate the bullet at the spawn position
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection; // Set bullet's direction

        // Apply force to the bullet to propel it
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        // Start coroutine to destroy the bullet after its lifetime
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));

        // Handle shot resetting
        if (allowReset)
        {
            Invoke("ResetShot", Player_Stats.Instance.shootTime); // Reset shot after delay
            allowReset = false; // Prevent multiple invokes
        }

    }


    public void ResetShot()
    {
        readyToShoot = true;  // Allow shooting again
        allowReset = true;    // Enable shot resetting
    }

    // Calculate shooting direction and add spread based on the camera's view
    public Vector3 CalculateDirectionAndSpread()
    {
        // Create a ray from the center of the camera's viewport
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        // Check if the ray hits an object
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; // Set target point to hit point
        }
        else
        {
            targetPoint = ray.GetPoint(100); // Set target point far away if no hit
        }

        // Calculate direction to the target point from the bullet spawn position
        Vector3 direction = targetPoint - bulletSpawn.position;

        // Add random spread to the direction
        float x = UnityEngine.Random.Range(-Player_Stats.Instance.spread, Player_Stats.Instance.spread);
        float y = UnityEngine.Random.Range(-Player_Stats.Instance.spread, Player_Stats.Instance.spread);

        return direction + new Vector3(0, y, x); // Return the final direction with spread
    }

    // Coroutine to destroy the bullet after a specified delay
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
        Destroy(bullet); // Destroy the bullet object
    }
}
