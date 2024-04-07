using UnityEngine;
using UnityEngine.UI;

public class quitMenu : MonoBehaviour
{

    public static bool IsActive;
    public Button quitButton;
    public Button exitButton;
    public GameObject gameOBJ;
    
    
    void Start()
    {
        quitButton.onClick.AddListener(Quit);
        exitButton.onClick.AddListener(CloseQuitMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsActive) // if it's true
            {
                CloseQuitMenu();
                IsActive = false;
            }
            else // if it's false
            {
                OpenQuitMenu();
                IsActive = true;
            }
        }
    }

    public static void Quit()
    {
        Application.Quit();
    }
    

    public void OpenQuitMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOBJ.SetActive(true);
        GameUtils.Pause();
    }

    public void CloseQuitMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameOBJ.SetActive(false);
        GameUtils.UnPause();
    }

}
