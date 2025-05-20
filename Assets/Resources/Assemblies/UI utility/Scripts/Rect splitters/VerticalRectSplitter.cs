using UnityEngine;

internal class VerticalRectSplitter : RectSplitter {
    public override void updateUI() {
        ChildOne.anchorMin = new Vector2(0f, 1f - Proportion);
        ChildOne.anchorMax = new Vector2(1f, 1f);
        ChildTwo.anchorMin = new Vector2(0f, 0f);
        ChildTwo.anchorMax = new Vector2(1f, 1f - Proportion);
    }
}
