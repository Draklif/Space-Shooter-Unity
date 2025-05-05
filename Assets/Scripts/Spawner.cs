using System.Collections;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab_1;
    [SerializeField] private GameObject enemyPrefab_2;
    [SerializeField] private GameObject enemyPrefab_3;
    [SerializeField] private GameObject enemyPrefab_4;
    [SerializeField] private TextMeshProUGUI textWave;
    [SerializeField] private TextMeshProUGUI textLevel;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private GameObject GetEnemyByLevel(int level)
    {
        float roll = Random.Range(0f, 1f);

        if (level >= 4 && roll > 0.85f)
            return enemyPrefab_4;
        if (level >= 3 && roll > 0.7f)
            return enemyPrefab_3;
        if (level >= 2 && roll > 0.5f)
            return enemyPrefab_2;

        return enemyPrefab_1;
    }

    IEnumerator SpawnEnemy()
    {
        for (int level = 0; level < 5; level++)
        {
            textLevel.text = "Level " + (level + 1);
            for (int wave = 0; wave < 3; wave++)
            {
                textWave.text = "Wave " + (wave + 1);
                for (int enemy = 0; enemy < 10; enemy++)
                {
                    Vector3 randomSpawnPoint = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), 0);
                    GameObject enemyToSpawn = GetEnemyByLevel(level + 1);
                    Instantiate(enemyToSpawn, randomSpawnPoint, Quaternion.identity);
                    yield return new WaitForSeconds(.5f);
                }
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(3f);
        }
        
    }
}
