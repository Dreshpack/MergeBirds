using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Animator animator;
    public int birdId;

    public void Animate()
    {
        animator.Play(birdId);
    }

    public void TurnOffImage()
    {
        icon.enabled = false;
    }

    public void SetBird(int id)
    {
        birdId = id;
        icon.enabled = true;
        animator.SetInteger("birdId", birdId);
    }
}
