using Battleground;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorController _animation;
    [SerializeField] private Timeline timeline;

    private void Update()
    {
        //var progress = (timeline.GetTime - timeline.MinTime) * 2 / (timeline.MaxTime - timeline.MinTime);
        //if (progress < 1)
        //    animator.Play("SizeChange");
        //if (progress > 1)
        //    animator.Play("Grow");
        //animator.SetFloat("Grow", progress / 2);
    }

}
