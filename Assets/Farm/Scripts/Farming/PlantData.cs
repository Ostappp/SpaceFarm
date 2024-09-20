using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "Scriptable Objects/Plant")]
public class PlantData : ScriptableObject, Item
{
    [SerializeField] private string _name;
    [SerializeField, Min(1)] private int _plantSeeds;
    [SerializeField, Min(1)] private int _dropSeeds;
    [SerializeField] private Texture _seedsTexture;
    [SerializeField] private List<PlantStage> _stageModels;

    public string GetName { get => _name; }
    public int GetPlantSeedsCount { get => _plantSeeds; }
    public int GetDropSeedsCount { get => _dropSeeds; }
    public Texture GetItemTexture { get => _seedsTexture; }
    public List<PlantStage> GetStages { get => _stageModels; }
    

    [System.Serializable]
    public struct PlantStage
    {
        [SerializeField] private GameObject model;
        [SerializeField] private float duration;
        
        public GameObject GetStageModel { get => model; }
        public float GetGrowthDuration { get => duration; }
    }
}
