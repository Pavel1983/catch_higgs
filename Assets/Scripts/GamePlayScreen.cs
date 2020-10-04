using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayScreen : MonoBehaviour
{
    [SerializeField] private Image energyImg;
    [SerializeField] private GameObject winScreen;

    private float totalEnergy;
    private float energyNeeded;
    
    private Level curLevel;

    private void OnEnable()
    {
        Level.EventEnergyChanged += OnEnergyChanged;
        Level.EventWin += OnWin;
    }

    private void OnDisable()
    {
        Level.EventEnergyChanged -= OnEnergyChanged;
        Level.EventWin -= OnWin;
    }

    private void OnWin()
    {
        winScreen.SetActive(true);
        Destroy(this.gameObject);
    }

    private void Start()
    {
        curLevel = FindObjectOfType<Level>();
        energyNeeded = curLevel.EnergyGoal;
    }

    private void OnEnergyChanged(float newEnergy)
    {
        totalEnergy = newEnergy;

        energyImg.fillAmount = Mathf.Clamp01(totalEnergy / energyNeeded);
    }
}
