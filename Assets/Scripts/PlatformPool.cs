using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    public GameObject platformPrefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    public void Initialize()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            GameObject platform = Instantiate(platformPrefab);
            platform.SetActive(false);
            pool.Enqueue(platform);
        }
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
            return Instantiate(platformPrefab);
        }
    }

    public void Return(GameObject platform)
    {
        platform.SetActive(false);

        var script = platform.GetComponent<MovingPlatform>();
        script.isFrozen = false;
        script.scored = false;

        platform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        pool.Enqueue(platform);
    }
}
