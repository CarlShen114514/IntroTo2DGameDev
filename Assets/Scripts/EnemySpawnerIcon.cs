using UnityEngine;

public class EnemySpawnerIcon : MonoBehaviour
{
    public Spawner associatedSpawner;

    private void OnDestroy()
    {
        if (associatedSpawner != null)
        {
            associatedSpawner.spawnerIcon = null; // 清除关联
        }
    }
}
