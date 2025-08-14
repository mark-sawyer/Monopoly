using UnityEngine;
using UnityEngine.UI;

public class Confetti : MonoBehaviour {
    [SerializeField] private RectTransform RT;
    [SerializeField] private Image image;
    private Quaternion rotation;
    private Vector3 direction;
    private float disappearHeight;
    private const float SPEED = 5;
    private const float ANGLE_SPEED = 10;



    #region MonoBehaviour
    private void Start() {
        direction = getDirection();
        rotation = getRotation();
        image.color = getColour();
        disappearHeight = ((RectTransform)transform.parent.parent.parent).rect.height + 300f;
    }
    private void Update() {
        RT.localPosition += direction;
        RT.rotation *= rotation;
        float poop = RT.anchoredPosition.y;
        if (RT.anchoredPosition.y < -disappearHeight) Destroy(gameObject);
    }
    #endregion



    #region private
    private Vector3 getDirection() {
        float degrees = Random.Range(240f, 300f);
        float radians = Mathf.Deg2Rad * degrees;
        float xComponent = Mathf.Cos(radians);
        float yComponent = Mathf.Sin(radians);
        Vector3 unitVector = new Vector3(xComponent, yComponent, 0f);
        return unitVector * SPEED;
    }
    private Quaternion getRotation() {
        Vector3 randomDirection = Random.onUnitSphere;
        return Quaternion.AngleAxis(ANGLE_SPEED, randomDirection);
    }
    private Color getColour() {
        return new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );
    }
    #endregion
}
