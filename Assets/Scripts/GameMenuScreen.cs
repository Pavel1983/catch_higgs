using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuScreen : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button rulesButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private RulesPopup rules;

    [SerializeField] private Level levelPrefab;

    [SerializeField] private GamePlayScreen gameplayScreen;
    [SerializeField] private Text title;

    private void OnEnable()
    {
        startButton.onClick.AddListener(OnStart);
        rulesButton.onClick.AddListener(OnRules);
        exitButton.onClick.AddListener(OnExit);

        rules.EventClose += OnRulesClosed;
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnStart);
        rulesButton.onClick.RemoveListener(OnRules);
        exitButton.onClick.RemoveListener(OnExit);
        
        rules.EventClose -= OnRulesClosed;
    }

    private void OnRulesClosed()
    {
        SetButtonsVisible(true);
    }

    private void OnExit()
    {
        Application.Quit();
    }

    private void OnRules()
    {
        SetButtonsVisible(false);
        rules.gameObject.SetActive(true);
    }

    private void SetButtonsVisible(bool flag)
    {
        title.gameObject.SetActive(flag);
        startButton.gameObject.SetActive(flag);
        rulesButton.gameObject.SetActive(flag);
        exitButton.gameObject.SetActive(flag);
    }

    private void OnStart()
    {
        var level = Instantiate(levelPrefab);
        gameplayScreen.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
}
