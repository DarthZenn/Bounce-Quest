using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public PlatformPool platformPool;
    public Transform player;
    public float spawnYGapMin;
    public float spawnYGapMax;
    public int initialPlatformCount;

    private float screenHalfWidth;
    private float nextSpawnY;
    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        float screenHeight = Camera.main.orthographicSize;
        screenHalfWidth = screenHeight * Screen.width / Screen.height;

        platformPool.Initialize();

        nextSpawnY = player.position.y + spawnYGapMin;

        for (int i = 0; i < initialPlatformCount; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        float playerTopY = player.position.y + Camera.main.orthographicSize + 2f;

        while (nextSpawnY < playerTopY)
        {
            SpawnPlatform();
        }

        RecycleOffScreenPlatforms();
    }

    void SpawnPlatform()
    {
        float randomX = Random.Range(-screenHalfWidth + 0.5f, screenHalfWidth - 0.5f);

        GameObject platform = platformPool.Get();

        platform.transform.position = new Vector3(randomX, nextSpawnY, 0f);

        platform.GetComponent<MovingPlatform>().Initialize();

        platform.SetActive(true);
        activePlatforms.Add(platform);

        float yGap = Random.Range(spawnYGapMin, spawnYGapMax);
        nextSpawnY += yGap;
    }

    void RecycleOffScreenPlatforms()
    {
        float camBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize - 3f;

        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            GameObject platform = activePlatforms[i];

            if (platform.transform.position.y - 3f < camBottomY)
            {
                platformPool.Return(platform);
                activePlatforms.RemoveAt(i);
            }
        }
    }
}
