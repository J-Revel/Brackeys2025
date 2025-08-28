using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class Goal : MonoBehaviour
{
    public static int score;
    public TMP_Text label;
    public UnityEvent score_event;
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
        if (collision.GetComponent<Cookie>() != null)
        {
            score += (int) collision.attachedRigidbody.mass;
            label.text = "Score : " + score;
            score_event.Invoke();
            Destroy(collision.gameObject);
            //GameObject.Find("Cookie Detector").GetComponent<GrabbableDetector>().Detach();
            //StartCoroutine(ShrinkAndDestroyCoroutine(collision));
        }
    }

    void Update()
    {
        
    }
}
