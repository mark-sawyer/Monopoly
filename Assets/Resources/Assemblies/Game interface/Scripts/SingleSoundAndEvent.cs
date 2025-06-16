using UnityEngine;

[CreateAssetMenu(menuName = "SoundEventPair/SingleSoundAndEvent")]
public class SingleSoundAndEvent : SoundEventPair {
    [SerializeField] private AudioClip audioClip;
    public override AudioClip AudioClip => audioClip;
}
