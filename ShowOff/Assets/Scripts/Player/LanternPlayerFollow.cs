using System.Collections;
using UnityEngine;

public class LanternPlayerFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Vector3 offset;

    [Header("While colliding props")]
    [SerializeField]
    LanternCollDetector collDetector;

    private LanternFollowState lanternFollowState = LanternFollowState.Normal;

    void Start()
    {
        offset = transform.localPosition;
    }

    void Update()
    {
        if (!collDetector.IsTouchingWall && lanternFollowState == LanternFollowState.WallColl)
        {
            lanternFollowState = LanternFollowState.Normal;
        }
        else if (collDetector.IsTouchingWall && lanternFollowState == LanternFollowState.Normal)
        {
            lanternFollowState = LanternFollowState.WallColl;
        }
    }

    void LateUpdate()
    {
        switch (lanternFollowState)
        {
            case LanternFollowState.Normal:
                transform.position = collDetector.transform.position;
                break;
            case LanternFollowState.WallColl:
                transform.position = collDetector.transform.position + collDetector.pushVector;
                break;
            default:
                break;
        }

        transform.rotation = player.rotation;
    }
}

public enum LanternFollowState
{
    Normal,
    WallColl
}