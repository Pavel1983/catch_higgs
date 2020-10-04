using UnityEngine;
using UnityEngine.EventSystems;

public class NodeController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color blockColor;
    [SerializeField] private Color freeColor;

    private Node node;
    private bool blocked;
    private SpriteRenderer renderer;

    private void OnEnable()
    {
        node.EventBlockedChanged += OnBlockedStateChanged;
    }

    private void OnDisable()
    {
        node.EventBlockedChanged -= OnBlockedStateChanged;
    }

    private void OnBlockedStateChanged()
    {
        if (node.Blocked)
            renderer.color = blockColor;
        else
            renderer.color = freeColor;
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        node = GetComponent<Node>();
        OnBlockedStateChanged();
    }

    #region IPointerClickHandler impl
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        node.Blocked = !node.Blocked;
    }
    #endregion
}
