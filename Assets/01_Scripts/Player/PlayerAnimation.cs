using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    PlayerContext plc;
    PlayerAnimationId animID;
    PlayerMovement movement;

    private void Awake()
    {
        plc = GetComponent<PlayerContext>();
        animID = GetComponent<PlayerAnimationId>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        plc.Anim.SetFloat(animID.GetAnimationHash(AnimationMap.Move), 
                          Mathf.Abs(movement.MoveInput.magnitude));

        plc.Anim.SetBool(animID.GetAnimationHash(AnimationMap.isJump),
                         !movement.IsGrounded);
    }
}
