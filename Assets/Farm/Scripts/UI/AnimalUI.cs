using UnityEngine;

public class AnimalUI : MonoBehaviour
{
    [SerializeField] private Vector3 _canvasShift = new Vector3(0,.3f,0);
    private AnimalData _animalData;
    private void Start()
    {
        ShowHideUI(false);
        transform.position += _canvasShift;
    }
    void LateUpdate()
    {
        // Створюємо промінь з центру екрану
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Встановлюємо напрямок погляду об'єкта вздовж променя
        transform.rotation = Quaternion.LookRotation(ray.direction);
    }

    public void Initiate(AnimalData animalData)
    {
        _animalData = animalData;
    }

    public void ShowHideUI(bool show)
    {
        foreach (Transform uiItem in transform)
        {
            uiItem.gameObject.SetActive(show);
        }
    }

    public void GetMeat()
    {
        Player player = (Player)FindAnyObjectByType(typeof(Player));
        player?.AddMeat(_animalData, _animalData.GetDropMeatCount);

        //trail
        var endPos = player.GetPlayerInventory.GetMeatCounterTransform;
        TrailItem.PickUpEffect(endPos, _animalData.GetItemTexture);

        Destroy(GetComponentInParent<Animal>().gameObject);
    }
}
