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
    float lerpAmountClipping = 10.0f;

    float lerpAmount;

    //bool isAlreadyWaiting = false;

    private LanternFollowState lanternFollowState = LanternFollowState.Normal;

    void Start()
    {
        offset = transform.localPosition;
    }

    void Update()
    {
        if (!collDetector.IsTouchingWall && lanternFollowState == LanternFollowState.WallColl
            /*&& !isAlreadyWaiting*/)
        {
            StartCoroutine(WaitBeforeSwitchingToNormal());
        }
        else if (collDetector.IsTouchingWall && lanternFollowState == LanternFollowState.Normal)
        {
            lanternFollowState = LanternFollowState.WallColl;
        }
    }

    IEnumerator WaitBeforeSwitchingToNormal()
    {
        //isAlreadyWaiting = true;

        yield return new WaitForSeconds(0.4f);

        lanternFollowState = LanternFollowState.Normal;
        //isAlreadyWaiting = false;
    }

    void LateUpdate()
    {
        Vector3 targetPos = transform.position;

        switch (lanternFollowState)
        {
            case LanternFollowState.Normal:
                targetPos = player.position + player.rotation * offset;
                transform.position = targetPos;
                break;
            case LanternFollowState.WallColl:
                targetPos = collDetector.transform.position + collDetector.pushVector;
                lerpAmount = lerpAmountClipping;
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpAmount);
                break;
            default:
                break;
        }

        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpAmount);
        transform.rotation = player.rotation;
    }
}

public enum LanternFollowState
{
    Normal,
    WallColl
}