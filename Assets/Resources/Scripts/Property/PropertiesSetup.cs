
public static class PropertiesSetup {
    public static Property[] getProperties(Space[] spaces) {
        EstateGroup[] estateGroups = new EstateGroup[] {
            new EstateGroup(EstateColour.BROWN),
            new EstateGroup(EstateColour.LIGHT_BLUE),
            new EstateGroup(EstateColour.PINK),
            new EstateGroup(EstateColour.ORANGE),
            new EstateGroup(EstateColour.RED),
            new EstateGroup(EstateColour.YELLOW),
            new EstateGroup(EstateColour.GREEN),
            new EstateGroup(EstateColour.DARK_BLUE)
        };
        Property[] properties = new Property[] {
            new Estate(spaces[1], "Old Kent Road", 60, estateGroups[0]),
            new Estate(spaces[3], "Whitechapel Road", 60, estateGroups[0]),
            new Railroad(spaces[5], "Kings Cross Station", 200),
            new Estate(spaces[6], "The Angel, Islington", 100, estateGroups[1]),
            new Estate(spaces[8], "Euston Road", 100, estateGroups[1]),

            new Estate(spaces[9], "Pentonville Road", 120, estateGroups[1]),
            new Estate(spaces[11], "Pall Mall", 140, estateGroups[2]),
            new Utility(spaces[12], "Electric Company", 150),
            new Estate(spaces[13], "Whitehall", 140, estateGroups[2]),
            new Estate(spaces[14], "Northumberland Avenue", 160, estateGroups[2]),

            new Railroad(spaces[15], "Marylebone Station", 200),
            new Estate(spaces[16], "Bow Street", 180, estateGroups[3]),
            new Estate(spaces[18], "Marlborough Street", 180, estateGroups[3]),
            new Estate(spaces[19], "Vine Street", 200, estateGroups[3]),
            new Estate(spaces[21], "Strand", 220, estateGroups[4]),

            new Estate(spaces[23], "Fleet Street", 220, estateGroups[4]),
            new Estate(spaces[24], "Trafalgar Square", 240, estateGroups[4]),
            new Railroad(spaces[25], "Fenchurch St Station", 200),
            new Estate(spaces[26], "Leicester Square", 260, estateGroups[5]),
            new Estate(spaces[27], "Coventry Street", 260, estateGroups[5]),

            new Utility(spaces[28], "Water Works", 150),
            new Estate(spaces[29], "Piccadilly", 280, estateGroups[5]),
            new Estate(spaces[31], "Regent Street", 300, estateGroups[6]),
            new Estate(spaces[32], "Oxford Street", 300, estateGroups[6]),
            new Estate(spaces[34], "Bond Street", 320, estateGroups[6]),

            new Railroad(spaces[35], "Liverpool Street Station", 200),
            new Estate(spaces[37], "Park Lane", 350, estateGroups[7]),
            new Estate(spaces[39], "Mayfair", 400, estateGroups[7])
        };

        setSpaceToPropertyReferences(properties);
        setEstateGroupToEstateReferences(estateGroups, properties);

        return properties;
    }

    private static void setSpaceToPropertyReferences(Property[] properties) {
        foreach (Property property in properties) {
            property.space.setProperty(property);
        }
    }
    private static void setEstateGroupToEstateReferences(EstateGroup[] estateGroups, Property[] properties) {
        estateGroups[0].setProperties(new Property[] { properties[0], properties[1] });
        estateGroups[1].setProperties(new Property[] { properties[3], properties[4], properties[5] });
        estateGroups[2].setProperties(new Property[] { properties[6], properties[8], properties[9] });
        estateGroups[3].setProperties(new Property[] { properties[11], properties[12], properties[13] });
        estateGroups[4].setProperties(new Property[] { properties[14], properties[15], properties[16] });
        estateGroups[5].setProperties(new Property[] { properties[18], properties[19], properties[21] });
        estateGroups[6].setProperties(new Property[] { properties[22], properties[23], properties[24] });
        estateGroups[7].setProperties(new Property[] { properties[26], properties[27] });
    }
}
