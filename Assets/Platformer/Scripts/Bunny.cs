using System.Timers;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] Animator animator;

    [Header("SFX")]
    [SerializeField] private AudioClip enemySoundClip;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundFXManager.instance.PlaySoundFXClip(enemySoundClip, transform, 1.0f);
            ScoreTracker.instance.AddBunnyScore();

            Destroy(gameObject);
        }
    }
}
