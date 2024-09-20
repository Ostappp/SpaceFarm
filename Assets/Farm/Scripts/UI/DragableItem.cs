using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static bool IsDragging { get; private set; }

    [SerializeField] private PlantData plant;
    private Transform parentAfterDrag;
    private Image _img;
    public PlantData GetPlantType { get => plant; }
    private void Start()
    {
        _img = GetComponent<Image>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        IsDragging = true;
        _img.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        IsDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        IsDragging = false;
        _img.raycastTarget = true;
    }
}
