using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PropertySelected : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textMesh;
    public int ID => id;
    private int id = 1;

    void Start() {
        textMesh.text = id.ToString();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) id = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) id = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) id = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) id = 4;
        textMesh.text = id.ToString();
    }
}
