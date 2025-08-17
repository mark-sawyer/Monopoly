using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundEffectAudioSource;
    #region RandomAudioClips
    [SerializeField] private RandomAudioClip diceSounds;
    [SerializeField] private RandomAudioClip paperSounds;
    [SerializeField] private RandomAudioClip wailSounds;
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
    [SerializeField] private AudioClip put;
    [SerializeField] private AudioClip take;
    [SerializeField] private AudioClip swoop;
    [SerializeField] private AudioClip otherChime;
    [SerializeField] private AudioClip flourish;
    [SerializeField] private AudioClip[] risingBoms;
    [SerializeField] private AudioClip brickLaying;
    [SerializeField] private AudioClip dub;
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
        soundEvents.sub_Whistle(play_Whistle);
        soundEvents.sub_CardDrawn(play_CardDrawn);
        soundEvents.sub_Put(play_Put);
        soundEvents.sub_Take(play_Take);
        soundEvents.sub_Swoop(play_Swoop);
        soundEvents.sub_OtherChime(play_OtherChime);
        soundEvents.sub_Flourish(play_Flourish);
        soundEvents.sub_RisingBom(play_Bom);
        soundEvents.sub_BrickLaying(play_BrickLaying);
        soundEvents.sub_Dub(play_Dub);

        uiEvents.sub_CardDrop(play_CardDrop);
        uiEvents.sub_MoneyAppearOrDisappear(play_PaperSound);
        uiEvents.sub_UpdateUIMoney(play_MoneyChing);

        uiPipelineEventHub.sub_RollButtonClicked(play_DiceSound);
        uiPipelineEventHub.sub_MoneyAdjustment(play_MoneyChing);
        uiPipelineEventHub.sub_MoneyBetweenPlayers(play_MoneyChing);
        uiPipelineEventHub.sub_MoneyRaisedForDebt(play_MoneyChing);

        screenEvents.sub_PlayerNumberSelection(play_QuestionChime);
        screenEvents.sub_PlayerNumberConfirmed(play_QuestionChime);
        screenEvents.sub_LuxuryTaxAnimationBegins(play_MupMooo);
        screenEvents.sub_SpinningPoliceman(play_Whistle);
        screenEvents.sub_PayingRentAnimationBegins(play_UhOh);
        screenEvents.sub_PurchaseQuestion(play_QuestionChime);
        screenEvents.sub_IncomeTaxQuestion(play_QuestionChime);
        screenEvents.sub_ResolveMortgage(play_QuestionChime);
        screenEvents.sub_ResolveDebt(play_DunDuuuuuuun);

        uiEvents.sub_SoundButtonClicked(toggleMusic);
    }
    #endregion



    #region private
    private void playSound(AudioClip sound) {
        soundEffectAudioSource.PlayOneShot(sound);
    }
    private void toggleMusic() {
        musicAudioSource.enabled = !musicAudioSource.enabled;
    }
    #endregion



    #region Event listeners
    private void play_DiceSound() => playSound(diceSounds.getRandom());
    private void play_PaperSound() => playSound(paperSounds.getRandom());
    private void play_DramaticWail() => playSound(wailSounds.getRandom());

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
    private void play_QuestionChime() => playSound(questionChime);
    private void play_QuestionChime(int x) => playSound(questionChime);
    private void play_QuestionChime(PlayerInfo x, PropertyInfo y) => playSound(questionChime);
    private void play_QuestionChime(PlayerInfo x) => playSound(questionChime);
    private void play_UhOh(DebtInfo x) => playSound(uhOh);
    private void play_Whistle() => playSound(whistle);
    private void play_DunDuuuuuuun(DebtInfo x) => playSound(dunDuuuuuuun);
    private void play_Punch() => playSound(punch);
    private void play_Put() => playSound(put);
    private void play_Take() => playSound(take);
    private void play_Swoop() => playSound(swoop);
    private void play_OtherChime() => playSound(otherChime);
    private void play_Flourish() => playSound(flourish);
    private void play_Bom(int i) {
        playSound(risingBoms[i - 1]);
    }
    private void play_BrickLaying() => playSound(brickLaying);
    private void play_Dub() => playSound(dub);
    #endregion
}
