using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBeingEliminated : MonoBehaviour {
    [SerializeField] private Transform innerPanelTransform;
    [SerializeField] private Transform outerPanelTransform;
    [SerializeField] private TokenIcon tokenIcon;
    [SerializeField] private RectTransform tokenContainerRT;
    [SerializeField] private Image silouhetteImage;
    [SerializeField] private Image tokenImage;
    [SerializeField] private Image outerCircleImage;
    [SerializeField] private Image innerCircleImage;
    [SerializeField] private GOOJFIcon chanceCardIcon;
    [SerializeField] private GOOJFIcon ccCardIcon;
    [SerializeField] private TextMeshProUGUI frontMoneyText;
    [SerializeField] private TextMeshProUGUI backMoneyText;
    [SerializeField] private PropertyGroupIcon[] propertyGroupIcons;
    [SerializeField] private Transform propertyIconContainerPanel;



    #region public
    public IEnumerator eliminatePlayerSequence() {
        yield return pulseOffAllPropertyIcons();

        SoundOnlyEventHub.Instance.call_Punch();
        yield return pulseToken();
        yield return WaitFrames.Instance.frames(10);

        SoundOnlyEventHub.Instance.call_DramaticWail();
        StartCoroutine(becomeSicklyPanelColour());
        StartCoroutine(becomeSicklyTokenColours());
        StartCoroutine(rotateToken());
        StartCoroutine(fadeToken());
        StartCoroutine(fadeAwayMoney());
        fadeAwayPropertyIcons();
        fadeAwayGOOJFCardIcons();
        StartCoroutine(fadeAwayPropertyIconPanel());
        yield return WaitFrames.Instance.frames(FrameConstants.DYING_PLAYER);

        yield return WaitFrames.Instance.frames(50);
        UIEventHub.Instance.call_PlayerEliminatedAnimationOver();
    }
    #endregion



    #region Coroutines
    private IEnumerator pulseOffAllPropertyIcons() {
        foreach (PropertyGroupIcon propertyGroupIcon in propertyGroupIcons) {
            if (!propertyGroupIcon.IsOn) continue;

            SoundOnlyEventHub.Instance.call_AppearingPop();
            yield return propertyGroupIcon.pulseAndTurnOff();
        }
    }
    private IEnumerator becomeSicklyPanelColour() {
        PanelRecolourer outerRecolourer = new PanelRecolourer(outerPanelTransform);
        PanelRecolourer innerRecolourer = new PanelRecolourer(innerPanelTransform);
        PanelColours panelColours = PanelColours.Instance;
        Color outerStart = panelColours.OuterColour.Colour;
        Color outerEnd = panelColours.DeadOuterColour.Colour;
        Color innerStart = panelColours.InnerColour.Colour;
        Color innerEnd = panelColours.DeadInnerColour.Colour;
        Func<float, Color> getOuter = getCoroutineColour(outerStart, outerEnd);
        Func<float, Color> getInner = getCoroutineColour(innerStart, innerEnd);
        for (int i = 1; i <= FrameConstants.DYING_PLAYER; i++) {
            Color outerColour = getOuter(i);
            Color innerColour = getInner(i);
            outerRecolourer.recolour(outerColour);
            innerRecolourer.recolour(innerColour);
            yield return null;
        }
        outerRecolourer.recolour(outerEnd);
        innerRecolourer.recolour(innerEnd);
    }
    private IEnumerator pulseToken() {
        float startScale = 0.11f;
        float bigScale = 0.15f;
        Func<float, float> expand = LinearValue.getFunc(0f, 5f, startScale, bigScale);
        Func<float, float> contract = LinearValue.getFunc(5f, 20f, bigScale, startScale);
        for (int i = 1; i <= 20; i++) {
            float scale = i <= 5 ? expand(i) : contract(i);
            tokenContainerRT.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        tokenContainerRT.localScale = new Vector3(startScale, startScale, startScale);
    }
    private IEnumerator rotateToken() {
        Func<float, float> getRotation = LinearValue.getFunc(0f, -180f, FrameConstants.DYING_PLAYER);
        for (int i = 1; i <= FrameConstants.DYING_PLAYER; i++) {
            float rotation = getRotation(i);
            tokenContainerRT.localRotation = Quaternion.Euler(0f, 0f, rotation);
            yield return null;
        }
        tokenContainerRT.localRotation = Quaternion.Euler(0f, 0f, -180);
    }
    private IEnumerator becomeSicklyTokenColours() {
        Func<float, Color> getFadedColour(Color col1, Color col2) {
            Func<float, float> getRed = LinearValue.getFunc(
                0f, FrameConstants.DYING_PLAYER,
                col1.r, col2.r
            );
            Func<float, float> getGreen = LinearValue.getFunc(
                0f, FrameConstants.DYING_PLAYER,
                col1.g, col2.g
            );
            Func<float, float> getBlue = LinearValue.getFunc(
                0f, FrameConstants.DYING_PLAYER,
                col1.b, col2.b
            );

            Color func(float x) {
                float red = getRed(x);
                float green = getGreen(x);
                float blue = getBlue(x);
                return new Color(red, green, blue);
            }

            return func;
        }


        TokenColours tokenColours = tokenIcon.TokenColours;
        Color outerColour = tokenColours.OuterCircleColour;
        Color innerColour = tokenColours.InnerCircleColour;
        Color deadOuterColour = tokenColours.DeadOuterColour;
        Color deadInnerColour = tokenColours.DeadInnerColour;
        Func<float, Color> getOuterColour = getFadedColour(outerColour, deadOuterColour);
        Func<float, Color> getInnerColour = getFadedColour(innerColour, deadInnerColour);
        for (int i = 1; i <= FrameConstants.DYING_PLAYER; i++) {
            Color newOuter = getOuterColour(i);
            Color newInner = getInnerColour(i);
            outerCircleImage.color = newOuter;
            innerCircleImage.color = newInner;
            yield return null;
        }
        outerCircleImage.color = deadOuterColour;
        innerCircleImage.color = deadInnerColour;
    }
    private IEnumerator fadeToken() {
        TokenColours tokenColours = tokenIcon.TokenColours;
        Color outlineColour = tokenColours.OutlineColour;
        Color tokenColour = tokenColours.TokenColour;
        float endAlpha = 50f / 255f;
        Func<float, float> getAlpha = LinearValue.getFunc(1f, endAlpha, FrameConstants.DYING_PLAYER);

        for (int i = 1; i <= FrameConstants.DYING_PLAYER; i++) {
            float alpha = getAlpha(i);
            outlineColour.a = alpha;
            tokenColour.a = alpha;
            silouhetteImage.color = outlineColour;
            tokenImage.color = tokenColour;
            yield return null;
        }
        outlineColour.a = endAlpha;
        tokenColour.a = endAlpha;
        silouhetteImage.color = outlineColour;
        tokenImage.color = tokenColour;
        yield return null;
    }
    private IEnumerator fadeAwayMoney() {
        Func<float, float> getAlpha = LinearValue.getFunc(1f, 0f, FrameConstants.DYING_PLAYER);

        Color frontColour = frontMoneyText.color;
        Color backColour = backMoneyText.color;
        for (int i = 0; i < FrameConstants.DYING_PLAYER; i++) {
            float alpha = getAlpha(i);
            frontColour.a = alpha;
            backColour.a = alpha;
            frontMoneyText.color = frontColour;
            backMoneyText.color = backColour;
            yield return null;
        }
        Destroy(frontMoneyText.transform.parent.gameObject);
    }
    private void fadeAwayPropertyIcons() {
        foreach (PropertyGroupIcon propertyGroupIcon in propertyGroupIcons) {
            StartCoroutine(propertyGroupIcon.fadeAway());
        }
    }
    private void fadeAwayGOOJFCardIcons() {
        if (chanceCardIcon.IsOn) StartCoroutine(chanceCardIcon.fadeAway());
        if (ccCardIcon.IsOn) StartCoroutine(ccCardIcon.fadeAway());
    }
    private IEnumerator fadeAwayPropertyIconPanel() {
        PanelRecolourer panelRecolourer = new PanelRecolourer(propertyIconContainerPanel);
        yield return panelRecolourer.fadeAway(FrameConstants.DYING_PLAYER);
        Destroy(propertyIconContainerPanel.gameObject);
    }
    #endregion



    #region private
    private Func<float, Color> getCoroutineColour(Color col1, Color col2) {
        Func<float, float> getRed = LinearValue.getFunc(
            0f, FrameConstants.DYING_PLAYER,
            col1.r, col2.r
        );
        Func<float, float> getGreen = LinearValue.getFunc(
            0f, FrameConstants.DYING_PLAYER,
            col1.g, col2.g
        );
        Func<float, float> getBlue = LinearValue.getFunc(
            0f, FrameConstants.DYING_PLAYER,
            col1.b, col2.b
        );

        Color func(float x) {
            float red = getRed(x);
            float green = getGreen(x);
            float blue = getBlue(x);
            return new Color(red, green, blue);
        }

        return func;
    }
    #endregion
}
