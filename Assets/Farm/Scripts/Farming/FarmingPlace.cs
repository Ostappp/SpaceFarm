using UnityEngine;

public class FarmingPlace : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && DragableItem.IsDragging)
        {
            Highlight(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && DragableItem.IsDragging)
        {
            Highlight(true);
        }
        else if (other.CompareTag("Player"))
        {
            Highlight(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Highlight(false);
        }
    }
    void Highlight(bool highlight)
    {
        PlantSpot[] spots = GetComponentsInChildren<PlantSpot>();
        foreach (PlantSpot spot in spots)
        {
            spot.HighlightSpot(highlight);
        }
    }
}
