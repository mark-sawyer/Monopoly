using System.Globalization;
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
    #endregion


    #region MonoBehaviour
    private void Start() {
        UIEventHub uiEvents = UIEventHub.Instance;
        ScreenAnimationEventHub screenEvents = ScreenAnimationEventHub.Instance;

        uiEvents.sub_ButtonDown(play_buttonDown);
        uiEvents.sub_ButtonUp(play_buttonUp);
        uiEvents.sub_RollButtonClicked(play_DiceSound);
        uiEvents.sub_MoneyAdjustment(play_MoneyChing);
        uiEvents.sub_MoneyBetweenPlayers(play_MoneyChing);
        uiEvents.sub_CorrectOutcome(play_CorrectSound);
        uiEvents.sub_IncorrectOutcome(play_IncorrectSound);
        uiEvents.sub_CardDrop(play_CardDrop);
        uiEvents.sub_MoneyAppearOrDisappear(play_PaperSound);
        uiEvents.sub_AppearingPop(play_Pop);
        uiEvents.sub_PlayerGetsGOOJFCard(play_Pop);

        screenEvents.sub_LuxuryTaxAnimationBegins(play_MupMooo);
        screenEvents.sub_SpinningPoliceman(play_Whistle);
        screenEvents.sub_PayingRentAnimationBegins(play_UhOh);
        screenEvents.sub_CardShown(play_CardDrawn);
        screenEvents.sub_PurchaseQuestion(play_QuestionChime);
        screenEvents.sub_IncomeTaxQuestion(play_QuestionChime);
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
    private void play_MoneyChing(PlayerInfo x) => playSound(moneyChing);
    private void play_MoneyChing(PlayerInfo x, PlayerInfo y) => playSound(moneyChing);
    private void play_MupMooo() => playSound(mupMooo);
    private void play_Pop() => playSound(pop);
    private void play_Pop(PlayerInfo x, CardType y) => playSound(pop);
    private void play_Pop(PlayerInfo x, PropertyInfo y) => playSound(pop);
    private void play_QuestionChime(PlayerInfo x, PropertyInfo y) => playSound(questionChime);
    private void play_QuestionChime(PlayerInfo x) => playSound(questionChime);
    private void play_UhOh(DebtInfo x) => playSound(uhOh);
    private void play_Whistle() => playSound(whistle);
    #endregion
}
