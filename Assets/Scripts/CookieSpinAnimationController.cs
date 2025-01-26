using UnityEngine;

public class CookieAnimationController : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("CollectableCookieSpinAnimation", 0, Random.Range(0f, 1f));
    }
}
