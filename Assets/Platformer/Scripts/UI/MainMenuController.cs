using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void ShowMenu()
    {
        animator.Play("TitleMainAnimation", 0, 0);
    }

    public void HideMenu()
    {
        animator.SetTrigger("OutOfScreenTrigger");
    }

    void OnMouseDown()
    {
        animator.SetTrigger("OutOfScreenTrigger");
    }
}
