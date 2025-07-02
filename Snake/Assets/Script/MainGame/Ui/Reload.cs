using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public void ReloadClick()
    {
        SceneManager.LoadScene(0);
        My_Text.side = -1;
    }
}
