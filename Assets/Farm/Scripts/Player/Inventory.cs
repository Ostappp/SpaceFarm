using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private PlantData _savedPlant;
    [SerializeField] private AnimalData _savedMeat;


    [SerializeField] private TMP_Text _seedsCounter;
    [SerializeField] private TMP_Text _meatCounter;
    private Dictionary<Item, int> _inventoryItems;

    public Transform GetSeedsCounterTransform { get => _seedsCounter.transform; }
    public Transform GetMeatCounterTransform { get => _meatCounter.transform; }

    private void OnEnable()
    {
        _inventoryItems = new Dictionary<Item, int>
        {
            { _savedPlant, PlayerPrefs.GetInt($"{_savedPlant.GetType().Name}:{_savedPlant.GetName}", 10) },
            { _savedMeat, PlayerPrefs.GetInt($"{_savedMeat.GetType().Name}:{_savedMeat.GetName}", 0) }
        };
        _seedsCounter.text = _inventoryItems[_savedPlant].ToString();
        _meatCounter.text = _inventoryItems[_savedMeat].ToString();

        UpdateCounter();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }

    public void AddItem(Item item, int count)
    {
        if (_inventoryItems.ContainsKey(item))
        {
            _inventoryItems[item] += count;
        }
        else
        {
            _inventoryItems.Add(item, count);
        }

        PlayerPrefs.SetInt($"{item.GetType().Name}:{item.GetName}", _inventoryItems[item]);
        PlayerPrefs.Save();
        UpdateCounter();
    }

    public bool RemoveItem(Item item, int count)
    {
        if (_inventoryItems.ContainsKey(item) && _inventoryItems[item] >= count)
        {
            _inventoryItems[item] -= count;
            PlayerPrefs.SetInt($"{item.GetType().Name}:{item.GetName}", _inventoryItems[item]);
            PlayerPrefs.Save();
            UpdateCounter();
            return true;
        }

        UpdateCounter();
        return false;
    }

    public int GetItemCount(Item item)
    {
        return _inventoryItems[item];
    }

    private void UpdateCounter()
    {
        _seedsCounter.text = _inventoryItems[_savedPlant].ToString();
        _meatCounter.text = _inventoryItems[_savedMeat].ToString();
    }


}
