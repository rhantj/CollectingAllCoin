using System;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationMap
{
    Move,
    isJump
}

public class PlayerAnimationId : MonoBehaviour
{
    private static Dictionary<AnimationMap, int> hashMap = new();

    private void Awake()
    {
        foreach (var am in Enum.GetValues(typeof(AnimationMap)))
        {
            if (!hashMap.ContainsKey((AnimationMap)am))
            {
                hashMap.Add((AnimationMap)am, Animator.StringToHash(am.ToString()));
            }
        }
    }

    public int GetAnimationHash(AnimationMap am) => hashMap[am];
}