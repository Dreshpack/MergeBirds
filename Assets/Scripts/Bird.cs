using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    [SerializeField] private Image icon;
    //[SerializeField] private Animator animator;
    public int birdId;

    private void Start()
    {
        // Ensure icon and animator are disabled by default to prevent flash on initialization
        if (icon != null)
        {
            icon.enabled = false;
        }

        //if (animator != null)
      //  {
         //   animator.enabled = false;
        //}
    }

    public void Animate()
    {
       // animator.Play(birdId);
    }

    public void TurnOffImage()
    {
        icon.sprite = null;
        icon.enabled = false;

        // Disable animator when no bird to prevent unwanted state changes
       // if (animator != null)
      //  {
       //     animator.enabled = false;
       // }
    }

    public void SetBird(ItemInfo id)
    {
        birdId = id.number;
        icon.sprite = id.icon;

        // Disable animator first to reset state and prevent flash of old animation
       // if (animator != null)
       // {
       //     animator.enabled = false;
      //  }

        // Disable icon while we update animator state
        icon.enabled = false;

        // Re-enable animator with fresh state
     //   if (animator != null)
    //    {
     //       animator.enabled = true;
     //   }

        // Set animator state
        //animator.SetInteger("birdId", birdId);

        // Force animator to update immediately to prevent showing wrong sprite
        //animator.Update(0f);

        // Now enable the icon with correct animation state
        icon.enabled = true;
    }
}
