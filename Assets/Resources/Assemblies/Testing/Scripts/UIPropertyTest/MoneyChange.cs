using TMPro;
using UnityEngine;

public class MoneyChange : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textMesh;
    public int Amount { get => amount; }
    private int amount = 100;

    void Start() {
        textMesh.text = amount.ToString();
    }

    void Update() {
        if (Input.GetKey(KeyCode.Keypad8)) amount += 1;
        else if (Input.GetKey(KeyCode.Keypad2) && amount > 0) amount -= 1;
        textMesh.text = amount.ToString();
    }
}
