using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyGroupIconSection : MonoBehaviour {
    [SerializeField] private PropertyGroupIcon[] propertyGroupIcons;
    private Dictionary<int, int> estateGroupToPropertyGroupIcon;
    private const int RAILROAD_INDEX = 4;
    private const int UTILITY_INDEX = 9;



    public void setup(PlayerInfo playerInfo) {
        foreach (PropertyGroupIcon propertyGroupIcon in propertyGroupIcons) {
            propertyGroupIcon.setup(playerInfo);
        }
        estateGroupToPropertyGroupIcon = new Dictionary<int, int>() {
            { 0, 0 },
            { 1, 1 },
            { 2, 2 },
            { 3, 3 },
            { 4, 5 },
            { 5, 6 },
            { 6, 7 },
            { 7, 8 },
        };
    }
    public IEnumerator updatePropertyIconVisual(PlayerInfo playerInfo, PropertyInfo propertyInfo) {
        PropertyGroupIcon getPropertyGroupIcon() {
            if (propertyInfo is EstateInfo estateInfo) {
                int groupID = (int)estateInfo.EstateColour;
                int propertyGroupIndex = estateGroupToPropertyGroupIcon[groupID];
                return propertyGroupIcons[propertyGroupIndex];
            }
            else if (propertyInfo is RailroadInfo) return propertyGroupIcons[RAILROAD_INDEX];
            else return propertyGroupIcons[UTILITY_INDEX];
        }

        PropertyGroupIcon propertyGroupIcon = getPropertyGroupIcon();
        SoundPlayer.Instance.play_Pop();
        yield return propertyGroupIcon.pulseAndUpdate();
    }
    public List<PropertyGroupIcon> propertyGroupIconsNeedingAnUpdate() {
        List<PropertyGroupIcon> needsUpdate = new();
        foreach (PropertyGroupIcon propertyGroupIcon in propertyGroupIcons) {
            if (propertyGroupIcon.NeedsToUpdate) {
                needsUpdate.Add(propertyGroupIcon);
            }
        }
        return needsUpdate;
    }
    public PropertyGroupIcon getIcon(int i) {
        return propertyGroupIcons[i];
    }
}
