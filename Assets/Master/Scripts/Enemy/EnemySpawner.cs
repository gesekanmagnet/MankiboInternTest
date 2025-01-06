using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpawnPos;
    [SerializeField] private AudioClip clip;

    [SerializeField] private float firstTimeSpawn, acceleration, maxInterval;

    private float currentInterval;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += Spawn;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= Spawn;
    }

    private void Spawn()
    {
        currentInterval = firstTimeSpawn;
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        while (GameManager.Instance.currentState == GameState.GameStart)
        {
            int randomIndex = Random.Range(0, enemySpawnPos.Length);
            Transform spawnPoint = enemySpawnPos[randomIndex];
            GameObject enemy = ObjectPool.Instance.GetEnemy();
            enemy.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

            AudioManager.Instance.PlaySFX(clip);

            yield return new WaitForSeconds(currentInterval);
            currentInterval = Mathf.Max(maxInterval, currentInterval - acceleration);
        }
    }
}
