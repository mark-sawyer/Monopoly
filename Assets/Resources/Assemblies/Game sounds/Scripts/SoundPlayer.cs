using System.Collections;
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
    [SerializeField] private AudioClip questionChime;
    [SerializeField] private AudioClip tradeChime;
    [SerializeField] private AudioClip otherChime;
    [SerializeField] private AudioClip buttonDown;
    [SerializeField] private AudioClip buttonUp;
    [SerializeField] private AudioClip cardDrawn;
    [SerializeField] private AudioClip cardDrop;
    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip incorrect;
    [SerializeField] private AudioClip moneyChing;
    [SerializeField] private AudioClip mupMooo;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip uhOh;
    [SerializeField] private AudioClip whistle;
    [SerializeField] private AudioClip dunDuuuuuuun;
    [SerializeField] private AudioClip punch;
    [SerializeField] private AudioClip put;
    [SerializeField] private AudioClip take;
    [SerializeField] private AudioClip swoop;
    [SerializeField] private AudioClip flourish;
    [SerializeField] private AudioClip[] risingBoms;
    [SerializeField] private AudioClip brickLaying;
    [SerializeField] private AudioClip dub;
    [SerializeField] private AudioClip unlockAndSqueak;
    [SerializeField] private AudioClip tinyPut;
    [SerializeField] private AudioClip wipe;
    [SerializeField] private AudioClip congratulations;
    [SerializeField] private AudioClip winner;
    [SerializeField] private AudioClip applause;
    [SerializeField] private AudioClip ding;
    [SerializeField] private AudioClip dingDing;
    #endregion



    #region Singleton boilerplate
    public static SoundPlayer Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    #region private
    private void playSound(AudioClip sound) {
        soundEffectAudioSource.PlayOneShot(sound);
    }
    private IEnumerator playSoundAndWait(AudioClip sound) {
        soundEffectAudioSource.clip = sound;
        soundEffectAudioSource.Play();
        while (soundEffectAudioSource.isPlaying) {
            yield return null;
        }
    }
    #endregion



    #region Sound effects
    public void play_QuestionChime() => playSound(questionChime);
    public void play_OtherChime() => playSound(otherChime);
    public void play_TradeChime() => playSound(tradeChime);
    public void play_DiceSound() => playSound(diceSounds.getRandom());
    public void play_PaperSound() => playSound(paperSounds.getRandom());
    public void play_DramaticWail() => playSound(wailSounds.getRandom());
    public void play_ButtonDown() => playSound(buttonDown);
    public void play_ButtonUp() => playSound(buttonUp);
    public void play_CardDrawn() => playSound(cardDrawn);
    public void play_CardDrop() => playSound(cardDrop);
    public void play_CorrectSound() => playSound(correct);
    public void play_IncorrectSound() => playSound(incorrect);
    public void play_MoneyChing() => playSound(moneyChing);
    public void play_MupMooo() => playSound(mupMooo);
    public void play_Pop() => playSound(pop);
    public void play_UhOh() => playSound(uhOh);
    public void play_Whistle() => playSound(whistle);
    public void play_DunDuuuuuuun() => playSound(dunDuuuuuuun);
    public void play_Punch() => playSound(punch);
    public void play_Put() => playSound(put);
    public void play_Take() => playSound(take);
    public void play_Swoop() => playSound(swoop);
    public void play_Flourish() => playSound(flourish);
    public void play_Bom(int i) {
        playSound(risingBoms[i - 1]);
    }
    public void play_BrickLaying() => playSound(brickLaying);
    public void play_Dub() => playSound(dub);
    public void play_TinyPut() => playSound(tinyPut);
    public void play_Wipe() => playSound(wipe);
    public void play_Congratulations(PlayerInfo playerInfo) {
        IEnumerator congratulationsSequence(AudioClip tokenSound, AudioClip colourSound) {
            yield return playSoundAndWait(applause);
            yield return playSoundAndWait(congratulations);
            yield return playSoundAndWait(colourSound);
            yield return playSoundAndWait(tokenSound);
            yield return playSoundAndWait(winner);
        }

        Token token = playerInfo.Token;
        PlayerColour colour = playerInfo.Colour;
        AudioClip tokenSound = TokenSoundDictionary.Instance.getTokenSound(token);
        AudioClip colourSound = TokenSoundDictionary.Instance.getColourSound(colour);
        StartCoroutine(congratulationsSequence(tokenSound, colourSound));
    }
    public void play_DoublesDing(int doublesCount) {
        if (doublesCount == 1) playSound(ding);
        else if (doublesCount == 2) playSound(dingDing);
        else throw new System.Exception("Doubles count should only be one or two for ding sounds.");
    }
    public void play_UnlockAndSqueak() => playSound(unlockAndSqueak);
    #endregion



    #region Music
    public void toggleMusic() {
        musicAudioSource.enabled = !musicAudioSource.enabled;
    }
    #endregion
}
