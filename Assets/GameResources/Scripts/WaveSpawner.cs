using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public float timesNextWaves = 5f;
    public Transform SpawnPoint;
    public TextMeshProUGUI waveCountdownText;




    private float countdown = 2f;
    private int waveIndex = 0;


    void Update()
    {
        #region Countdown Next Wave
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timesNextWaves;
        }

        countdown -= Time.deltaTime;

        waveCountdownText.text = string.Format("{0:0}", countdown);
        #endregion
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex + 1; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
    }
}
