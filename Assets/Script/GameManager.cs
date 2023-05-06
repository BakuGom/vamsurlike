using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject CoverImage;
    private void Start()
    {

    }

    public void OnClickSignInButton()
    {

        CoverImage.SetActive(false);
        SceneManager.LoadScene("InGame");
    }
    public void OnClickSignUpButton()
    {

    }
}