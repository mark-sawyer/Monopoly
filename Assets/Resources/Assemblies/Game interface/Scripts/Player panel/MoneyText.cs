using TMPro;
using UnityEngine;

[ExecuteAlways]
public class MoneyText : MonoBehaviour, TypeSettable<string> {
    [SerializeField] private TextMeshProUGUI textMesh;

    public void setType(string type) {
        textMesh.text = type;
    }
}
