using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plant : MonoBehaviour, IPointerClickHandler
{
    private PlantData _currentPlant;
    private int _currStageIndex;

    public PlantData GetPlantData { get => _currentPlant; }
    public int GetPlantStage { get => _currStageIndex; }

    public static Plant Instantiate(PlantData plant, Transform parentTransform)
    {
        GameObject newObj = new GameObject(plant.GetName);
        newObj.transform.SetParent(parentTransform);
        newObj.transform.localPosition = Vector3.zero;

        var newPlant = newObj.AddComponent<Plant>();
        newPlant.StartGrowthPlant(plant);

        return newPlant;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (_currStageIndex == _currentPlant.GetStages.Count)
        {
            Player player = (Player)FindAnyObjectByType(typeof(Player));

            var endPos = player.GetPlayerInventory.GetSeedsCounterTransform;
            TrailItem.PickUpEffect(endPos, _currentPlant.GetItemTexture);

            var harvest = Harvest();
            player?.AddSeeds(harvest.plant, harvest.seedsCount);

        }
    }

    public (PlantData plant, int seedsCount) Harvest()
    {
        (PlantData plant, int seedsCount) res = (_currentPlant, _currentPlant.GetDropSeedsCount);
        _currentPlant = null;
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        GetComponentInParent<PlantSpot>()?.ClearSpot();

        return res;
    }

    private void StartGrowthPlant(PlantData plant)
    {
        _currentPlant = plant;
        StartCoroutine(PlantGrowth(_currentPlant.GetStages[0].GetGrowthDuration, _currentPlant.GetStages[0].GetStageModel));
        _currStageIndex = 0;
    }

    IEnumerator PlantGrowth(float time, GameObject newModel)
    {
        yield return new WaitForSeconds(time);
        _currStageIndex++;
        //clear previous model
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        //set new model
        GameObject newChild = Instantiate(newModel, transform);
        newChild.transform.localPosition = Vector3.zero;

        //start growing again
        if (_currStageIndex < _currentPlant.GetStages.Count)
        {
            StartCoroutine(PlantGrowth(_currentPlant.GetStages[_currStageIndex].GetGrowthDuration, _currentPlant.GetStages[_currStageIndex].GetStageModel));
        }
        else
        {
            newChild.layer = 7;
            var collider = newChild.AddComponent<BoxCollider>();
            collider.isTrigger = true;
        }
    }
}
