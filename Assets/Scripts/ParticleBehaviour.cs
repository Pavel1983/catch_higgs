using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct ParticleStats
{
    [SerializeField] private float speed;

    public float Speed => speed;
}

public class ParticleBehaviour : MonoBehaviour
{
    public event Action EventDestroy;
    public static event Action<ParticleBehaviour> EventReachedSystem;
    public static event Action<ParticleBehaviour> EventLeftSystem;
    
    [SerializeField] private ParticleStats stats;
    
    private Node source;
    private Node destination;

    public float Speed => stats.Speed;
    
    public void SetupAndStart(Node fromNode)
    {
        SetupDestination(fromNode, GetNextNode(fromNode));
    }

    private void SetupDestination(Node from, Node to)
    {
        source = from;
        destination = to;
        
        bool sourceIsSpawner = source.GetComponent<NodeParticleSpawner>() != null;
        bool destinationIsSpawner = destination.GetComponent<NodeParticleSpawner>() != null;
        
        if (!sourceIsSpawner && destinationIsSpawner)
            EventLeftSystem?.Invoke(this);
        
        Debug.Log($"{source.ID} => {destination.ID}");
        StartCoroutine(DoMove(OnFinishedMoving));
    }

    private void OnFinishedMoving()
    {
        bool sourceIsSpawner = source.GetComponent<NodeParticleSpawner>() != null;
        bool destinationIsSpawner = destination.GetComponent<NodeParticleSpawner>() != null;
        
        if (sourceIsSpawner && !destinationIsSpawner)
            EventReachedSystem?.Invoke(this);

        // если попали в источник то удаляем себя
        if (destinationIsSpawner)
        {
            EventDestroy?.Invoke();
            Destroy(this.gameObject);
            return;
        }

        if (destination.Blocked)
            SetupDestination(destination, source);
        else
            SetupDestination(destination, GetNextNode(destination));
    }

    private Node GetNextNode(Node node)
    {
        // выбрать все кроме той ноды с которой пришли
        var allNodes = node.NodeList.ToList();
        if (allNodes.Count > 1)
            allNodes.Remove(source);
        
        return allNodes[Random.Range(0, allNodes.Count)];
    }

    private IEnumerator DoMove(Action onFinished)
    {
        float timer = 0.0f;
        Vector3 dir = destination.transform.position - source.transform.position;
        float distance = dir.magnitude;
        float pathTime = distance / stats.Speed;
        while (timer < pathTime)
        {
            timer += stats.Speed * Time.deltaTime;
            transform.position = source.transform.position + dir * (timer / pathTime);
            yield return null;
        }
        
        onFinished?.Invoke();
    }
}
