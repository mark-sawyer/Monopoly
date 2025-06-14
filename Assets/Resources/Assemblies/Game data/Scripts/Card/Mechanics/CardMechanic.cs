using UnityEngine;

internal abstract class CardMechanic : ScriptableObject, CardMechanicInfo {
    private Game game;

    #region internal
    internal void setup(Game game) {
        this.game = game;
    }
    #endregion

    #region protected
    protected Game Game => game;
    #endregion
}

public interface CardMechanicInfo { }
