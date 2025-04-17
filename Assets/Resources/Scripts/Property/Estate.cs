
public class Estate : Property {
    private EstateGroup estateGroup;

    public Estate(Space space, string name, int cost, EstateGroup estateGroup) : base(space, name, cost) {
        this.estateGroup = estateGroup;
    }
}
