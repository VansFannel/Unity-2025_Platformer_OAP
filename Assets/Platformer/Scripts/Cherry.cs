using UnityEngine;

public class Cherry : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip cherrySoundClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundFXManager.instance.PlaySoundFXClip(cherrySoundClip, transform, 1.0f);
            ScoreTracker.instance.AddCherryScore();

            Destroy(gameObject);
        }
    }
}
