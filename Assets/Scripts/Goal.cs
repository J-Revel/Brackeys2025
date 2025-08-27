using UnityEngine;

public class Goal : MonoBehaviour
{
    public static int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name[..6] == "Cookie" && collision.gameObject.name.Length < 15)
        {
            Destroy(collision.gameObject);
            score += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
