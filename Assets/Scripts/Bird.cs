using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Animator animator;
    public int birdId;

    private void Start()
    {
        // Ensure icon is disabled by default to prevent flash on initialization
        if (icon != null)
        {
            icon.enabled = false;
        }
    }

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
