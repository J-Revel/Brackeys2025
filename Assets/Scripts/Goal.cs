using UnityEngine;
public class Goal : MonoBehaviour
{
    public static int score;
    void Start()
    {
        score = 0;
    }

    private System.Collections.IEnumerator ShrinkAndDestroyCoroutine(Collider2D collision)
    {
        Vector3 originalScale = collision.attachedRigidbody.transform.localScale;
        float timer = 0f;

        while (transform.localScale.x > 0.1)
        {
            timer += Time.deltaTime;
            float progress = timer;

            // Scale down smoothly
            collision.attachedRigidbody.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);
            yield return null;
        }

        Destroy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name[..6] == "Cookie" && collision.gameObject.name.Length < 15)
        {
            score += (int) collision.attachedRigidbody.mass;
            GameObject.Find("Cookie Detector").GetComponent<GrabbableDetector>().Detach();
            StartCoroutine(ShrinkAndDestroyCoroutine(collision));
        }
    }

    void Update()
    {
        
    }
}
