using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardManager : MonoBehaviour {
    GamePlayer gamePlayer;
    GameStateInfo gameStateInfo;



    private void Start() {
        GameFactory gameFactory = new();
        gameFactory.makeGame(4);
        gamePlayer = gameFactory.GamePlayer;
        gameStateInfo = gameFactory.GameStateInfo;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CardType cardType = Random.Range(0, 2) == 0 ? CardType.COMMUNITY_CHEST : CardType.CHANCE;
            gamePlayer.drawCard(cardType);

        }
    }
}
