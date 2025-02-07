using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManagerScript : MonoBehaviour
{
    // Singleton instance of InteractionManagerScript
    public static InteractionManagerScript Instance { get; set; }

    private void Awake()
    {
        // Ensure only one instance of InteractionManagerScript exists (Singleton pattern)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        // Create a ray from the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Check if the ray hits an object
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;

            // If the hit object is a weapon
            if (objectHit.CompareTag("Weapon"))
            {
                // Retrieve the price of the weapon
                float price = Shop_Script.Instance.GunStats[objectHit.name + "price"];

                // Display weapon price
                Shop_Script.Instance.priceDisplay.text = $"{objectHit.name}: £{price}";

                // Check for right mouse button input to purchase the weapon
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (Player_Stats.Instance.money >= price)
                    {
                        // Hide all player weapons
                        GameObject.Find("Player_AK47").transform.localScale = new Vector3(0, 0, 0);
                        GameObject.Find("Player_Uzi").transform.localScale = new Vector3(0, 0, 0);
                        GameObject.Find("Player_RPG").transform.localScale = new Vector3(0, 0, 0);
                        GameObject.Find("M1911").transform.localScale = new Vector3(0, 0, 0);

                        // Show the purchased weapon
                        GameObject.Find("Player_" + objectHit.name).transform.localScale = new Vector3(2, 2, 2);

                        // Update player stats with weapon properties
                        Player_Stats.Instance.shootTime = Shop_Script.Instance.GunStats[objectHit.name + "shootTime"];
                        Player_Stats.Instance.damage = Shop_Script.Instance.GunStats[objectHit.name + "damage"];
                        Player_Stats.Instance.spread = Shop_Script.Instance.GunStats[objectHit.name + "spread"];
                        Player_Stats.Instance.ammo = Shop_Script.Instance.GunStats[objectHit.name + "ammo"];
                        Player_Stats.Instance.currentAmmo = Player_Stats.Instance.ammo;
                        Player_Stats.Instance.mode = Shop_Script.Instance.GunStats[objectHit.name + "mode"]; // 0 = auto, 1 = single

                        // Adjust weapon sound based on shooting time
                        SoundHandler.Instance.PistolShootSound.pitch = (0.3f) / (Player_Stats.Instance.shootTime);

                        // Reset pistol shooting state
                        GameObject.Find("Pistol").GetComponent<Weapon>().ResetShot();

                        // Deduct money and update UI
                        Player_Stats.Instance.money -= price;
                        Player_Stats.Instance.coinsDisplay.text = $"Coins: {Player_Stats.Instance.money}";
                        Player_Stats.Instance.ammoDisplay.text = $"Ammo: {Player_Stats.Instance.currentAmmo}/{Player_Stats.Instance.ammo}";
                    }
                    else
                    {
                        // Print insufficient funds message
                        print("Insufficient Funds");
                    }
                }
            }
            // If the hit object is a consumable item
            else if (objectHit.CompareTag("Consumable"))
            {
                // Retrieve the price of the consumable
                float price = Shop_Script.Instance.GunStats[objectHit.name + "price"];

                // Display consumable price
                Shop_Script.Instance.priceDisplay.text = $"{objectHit.name}: £{price}";

                // Check if the consumable is a medkit
                if (objectHit.name == "Medkit")
                {
                    // Check for right mouse button input to purchase the medkit
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        if (Player_Stats.Instance.money >= price)
                        {
                            // Deduct money and update UI
                            Player_Stats.Instance.money -= price;
                            Player_Stats.Instance.coinsDisplay.text = $"Coins: {Player_Stats.Instance.money}";

                            // Restore player health and update UI
                            Player_Stats.Instance.Health = 100f;
                            Player_Stats.Instance.healthDisplay.text = $"Health: {Player_Stats.Instance.Health}/100";
                        }
                        else
                        {
                            // Print insufficient funds message
                            print("Insufficient Funds");
                        }
                    }
                }
            }
            else
            {
                // Clear price display when not interacting with a valid item
                Shop_Script.Instance.priceDisplay.text = "";
            }
        }
    }
}
