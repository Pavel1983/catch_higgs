using System;
using UnityEngine;
using UnityEngine.UI;

public class RulesPopup : MonoBehaviour
{
    public event Action EventClose;
    
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnClose);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(OnClose);
    }

    private void OnClose()
    {
        EventClose?.Invoke();
        gameObject.SetActive(false);
    }
}
