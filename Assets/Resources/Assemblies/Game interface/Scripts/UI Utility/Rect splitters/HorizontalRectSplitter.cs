using UnityEngine;

[ExecuteAlways]
public class HorizontalRectSplitter : RectSplitter {
    public override void updateUI() {
        ChildOne.anchorMin = new Vector2(0f, 0f);
        ChildOne.anchorMax = new Vector2(Proportion, 1f);
        ChildTwo.anchorMin = new Vector2(Proportion, 0f);
        ChildTwo.anchorMax = new Vector2(1f, 1f);
    }
}
