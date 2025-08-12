using UnityEngine;

public class TestVisualSpawnerManager : MonoBehaviour {
    [SerializeField] private RectTransform rt;
    [SerializeField] private ScriptableObject[] propertyGroups;
    [SerializeField] private ScriptableObject[] cards;


    #region MonoBehaviour
    private void Start() {
        
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) spawnDeed();
        else if (Input.GetKeyDown(KeyCode.Space)) AccompanyingVisualSpawner.Instance.removeObjectAndResetPosition();
    }
    #endregion



    #region private
    private void spawnDeed() {
        AccompanyingVisualSpawner accompanyingVisualSpawner = AccompanyingVisualSpawner.Instance;
        int indexOne = Random.Range(0, propertyGroups.Length);
        ScriptableObject propertyGroup = propertyGroups[indexOne];
        PropertyGroupInfo propertyGroupInfo = (PropertyGroupInfo)propertyGroup;
        int indexTwo = Random.Range(0, propertyGroupInfo.NumberOfPropertiesInGroup);
        PropertyInfo propertyInfo = propertyGroupInfo.getPropertyInfo(indexTwo);
        accompanyingVisualSpawner.spawnAndMove(rt, propertyInfo);
    }
    #endregion
}
