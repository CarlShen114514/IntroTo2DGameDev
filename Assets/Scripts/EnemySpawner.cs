using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab = null; // 敌人的预制件
    public float spawnInterval = 5f; // 生成间隔时间
    //public GameObject spawnerIcon; // 关联的Icon对象
    public Vector2 initialVelocity = new Vector2(-50f, 0f); // 初速度

    private Coroutine spawnRoutine;
    private float lastSpawnTime = 0f;

    private void Start()
    {
        // HideInitialPrefab();
        // if (spawnerIcon != null)
        // {
        //     spawnRoutine = StartCoroutine(SpawnEnemyRoutine()); // 启动生成敌人的协程
        // }
    }

    private void HideInitialPrefab()
    {
        // 禁用初始预制件的渲染器组件
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // 等待指定的间隔时间
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity); // 在生成器的位置生成敌人
            lastSpawnTime = Time.realtimeSinceStartup;
            // 给敌人一个初速度
            Rigidbody2D rb = newEnemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = initialVelocity;
            }
            else
            {
                Debug.LogWarning("Enemy prefab does not have a Rigidbody2D component.");
            }
        }
        else
        {
            Debug.LogWarning("Enemy prefab is missing!");
        }
    }

    private void Update()
    {
        // // 检查spawnerIcon是否存在
        // if (spawnerIcon == null && spawnRoutine != null)
        // {
        //     StopCoroutine(spawnRoutine);
        //     spawnRoutine = null;
        // }
        if (TimeTillNext() <= 0)
        {
            SpawnEnemy();
        }
    }

    public float TimeTillNext()
    {
        return spawnInterval + lastSpawnTime - Time.realtimeSinceStartup;
    }
}
