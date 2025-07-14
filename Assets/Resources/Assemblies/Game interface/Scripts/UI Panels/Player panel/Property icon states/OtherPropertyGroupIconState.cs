
public class OtherPropertyGroupIconState : PropertyGroupIconState {
    private enum PropertyStatus {
        UNOWNED,
        OWNED,
        MORTGAGED
    }
    private PropertyStatus[] groupStatuses;



    #region public
    public OtherPropertyGroupIconState(PropertyInfo[] propertyInfos, PlayerInfo owner) {
        int propertiesInGroup = propertyInfos.Length;
        groupStatuses = new PropertyStatus[propertiesInGroup];
        for (int i = 0; i < propertiesInGroup; i++) {
            PropertyInfo propertyInfo = propertyInfos[i];
            groupStatuses[i] = getPropertyStatus(propertyInfo, owner);
        }
    }
    public OtherPropertyGroupIconState(PropertyInfo[] propertyInfos) {
        int propertiesInGroup = propertyInfos.Length;
        groupStatuses = new PropertyStatus[propertiesInGroup];
        for (int i = 0; i < propertiesInGroup; i++) {
            groupStatuses[i] = PropertyStatus.UNOWNED;
        }
    }
    public bool stateHasChanged(OtherPropertyGroupIconState newState) {
        bool differenceFound = false;
        for (int i = 0; i < groupStatuses.Length; i++) {
            PropertyStatus oldStatus = groupStatuses[i];
            PropertyStatus newStatus = newState.groupStatuses[i];
            if (oldStatus != newStatus) {
                differenceFound = true;
                break;
            }
        }
        return differenceFound;
    }
    #endregion



    #region private
    private PropertyStatus getPropertyStatus(PropertyInfo propertyInfo, PlayerInfo owner) {
        if (propertyInfo.Owner != owner) return PropertyStatus.UNOWNED;
        else if (propertyInfo.IsMortgaged) return PropertyStatus.MORTGAGED;
        else return PropertyStatus.OWNED;
    }
    #endregion
}
