using UnityEngine;
using TMPro;
using System.Collections;

public class SaveInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI difficulty;
    [SerializeField] private TextMeshProUGUI maintext;
    [SerializeField] private GameObject canvasMain;
    [SerializeField] private GameObject canvasGame;
    private string previousDifficulty = "";
    [SerializeField] private GameObject snake;
    [SerializeField] private GameObject coin;

    void Update()
    {
        string currentDifficulty = difficulty.text;

        if (currentDifficulty != previousDifficulty)
        {
            previousDifficulty = currentDifficulty;

            My_Text.Difficult = currentDifficulty;

            // Знайти всі MazeCellGenerator на сцені
            MazeCellGenerator[] generators = FindObjectsOfType<MazeCellGenerator>();

            foreach (var generator in generators)
            {
                // Видалити стару сітку (опціонально, якщо потрібно очищення)
                foreach (Transform child in generator.transform)
                {
                    Destroy(child.gameObject);
                }

                generator.CreateGrid();
                generator.CreateMaze();
            }
        }
    }

    public void LoadGame()
    {
        if (string.IsNullOrEmpty(My_Text.Difficult) || My_Text.side == -1)
        {
            maintext.text = "U didn't type date";
            maintext.fontSize = 12;
            maintext.color = Color.red;
            return;
        }
        StartCoroutine(LoadGameWithDelay());
    }
    private IEnumerator LoadGameWithDelay()
    {
        yield return new WaitForSeconds(1.25f);

        snake.SetActive(true);
        coin.SetActive(true);
        canvasMain.SetActive(false);
        canvasGame.SetActive(true);
    }
}
