using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Animal : MonoBehaviour, IPointerClickHandler
{
    private Transform _body;
    private AnimalUI _UI;
    private AnimalData _currentAnimal;
    private int _currStageIndex;

    public AnimalData GetAnimalData { get => _currentAnimal; }
    public int GetAnimalStage { get => _currStageIndex; }

    private static Animal _selectedAnimal;

    public static Animal Instantiate(AnimalData animalData, Vector3 position)
    {
        GameObject newObj = Instantiate(animalData.GetAnimalHolder);
        newObj.transform.position = position;

        var newAnimal = newObj.AddComponent<Animal>();
        newAnimal.StartGrowthAnimal(animalData);

        return newAnimal;
    }

    private void Start()
    {
        _UI = GetComponentInChildren<AnimalUI>();
        _UI.transform.SetParent(_body);
        _UI.Initiate(_currentAnimal);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //close ui of another animal
        if (_selectedAnimal != null)
            _selectedAnimal._UI.ShowHideUI(false);

        if (_currStageIndex == _currentAnimal.GetStages.Count)
        {     
            _selectedAnimal = this;
            _UI.ShowHideUI(true);
        }
    }

    private void StartGrowthAnimal(AnimalData animal)
    {
        var body = new GameObject("Model");
        body.transform.SetParent(transform);
        body.transform.localPosition = Vector3.zero;

        _body = body.transform;

        _currentAnimal = animal;
        StartCoroutine(AnimalGrowth(_currentAnimal.GetStages[0].GetGrowthDuration, _currentAnimal.GetStages[0].GetStageModel));
        _currStageIndex = 0;
    }

    IEnumerator AnimalGrowth(float time, GameObject newModel)
    {
        yield return new WaitForSeconds(time);
        _currStageIndex++;
        //clear previous model
        for (int i = _body.transform.childCount - 1; i > 0; i--)//ігноруємо перший елемент (ui)
        {
            Destroy(_body.GetChild(i).gameObject);
        }

        //set new model
        GameObject newChild = Instantiate(newModel, _body);
        newChild.transform.localPosition = Vector3.zero;
        if (newChild.GetComponent<Collider>() == null)
        {
            Renderer renderer = GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                // Додаємо BoxCollider
                BoxCollider boxCollider = newChild.AddComponent<BoxCollider>();

                // Встановлюємо розміри BoxCollider відповідно до розмірів об'єкта
                boxCollider.size = renderer.bounds.size;
                boxCollider.center = renderer.bounds.center - transform.position;
            }
        }

        var rb = newChild.AddComponent<Rigidbody>();
        // Заморожуємо обертання навколо осі X та Z
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        //start growing again
        if (_currStageIndex < _currentAnimal.GetStages.Count)
        {
            StartCoroutine(AnimalGrowth(_currentAnimal.GetStages[_currStageIndex].GetGrowthDuration, _currentAnimal.GetStages[_currStageIndex].GetStageModel));
        }
        else
        {
            newChild.layer = 7;
        }
    }
}
