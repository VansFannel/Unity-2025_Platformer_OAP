using UnityEngine;

public class Cherry : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip cherrySoundClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
