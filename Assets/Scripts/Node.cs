using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Node : MonoBehaviour
{
    public event Action EventBlockedChanged = null;
    
    [SerializeField] private string id;
    [SerializeField] private List<Node> nodeList;

    // locals
    private bool blocked;
    
    // properties
    public string ID => id;
    public bool Blocked
    {
        get => blocked;
        set
        {
            if (blocked != value)
            {
                blocked = value;
                EventBlockedChanged?.Invoke();
            }
        }
    }

    public IReadOnlyCollection<Node> NodeList => nodeList;

    private void Start()
    {
        var lr = gameObject.GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.sortingOrder = 2;
        lr.positionCount = nodeList.Count * 2;
        for (int i = 0; i < nodeList.Count; ++i)
        {
            int k = i * 2;
            lr.SetPosition(k, transform.position);
            lr.SetPosition(k+1, nodeList[i].transform.position);
        }
    }

#if UNITY_EDITOR
    #region Editor stuff
    private void OnDrawGizmos()
    {
        var content = new GUIContent(id);
        var style = new GUIStyle()
        {
            fontSize = 22
        };
        
        Handles.Label(transform.position, content, style);

        for (int i = 0; i < nodeList.Count; ++i)
        {
            if (nodeList[i] != null)
                Gizmos.DrawLine(transform.position, nodeList[i].transform.position);
        }
    }
    #endregion
    #endif
}
