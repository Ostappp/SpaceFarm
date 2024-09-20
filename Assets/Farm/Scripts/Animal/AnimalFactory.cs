using System.Collections;
using UnityEngine;

public class AnimalFactory : MonoBehaviour
{
    [SerializeField] private AnimalData _currentAnimal;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField, Min(0)] private float _newSpawnTime = 10;

    private Animal _createdAnimal;
    private bool _isCreatingAnimal = false;

    private void Update()
    {
        if(!_isCreatingAnimal && _createdAnimal == null)
        {
            StartCoroutine(CreateAnimal(_newSpawnTime));
        }
    }
    public IEnumerator CreateAnimal(float delay)
    {
        _isCreatingAnimal = true;

        if (_createdAnimal == null)
        {
            yield return new WaitForSeconds(delay);
            _createdAnimal = Animal.Instantiate(_currentAnimal, transform.position + _spawnPosition);
        }

        _isCreatingAnimal = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + _spawnPosition, 0.1f);
    }
}
