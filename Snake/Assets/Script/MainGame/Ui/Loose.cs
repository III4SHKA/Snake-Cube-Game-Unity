using UnityEngine;
using TMPro;
public class Loose : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MainText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI sideText;

    void Start()
    {
        UpdateTexts();
    }
    void UpdateTexts()
    {
        if (difficultyText != null)
            difficultyText.text = My_Text.Difficult;

        if (sideText != null)
            sideText.text = My_Text.side.ToString();
    }
    public void LooseOrWin(string Event)
    {
        if (Event == "Loose")
        {
            MainText.color = Color.red;
        }
        MainText.text = Event;
    }
}