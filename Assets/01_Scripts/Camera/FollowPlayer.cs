using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset = new(0, 9.13f, -6.08f);
    Vector3 newlocation;

    private void LateUpdate()
    {
        FollowingPlayer();
    }

    void FollowingPlayer()
    {
        newlocation = player.position + offset;
        transform.position = newlocation;
    }
}
