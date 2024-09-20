using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlantData _plantFarming;
    [SerializeField] private Inventory _inventory;

    public Inventory GetPlayerInventory { get => _inventory; }
    
    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void AddSeeds(PlantData plant, int count)
    {
        _inventory.AddItem(plant, count);
    }

    public void AddMeat(AnimalData animal, int count)
    {
        _inventory.AddItem(animal, count);
    }

    public void PlantSeeds(PlantData plant, PlantSpot spot)
    {
        if (_inventory.GetItemCount(plant) >= plant.GetPlantSeedsCount)
        {
            if (spot.PlantSeed(_plantFarming))
                _inventory.RemoveItem(_plantFarming, plant.GetPlantSeedsCount);
        }
    }
}
