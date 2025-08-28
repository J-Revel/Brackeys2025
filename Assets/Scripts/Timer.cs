using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public float timer;
    private TMP_Text label;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        label = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0) return;
        timer -= Time.deltaTime;
        label.text = string.Format("Time: {0:00}:{1:00}",
                                    Mathf.FloorToInt(timer) / 60,
                                    Mathf.FloorToInt(timer) % 60 + 1);
        if (timer <= 0)
        {
            Destroy(GameObject.Find("Player"));
        }
    }
}
