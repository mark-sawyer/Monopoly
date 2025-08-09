using System.Linq;

public class EstateGroupIconState : PropertyGroupIconState {
    private enum EstateStatus {
        UNOWNED,
        OWNED,
        MORTGAGED,
        HOUSE_1,
        HOUSE_2,
        HOUSE_3,
        HOUSE_4,
        HOTEL
    }
    private EstateStatus[] estateStatuses;



    #region public
    public override bool NoOwnership => estateStatuses.All(x => x == EstateStatus.UNOWNED);
    public EstateGroupIconState(EstateGroupInfo estateGroupInfo, PlayerInfo owner) {
        int estatesInGroup = estateGroupInfo.NumberOfPropertiesInGroup;
        estateStatuses = new EstateStatus[estatesInGroup];
        for (int i = 0; i < estatesInGroup; i++) {
            EstateInfo estateInfo = estateGroupInfo.getEstateInfo(i);
            estateStatuses[i] = getEstateStatus(estateInfo, owner);
        }
    }
    public EstateGroupIconState(EstateGroupInfo estateGroupInfo) {
        int estatesInGroup = estateGroupInfo.NumberOfPropertiesInGroup;
        estateStatuses = new EstateStatus[estatesInGroup];
        for (int i = 0; i < estatesInGroup; i++) {
            estateStatuses[i] = EstateStatus.UNOWNED;
        }
    }
    public bool stateHasChanged(EstateGroupIconState newState) {
        bool differenceFound = false;
        for (int i = 0; i < estateStatuses.Length; i++) {
            EstateStatus oldStatus = estateStatuses[i];
            EstateStatus newStatus = newState.estateStatuses[i];
            if (oldStatus != newStatus) {
                differenceFound = true;
                break;
            }
        }
        return differenceFound;
    }
    #endregion



    #region private
    private EstateStatus getEstateStatus(EstateInfo estateInfo, PlayerInfo owner) {
        if (estateInfo.Owner != owner) return EstateStatus.UNOWNED;
        if (estateInfo.IsMortgaged) return EstateStatus.MORTGAGED;
        int buildings = estateInfo.BuildingCount;
        if (buildings == 0) return EstateStatus.OWNED;
        if (estateInfo.HasHotel) return EstateStatus.HOTEL;
        else {
            if (buildings == 1) return EstateStatus.HOUSE_1;
            if (buildings == 2) return EstateStatus.HOUSE_2;
            if (buildings == 3) return EstateStatus.HOUSE_3;
            return EstateStatus.HOUSE_4;
        }
    }
    #endregion
}
