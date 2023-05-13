using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public GameObject CoverImage;
    private void Awake()
    {
    }

    public async void OnClickSignInButton()
    {
        await Task.Delay(500);
        if (UserManager.check==true)
        {
            CoverImage.SetActive(false);
            SceneManager.LoadScene("InGame");
        }
    }
    public void OnClickSignUpButton()
    {

    }
}