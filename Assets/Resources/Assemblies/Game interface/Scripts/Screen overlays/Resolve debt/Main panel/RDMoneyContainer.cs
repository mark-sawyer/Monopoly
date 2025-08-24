using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class RDMoneyContainer : MonoBehaviour {
    [SerializeField] private RectTransform rt;
    [SerializeField] private RectTransform moneyBackRT;



    private void LateUpdate() {
        LayoutRebuilder.ForceRebuildLayoutImmediate(moneyBackRT);
        float width = moneyBackRT.rect.width;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
