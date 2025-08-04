using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandshakeButton : MonoBehaviour {
    [SerializeField] private Transform whiteBackgroundPanel;
    [SerializeField] private Transform whiteBorderPanel;
    [SerializeField] private Transform redPanel;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Button button;
    [SerializeField] private int index;
    private TradeEventHub tradeEventHub;
    private PanelRecolourer whiteBackgroundRecolourer;
    private PanelRecolourer whiteBorderRecolourer;
    private PanelRecolourer redRecolourer;
    private const float STARTING_SCALE = 1.5f;
    private const float ENDING_SCALE = 2.5f;
    private const int DISAPPEAR_FRAMES = 50;



    #region MonoBehaviour
    private void Awake() {
        whiteBackgroundRecolourer = new PanelRecolourer(whiteBackgroundPanel);
        whiteBorderRecolourer = new PanelRecolourer(whiteBorderPanel);
        redRecolourer = new PanelRecolourer(redPanel);
        tradeEventHub = TradeEventHub.Instance;
    }
    private void OnEnable() {
        tradeEventHub.sub_NumberedButtonClicked(someButtonClicked);
        transform.localScale = new Vector3(STARTING_SCALE, STARTING_SCALE, STARTING_SCALE);
        setAlphas(1);
    }
    private void OnDisable() {
        StopAllCoroutines();
    }
    private void OnDestroy() {
        tradeEventHub.unsub_NumberedButtonClicked(someButtonClicked);
    }
    #endregion




    #region public
    public void thisButtonClicked() {
        tradeEventHub.call_NumberedButtonClicked(index);
        button.interactable = false;
        StartCoroutine(expandAndDisappear());
    }
    #endregion



    #region private
    private void someButtonClicked(int otherIndex) {
        if (otherIndex == index - 1) {
            button.interactable = true;
        }
    }
    private IEnumerator expandAndDisappear() {
        Func<float, float> getScale = LinearValue.getFunc(STARTING_SCALE, ENDING_SCALE, DISAPPEAR_FRAMES);
        Func<float, float> getAlpha = LinearValue.getFunc(1f, 0f, DISAPPEAR_FRAMES);
        for (int i = 1; i <= DISAPPEAR_FRAMES; i++) {
            float scale = getScale(i);
            float alpha = getAlpha(i);
            transform.localScale = new Vector3(scale, scale, scale);
            setAlphas(alpha);
            yield return null;
        }
        gameObject.SetActive(false);
        if (index == 3) {
            tradeEventHub.call_HandshakeComplete();
        }
    }
    private void setAlphas(float alpha) {
        changePanelAlpha(whiteBackgroundRecolourer, whiteBackgroundPanel, alpha);
        changePanelAlpha(whiteBorderRecolourer, whiteBorderPanel, alpha);
        changePanelAlpha(redRecolourer, redPanel, alpha);
        textMesh.color = new Color(1f, 1f, 1f, alpha);
    }
    private void changePanelAlpha(PanelRecolourer pr, Transform panel, float alpha) {
        Color currentColour = panel.GetChild(0).GetChild(0).GetComponent<Image>().color;
        currentColour.a = alpha;
        pr.recolour(currentColour);
    }
    #endregion
}
