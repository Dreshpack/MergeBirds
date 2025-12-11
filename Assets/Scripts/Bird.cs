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

    public void SetBird(ItemInfo id)
    {
        birdId = id.number;
        icon.sprite = id.icon;
        icon.enabled = true;
        animator.SetInteger("birdId", birdId);
    }
}
