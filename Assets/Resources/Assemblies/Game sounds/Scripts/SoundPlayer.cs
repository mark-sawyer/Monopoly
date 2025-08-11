using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    #region RandomAudioClips
    [SerializeField] private RandomAudioClip diceSounds;
    [SerializeField] private RandomAudioClip paperSounds;
    #endregion
    #region AudioClips
    [SerializeField] private AudioClip buttonDown;
    [SerializeField] private AudioClip buttonUp;
    [SerializeField] private AudioClip cardDrawn;
    [SerializeField] private AudioClip cardDrop;
    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip incorrect;
    [SerializeField] private AudioClip moneyChing;
    [SerializeField] private AudioClip mupMooo;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip questionChime;
    [SerializeField] private AudioClip uhOh;
    [SerializeField] private AudioClip whistle;
    [SerializeField] private AudioClip dunDuuuuuuun;
    [SerializeField] private AudioClip punch;
    [SerializeField] private AudioClip dramaticWail;
    #endregion



    #region MonoBehaviour
    private void Start() {
        SoundOnlyEventHub soundEvents = SoundOnlyEventHub.Instance;
        UIEventHub uiEvents = UIEventHub.Instance;
        UIPipelineEventHub uiPipelineEventHub = UIPipelineEventHub.Instance;
        ScreenOverlayEventHub screenEvents = ScreenOverlayEventHub.Instance;

        soundEvents.sub_ButtonDown(play_buttonDown);
        soundEvents.sub_ButtonUp(play_buttonUp);
        soundEvents.sub_CorrectOutcome(play_CorrectSound);
        soundEvents.sub_IncorrectOutcome(play_IncorrectSound);
        soundEvents.sub_DramaticWail(play_DramaticWail);
        soundEvents.sub_AppearingPop(play_Pop);
        soundEvents.sub_Punch(play_Punch);

        uiEvents.sub_CardDrop(play_CardDrop);
        uiEvents.sub_MoneyAppearOrDisappear(play_PaperSound);
        uiEvents.sub_UpdateUIMoney(play_MoneyChing);

        uiPipelineEventHub.sub_RollButtonClicked(play_DiceSound);
        uiPipelineEventHub.sub_MoneyAdjustment(play_MoneyChing);
        uiPipelineEventHub.sub_MoneyBetweenPlayers(play_MoneyChing);
        uiPipelineEventHub.sub_MoneyRaisedForDebt(play_MoneyChing);

        screenEvents.sub_LuxuryTaxAnimationBegins(play_MupMooo);
        screenEvents.sub_SpinningPoliceman(play_Whistle);
        screenEvents.sub_PayingRentAnimationBegins(play_UhOh);
        screenEvents.sub_CardShown(play_CardDrawn);
        screenEvents.sub_PurchaseQuestion(play_QuestionChime);
        screenEvents.sub_IncomeTaxQuestion(play_QuestionChime);
        screenEvents.sub_ResolveMortgage(play_QuestionChime);
        screenEvents.sub_ResolveDebt(play_DunDuuuuuuun);
    }
    #endregion



    #region private
    private void playSound(AudioClip sound) {
        audioSource.PlayOneShot(sound);
    }
    #endregion



    #region Event listeners
    private void play_DiceSound() => playSound(diceSounds.getRandom());
    private void play_PaperSound() => playSound(paperSounds.getRandom());

    private void play_buttonDown() => playSound(buttonDown);
    private void play_buttonUp() => playSound(buttonUp);
    private void play_CardDrawn() => playSound(cardDrawn);
    private void play_CardDrop() => playSound(cardDrop);
    private void play_CorrectSound() => playSound(correct);
    private void play_IncorrectSound() => playSound(incorrect);
    private void play_MoneyChing() => playSound(moneyChing);
    private void play_MoneyChing(PlayerInfo x) => playSound(moneyChing);
    private void play_MoneyChing(PlayerInfo x, PlayerInfo y) => playSound(moneyChing);
    private void play_MoneyChing(PlayerInfo[] x) => playSound(moneyChing);
    private void play_MupMooo() => playSound(mupMooo);
    private void play_Pop() => playSound(pop);
    private void play_QuestionChime(PlayerInfo x, PropertyInfo y) => playSound(questionChime);
    private void play_QuestionChime(PlayerInfo x) => playSound(questionChime);
    private void play_UhOh(DebtInfo x) => playSound(uhOh);
    private void play_Whistle() => playSound(whistle);
    private void play_DunDuuuuuuun(DebtInfo x) => playSound(dunDuuuuuuun);
    private void play_Punch() => playSound(punch);
    private void play_DramaticWail() => playSound(dramaticWail);
    #endregion
}
