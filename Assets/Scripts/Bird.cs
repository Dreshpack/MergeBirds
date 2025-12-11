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

        // Set animator state FIRST before enabling icon
        animator.SetInteger("birdId", birdId);

        // Force animator to update immediately to prevent showing wrong sprite
        animator.Update(0f);

        // Now enable the icon with correct animation state
        icon.enabled = true;
    }
}
