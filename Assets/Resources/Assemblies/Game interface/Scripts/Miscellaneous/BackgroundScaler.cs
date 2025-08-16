using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour {
    private void Start() {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;
        float maxHalfExtent = Mathf.Sqrt(Mathf.Pow(halfHeight, 2) + Mathf.Pow(halfWidth, 2));
        float requiredSize = maxHalfExtent * 2f;
        transform.localScale = new Vector3(requiredSize, requiredSize, requiredSize);
    }
}
