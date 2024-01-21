using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    private Slime slime => GetComponentInParent<Slime>();

    private void AnimationTrigger()
    {
        slime.AnimationTrigger();
    }
}
