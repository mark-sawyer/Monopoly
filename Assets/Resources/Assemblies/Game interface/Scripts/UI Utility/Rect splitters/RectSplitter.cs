using UnityEngine;

public abstract class RectSplitter : UIAutoUpdater {
    [SerializeField] private float proportion;
    [SerializeField] private RectTransform childOne;
    [SerializeField] private RectTransform childTwo;
    private float lastProportion;
    public float Proportion => Mathf.Clamp01(proportion);

    protected RectTransform ChildOne => childOne;
    protected RectTransform ChildTwo => childTwo;

    public override bool changeOccurred() {
        return lastProportion != proportion;
    }
    public override void updateLastState() {
        lastProportion = proportion;
    }
}
