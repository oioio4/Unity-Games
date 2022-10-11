using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float swarmInterval = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        Enemy enemy = enemyPrefab.GetComponent<Enemy>();
        enemy.setHealth(3f);
        StartCoroutine(spawnEnemy(swarmInterval, enemyPrefab));
    }

    void Update() {
        float increase = Time.fixedDeltaTime * 0.05f;
        Enemy enemy = enemyPrefab.GetComponent<Enemy>();
        enemy.increaseHealth(increase);
    }

    // Update is called once per frame
    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
