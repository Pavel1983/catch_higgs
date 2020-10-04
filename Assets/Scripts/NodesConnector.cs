using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeLinks
{
    [SerializeField] private Node sourceNode;
    [SerializeField] private List<Node> nodeList;

    public Node SourceNode => sourceNode;
    public List<Node> NodeList => nodeList;
}

public class NodesConnector : MonoBehaviour
{
    [SerializeField] private List<NodeLinks> nodesLinks;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < nodesLinks.Count; ++i)
        {
            var currentNodeLinks = nodesLinks[i];
            var curNode = currentNodeLinks.SourceNode;
            for (int j = 0; j < currentNodeLinks.NodeList.Count; ++j)
            {
                
            }

        }
    }
}
