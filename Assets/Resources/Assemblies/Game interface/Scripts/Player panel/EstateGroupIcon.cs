using UnityEngine;

[ExecuteAlways]
public class EstateGroupIcon : MonoBehaviour {
    [SerializeField] private ScriptableObject estateGroup;
    [SerializeField] private MonoBehaviour panel;
    private EstateGroupInfo estateGroupInfo;

    private void Start() {
        estateGroupInfo = (EstateGroupInfo)estateGroup;
        Color estateColour = estateGroupInfo.EstateColour;
        estateColour.a = 0.125f;
        ((TypeSettable<Color>)panel).setType(estateColour);
    }
}
