using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrifab;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _enemyConteiner;
    [SerializeField]
    private float _enemySpowncoolDownTime = 1f;

    private bool _stopSpawning = false;

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            GameObject newEnemy = SpawnGameObject(_enemyPrifab);
            newEnemy.transform.parent = _enemyConteiner.transform;
            yield return new WaitForSeconds(_enemySpowncoolDownTime);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        while (!_stopSpawning)
        {

            yield return new WaitForSeconds(Random.Range(3f, 8f));
            SpawnGameObject(_powerups[Random.Range(0, 3)]);
        }
    }
    GameObject SpawnGameObject(GameObject prifab)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-15.50f, 15.5f), 11f, 0f);
        return Instantiate(prifab, spawnPos, Quaternion.identity);
    }
    public void OnPlayerDeth()
    {
        _stopSpawning = true;
    }
}
