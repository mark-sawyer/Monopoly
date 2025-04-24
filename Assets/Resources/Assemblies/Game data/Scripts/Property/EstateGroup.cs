
internal class EstateGroup {
    private Estate[] estates;
    public EstateColour estateColour { get; private set; }

    public EstateGroup(EstateColour estateColour, Estate[] estates) {
        this.estateColour = estateColour;
        this.estates = estates;
    }
    public void setEstateReferencesToThis() {
        foreach (Estate estate in estates) {
            estate.assignEstateGroup(this);
        }
    }
}
