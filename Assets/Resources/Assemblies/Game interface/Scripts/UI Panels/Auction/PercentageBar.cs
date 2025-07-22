using UnityEngine;
using UnityEngine.UI;

public class PercentageBar : MonoBehaviour {
    [SerializeField] private RectTransform containerRT;
    [SerializeField] private RectTransform barRT;
    [SerializeField] private Image barImage;
    private float totalMoney;
    private float containerWidth;
    private float barHeight;



    #region MonoBehaviour
    private void Start() {
        containerWidth = containerRT.rect.width;
        barHeight = barRT.rect.height;
    }
    #endregion



    #region public
    public void setup(int totalMoney) {
        this.totalMoney = totalMoney;
    }
    public void adjustVisual(int currentBid) {
        float proportion = currentBid / totalMoney;
        float newWidth = containerWidth * proportion;
        barRT.sizeDelta = new Vector2(newWidth, barHeight);
        Color barColour = getBarColour(proportion);
        barImage.color = barColour;
    }
    #endregion



    #region private
    private Color getBarColour(float proportion) {
        float getRed() {
            if (proportion <= 0.5f) return 2 * proportion;
            else return 1f;
        }
        float getGreen() {
            if (proportion <= 0.5f) return 1f;
            else return (-2 * proportion) + 2f;
        }

        float red = getRed();
        float green = getGreen();
        Color colour = new Color(red, green, 0f);
        return colour;
    }
    #endregion
}
