using UnityEngine;

public class Pause : MonoBehaviour
{
    private Snake snake;
    private float originalMoveRate;

    private bool isPaused = false;

    void Start()
    {
        snake = FindObjectOfType<Snake>();
        if (snake != null)
        {
            originalMoveRate = snake.moveRate;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (snake == null) return;

        if (!isPaused)
        {
            snake.moveRate = 500f;
            Time.timeScale = 0f;
        }
        else
        {
            snake.moveRate = originalMoveRate;
            Time.timeScale = 1f;
        }

        isPaused = !isPaused;
    }
}
