using UnityEngine;

public class TokenSprites : ScriptableObject {
    public Sprite SilouhetteSprite { get => silouhetteSprite; }
    public Sprite ForegroundSprite { get => foregroundSprite; }
    [SerializeField] private Sprite foregroundSprite;
    [SerializeField] private Sprite silouhetteSprite;
}
