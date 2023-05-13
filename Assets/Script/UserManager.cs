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
            Debug.Log("���̵� �������� �ʽ��ϴ�.");
            return;
        }
        Dictionary<string, object> documentData = documentSnapshot.ToDictionary();
        if (documentData.ContainsKey("password") && documentData["password"].ToString() == password)
        {
            Debug.Log("Signin ���� ���Ӿ����� ��ȯ");
            check = true;
        }
        else
        {
            Debug.Log("��й�ȣ�� Ʋ�Ƚ��ϴ�.");
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
            Debug.Log("���̵� �̹� �����մϴ�.");
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