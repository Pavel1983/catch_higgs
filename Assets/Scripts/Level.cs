using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static event Action<float> EventEnergyChanged;
    public static event Action EventWin;
    
    [SerializeField] private float energyGoal;

    private float energyGathered = 0;

    public float EnergyGoal => energyGoal;
    
    private float TotalEnergy
    {
        get => energyGathered;
        set
        {
            if (!Mathf.Approximately(energyGathered, value))
            {
                energyGathered = Mathf.Clamp(value, 0, float.MaxValue);
                EventEnergyChanged?.Invoke(energyGathered);
                if (ReachedGoal())
                {
                    Time.timeScale = 0.3f;
                    EventWin?.Invoke();
                }
            }
        }
    }

    // locals
    private List<ParticleBehaviour> particlesInSystem = new List<ParticleBehaviour>();

    private void OnEnable()
    {
        ParticleBehaviour.EventReachedSystem += OnParticleReachedSystem;
        ParticleBehaviour.EventLeftSystem += OnParticleLeftSystem;
    }

    private void OnDisable()
    {
        ParticleBehaviour.EventReachedSystem -= OnParticleReachedSystem;
        ParticleBehaviour.EventLeftSystem -= OnParticleLeftSystem;
    }

    private void Start()
    {
        var spawners = GetComponentsInChildren<NodeParticleSpawner>();
        foreach (var spawner in spawners)
        {
            spawner.StartSpawning();
        }
    }

    private void OnParticleLeftSystem(ParticleBehaviour particle)
    {
        TotalEnergy -= particle.Speed;
    }

    private void OnParticleReachedSystem(ParticleBehaviour particle)
    {
        TotalEnergy += particle.Speed;
    }

    private bool ReachedGoal()
    {
        return energyGathered >= energyGoal;
    }
}
