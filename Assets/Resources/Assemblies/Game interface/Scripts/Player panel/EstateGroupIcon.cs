using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EstateGroupIcon : MonoBehaviour {
    [SerializeField] private ScriptableObject estateGroup;
    [SerializeField] private Panel panel;
    private EstateGroupInfo estateGroupInfo;

    private void Start() {
        estateGroupInfo = (EstateGroupInfo)estateGroup;
        Color estateColour = estateGroupInfo.EstateColour;
        estateColour.a = 0.125f;
        panel.Colour = estateColour;
    }
}
