using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnSettings
{
    public Vector2 SpawnPeriod;
}

[RequireComponent(typeof(Node))]
public class NodeParticleSpawner : MonoBehaviour
{
    [SerializeField] private List<ParticleBehaviour> particlePrefabList;
    [SerializeField] private SpawnSettings spawnSettings;

    private float timer;
    private Node node;
    
    #region life cycle
    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        timer = 0.0f;
        node = GetComponent<Node>();
    }
    #endregion

    public void StartSpawning()
    {
        StartCoroutine(WaitUntilSpawn(GenerateNextSpawnTime(), OnTimeToSpawn));
    }

    private void OnTimeToSpawn()
    {
        SpawnParticle();
        StartSpawning();
    }

    private void SpawnParticle()
    {
        var particlePrefab = particlePrefabList[Random.Range(0, particlePrefabList.Count)];
        var particle = Instantiate(particlePrefab, transform, false);
        particle.transform.localPosition = Vector3.zero;
        particle.SetupAndStart(node);
    }

    private IEnumerator WaitUntilSpawn(float spawnTime, Action onFinished)
    {
        while (timer < spawnTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = timer - spawnTime;
        onFinished?.Invoke();
    }

    private float GenerateNextSpawnTime()
    {
        return Random.Range(spawnSettings.SpawnPeriod.x, spawnSettings.SpawnPeriod.y);
    }



}
