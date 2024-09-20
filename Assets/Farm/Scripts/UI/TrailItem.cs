using UnityEngine;
using UnityEngine.UI;

public class TrailItem : MonoBehaviour
{
    private Transform _endPosition;
    private float _speed = 5f;

    private void Update()
    {
        if (_endPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition.position, _speed);

            if (Vector3.Distance(transform.position, _endPosition.position) < 0.01f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PickUpEffect(Transform endPosition, Texture texture, float speed = 5f)
    {
        if (endPosition != null && texture != null && speed > 0)
        {
            GameObject icon = new GameObject("ResourceIcon");

            Image iconImage = icon.AddComponent<Image>();
            iconImage.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            icon.transform.SetParent(endPosition.transform, false);
            icon.transform.position = Input.mousePosition;

            var trail = icon.AddComponent<TrailItem>();
            trail._endPosition = endPosition;
            trail._speed = speed;
        }
    }

}
