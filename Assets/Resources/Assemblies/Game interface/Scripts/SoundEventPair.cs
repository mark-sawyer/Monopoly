using UnityEngine;

public abstract class SoundEventPair : ScriptableObject {
    [SerializeField] private GameEvent gameEvent;
    public abstract AudioClip AudioClip { get; }
    public GameEvent GameEvent => gameEvent;
}
