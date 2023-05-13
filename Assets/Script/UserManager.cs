using Firebase.Firestore;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    private FirebaseFirestore db;
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;
    public static bool check = false;
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }
    public async void CheckUserSigninInfo()
    {
        string id = idInputField.text;
        string password = passwordInputField.text;
        CollectionReference PlaylistRef = db.Collection("Playlist");
        DocumentSnapshot documentSnapshot = await PlaylistRef.Document(id).GetSnapshotAsync();
        if (!documentSnapshot.Exists)
        {
            Debug.Log("아이디가 존재하지 않습니다.");
            return;
        }
        Dictionary<string, object> documentData = documentSnapshot.ToDictionary();
        if (documentData.ContainsKey("password") && documentData["password"].ToString() == password)
        {
            Debug.Log("Signin 성공 게임씬으로 전환");
            check = true;
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다.");
            return;
        }
    }
    public async void AddNewUserToPlaylist()
    {
        string id = idInputField.text;
        string password = passwordInputField.text;
        CollectionReference PlaylistRef = db.Collection("Playlist");
        DocumentSnapshot documentSnapshot = await PlaylistRef.Document(id).GetSnapshotAsync();
        if (documentSnapshot.Exists)
        {
            Debug.Log("아이디가 이미 존재합니다.");
            return;
        }
        DocumentReference documentRef = PlaylistRef.Document(id);
        await documentRef.SetAsync(new Dictionary<string, object>
    {
        {"id", id},
        {"password", password}
    });
        idInputField.text = "";
        passwordInputField.text = "";
    }
}