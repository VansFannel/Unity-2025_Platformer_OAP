using System.Timers;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] Animator animator;

    private bool isJumping = false;
    private float direction = 1.0f;

    void Update()
    {
        if (isJumping)
        {
            transform.position += direction * speed * Time.deltaTime * new Vector3(1.0f, .0f, .0f);
        }
    }

    public void StartJumping()
    {
        SetJumping(true);
    }

    public void StopJumping()
    {
        SetJumping(false);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        direction = -direction;
    }

    private void SetJumping(bool jumping)
    {
        isJumping = jumping;
    }
}
