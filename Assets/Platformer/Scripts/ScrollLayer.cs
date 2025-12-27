using UnityEngine;

public class ScrollLayer : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float parallaxMultiplier = .5f;

    private float spriteWidth;
    private Vector3 lastCamPos;

    private Transform[] backgrounds = new Transform[2];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.childCount != 2)
        {
            Debug.LogError($"ParallaxScrollLayer only works with 2 backgroud children. Fix '{name}' GameObject");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }

        if (cam == null)
        {
            cam = Camera.main.transform;
        }
        lastCamPos = cam.position;

        spriteWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = cam.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier, .0f, .0f);
        lastCamPos = cam.position;

        foreach (Transform bg in backgrounds)
        {
            float camDistance = cam.position.x - bg.position.x;

            if (Mathf.Abs(camDistance) >= spriteWidth)
            {
                float offset = .0f;
                if (camDistance > 0.0f)
                {
                    offset = spriteWidth * 2.0f;
                }
                else
                {
                    offset = -spriteWidth * 2.0f;
                }

                bg.position += new Vector3(offset, .0f, .0f);
            }
        }
    }
}
