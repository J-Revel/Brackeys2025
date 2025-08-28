using UnityEngine;
using TMPro;
public class Goal : MonoBehaviour
{
    public static int score;
    private TMP_Text label;
    void Start()
    {
        score = 0;
        label = GetComponentInChildren<TMP_Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name[..6] == "Cookie" && collision.gameObject.name.Length < 15)
        {
            score += (int) collision.attachedRigidbody.mass;
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        label.text = "Score: " + score;
    }
}
