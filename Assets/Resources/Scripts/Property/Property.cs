
public abstract class Property {
    private Player owner;
    public Space space { get; private set; }
    private string name;
    private int cost;

    public Property(Space space, string name, int cost) {
        this.space = space;
        this.name = name;
        this.cost = cost;
    }
}
