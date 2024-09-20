using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Scriptable Objects/Animal")]
public class AnimalData : ScriptableObject, Item
{
    [SerializeField] private string _name;
    [SerializeField, Min(0)] private int _dropMeat;
    [SerializeField] private Texture _meatTexture;
    [SerializeField] private GameObject _animalHolder;
    [SerializeField] private List<AnimalStage> _stageModels;

    public string GetName { get => _name; }
    public int GetDropMeatCount { get => _dropMeat; }
    public Texture GetItemTexture { get => _meatTexture; }
    public List<AnimalStage> GetStages { get => _stageModels; }
    public GameObject GetAnimalHolder { get => _animalHolder; }








    [System.Serializable]
    public struct AnimalStage
    {
        [SerializeField] private GameObject model;
        [SerializeField] private float duration;

        public GameObject GetStageModel { get => model; }
        public float GetGrowthDuration { get => duration; }
    }
}
