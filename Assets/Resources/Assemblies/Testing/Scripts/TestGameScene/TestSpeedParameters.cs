using UnityEngine;

public class TestSpeedParameters : MonoBehaviour {
    [SerializeField] private float ACCELERATION_CONSTANT_ = 0.1f;
    [SerializeField] private float VELOCITY_CONSTANT_ = 0.5f;
    [SerializeField] private float MAX_VELOCITY_ = 60f;
    [SerializeField] private float DISTANCE_TO_SPACE_THRESHOLD_ = 5f;
    [SerializeField] private float DISTANCE_FOR_SETTLING_THRESHOLD_ = 0.2f;
    [SerializeField] private float VELOCITY_FOR_SETTLING_THRESHOLD_ = 0.2f;



    #region Singleton boilerplate
    public static TestSpeedParameters Instance { get; private set; }
    private void OnEnable() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy() {
        if (Instance == this) Instance = null;
    }
    #endregion



    # region Properties
    public float ACCELERATION_CONSTANT => ACCELERATION_CONSTANT_;
    public float VELOCITY_CONSTANT => VELOCITY_CONSTANT_;
    public float MAX_VELOCITY => MAX_VELOCITY_;
    public float DISTANCE_TO_SPACE_THRESHOLD => DISTANCE_TO_SPACE_THRESHOLD_;
    public float DISTANCE_FOR_SETTLING_THRESHOLD => DISTANCE_FOR_SETTLING_THRESHOLD_;
    public float VELOCITY_FOR_SETTLING_THRESHOLD => VELOCITY_FOR_SETTLING_THRESHOLD_;
    #endregion
}
