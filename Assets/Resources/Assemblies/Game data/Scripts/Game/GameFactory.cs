
public class GameFactory {
    public GamePlayer GamePlayer => game;
    public GameStateInfo GameStateInfo => game;
    private Game game;

    public void makeGame(int playerNum) {
        game = new Game(playerNum, new Dice());
    }
    public void makeRiggedDiceGame(int playerNum) {
        game = new Game(playerNum, new RiggedDice());
    }
}
