using UnityEngine;
using UnityEngine.EventSystems;

public class PlantSpot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Color _highlightColor = Color.green;

    private Plant _currentPlant;
    private bool _isGlowing;
    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = GetComponent<MeshRenderer>().material.color;
    }
    public bool PlantSeed(PlantData plant)
    {
        if (_currentPlant == null)
        {
            _currentPlant = Plant.Instantiate(plant, transform);
            return true;
        }

        return false;
    }

    public void ClearSpot()
    {
        _currentPlant = null;
        _isGlowing = false;
    }

    public void HighlightSpot(bool activate)
    {
        if(_currentPlant == null)
        {
            if (activate && !_isGlowing)
            {
                GetComponent<MeshRenderer>().material.color = _highlightColor;
                _isGlowing = true;
            }
            else if(!activate && _isGlowing)
            {
                GetComponent<MeshRenderer>().material.color = _defaultColor;
                _isGlowing = false;
            }
        }  
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent(out DragableItem item))
        {
            Player player = (Player)FindAnyObjectByType(typeof(Player));
            player?.PlantSeeds(item.GetPlantType, GetComponent<PlantSpot>());
            GetComponent<MeshRenderer>().material.color = _defaultColor;
        }
    }
}
