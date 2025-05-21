using TMPro;
using UnityEngine;

public class RailroadVisual : SpaceVisual {
    [SerializeField] ScriptableObject railroadSO;
    [SerializeField] TextMeshPro railroadNameText;

    private void Start() {
        RailroadInfo railroad = (RailroadInfo)railroadSO;
        railroadNameText.text = railroad.Name.ToUpper();
    }
}
