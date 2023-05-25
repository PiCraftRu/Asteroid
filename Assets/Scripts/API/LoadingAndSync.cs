using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// It's only for games.picraft.ru!
public class LoadingAndSync : MonoBehaviour
{
    private static string gamekey;
    private static Status.Root gameStatus;
    private Uri uri;
    [SerializeField] private TMPro.TextMeshProUGUI status;
    [SerializeField] private TMPro.TextMeshProUGUI id;
    [SerializeField] private int scene;
    

    public void Start()
    {
        if (!Uri.IsWellFormedUriString(Application.absoluteURL, UriKind.Absolute))
        {
            SceneManager.LoadScene(scene);
            return;
        }
        uri = new Uri(Application.absoluteURL);
        var query = HttpUtility.ParseQueryString(uri.Query);
        gamekey = query.Get("gamekey");
        StartCoroutine(Sync());
    }

    public IEnumerator Sync()
    {
        var ur = new UriBuilder(uri.Scheme, uri.Host, uri.Port, "/game/api/status");
        var request = UnityWebRequest.Get(ur.ToString() + "?gamekey=" + gamekey);
        yield return request.SendWebRequest();

        status.text = request.downloadHandler.text;
        var root = JsonUtility.FromJson<Status.Root>(status.text);
        status.text = "Hi, " + root.user.name;
        id.text = root.user.id;

        gameStatus = root;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(scene);
    }
}
