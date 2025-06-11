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
    [SerializeField]
    float lerpAmount = 10.0f;

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
                transform.position = player.position + player.rotation * offset;
                break;
            case LanternFollowState.WallColl:
                Vector3 targetPos = collDetector.transform.position + collDetector.pushVector;
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpAmount);
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