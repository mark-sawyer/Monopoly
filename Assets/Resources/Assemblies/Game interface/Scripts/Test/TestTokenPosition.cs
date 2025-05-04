using UnityEngine;

public class TestTokenPosition : MonoBehaviour {
    private BaseTokenOffsets baseTokenOffsets;
    private TokenScales tokenScales;
    private int tokens;
    private int order;

    private void Update() {
        adjustScaleAndSize();
    }

    public void setup(BaseTokenOffsets baseTokenOffsets, TokenScales tokenScales, int tokens, int order) {
        this.baseTokenOffsets = baseTokenOffsets;
        this.tokenScales = tokenScales;
        this.tokens = tokens;
        this.order = order;
    }

    public void adjustScaleAndSize() {
        Vector3 position = baseTokenOffsets.getTotalOffset(tokens, order);
        transform.localPosition = position;
        transform.localScale = new Vector3(tokenScales.getScaleValue(tokens - 1), tokenScales.getScaleValue(tokens - 1), 1f);
    }
}
