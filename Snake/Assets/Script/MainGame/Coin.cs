using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private Snake snake;
    private int score = 0;

    [System.Obsolete]
    private void Start()
    {
        snake = FindObjectOfType<Snake>();
        TeleportToRandomPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportToRandomPosition();
            score++;
            scoreText.text = score.ToString();
        }
    }

    private void TeleportToRandomPosition()
    {
        Vector3 newPos;
        int attempts = 0;
        float checkRadius = 0.1f; 
        do
        {
            float randomX = Mathf.Round(Random.Range(-2f, 2f) / 0.25f) * 0.25f;
            float randomY = Mathf.Round(Random.Range(-4.5f, -0.5f) / 0.25f) * 0.25f;
            newPos = new Vector3(randomX, randomY, transform.position.z);
            attempts++;
        }
        while (Physics.CheckSphere(newPos, checkRadius, LayerMask.GetMask("Wall")) && attempts < 100);

        if (attempts < 100)
        {
            transform.position = newPos;
        }
        else
        {
           
        }
    }
}
