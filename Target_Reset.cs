using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Reset : MonoBehaviour
{
    // Delay before resetting the target position
    public float delay = 2f;

    // Prefab reference for the target
    public GameObject targetPrefab;

    // Spawn point for the target
    public GameObject targetSpawn;

    // Bounds for random positioning
    public int width = 7;
    public int height = 3;
    public int length = 15;

    // Resets the target's position to a random location within the defined bounds
    public void Res()
    {
        float x = UnityEngine.Random.Range(-width, width);
        float y = UnityEngine.Random.Range(-height, height);
        float z = UnityEngine.Random.Range(-length, length);
        
        // Move the target to a new random position relative to the spawn point
        gameObject.transform.position = targetSpawn.transform.position + new Vector3(x, y, z);
    }
}
