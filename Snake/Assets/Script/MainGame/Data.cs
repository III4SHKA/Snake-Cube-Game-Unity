using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Data : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI side;
    [SerializeField] private TextMeshProUGUI difficult;
    void Start()
    {
        side.text = My_Text.side.ToString();
        difficult.text = My_Text.Difficult;
    }
}
