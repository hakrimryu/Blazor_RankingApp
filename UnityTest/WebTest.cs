using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// データ
/// </summary>
public class GameResult
{
    public string UserName;
    public int Score;
}

/// <summary>
/// WebApi取得テスト
/// </summary>
public class WebTest : MonoBehaviour
{
    /// <summary>
    /// 基本URL
    /// </summary>
    private string _baseUrl = "https://localhost:44354/api";
    
    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // テストデータ作成
        GameResult res = new GameResult()
        {
            UserName = "RYURYU",
            Score = 999
        };

        // テストデータ転送
        this.SendPostRequst("ranking", res, (request =>
        {
            Debug.Log("Done Recv : " + request.downloadHandler.text);
        }));
        
        // サーバーのデータ全部取得
        this.SendGetAllRequst("ranking", (request =>
        {
            Debug.Log("Done Recv : " + request.downloadHandler.text);
        }));
    }

    /// <summary>
    /// テストデータ転送
    /// </summary>
    public void SendPostRequst(string url, object obj, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendWebRequest(url, "Post", obj, callback));
    }
    
    /// <summary>
    /// サーバーのデータ全部取得
    /// </summary>
    public void SendGetAllRequst(string url, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendWebRequest(url, "Get", null, callback));
    }
    
    /// <summary>
    /// サーバーとデータのやりとり
    /// </summary>
    IEnumerator CoSendWebRequest( string url, string method, object obj, Action<UnityWebRequest>  callback)
    {
        // url
        string senUrl = $"{this._baseUrl}/{url}";
        
        // データをJsonに、変換およびエンコード
        byte[] jsonBytes = null;
        if (obj != null)
        {
            string jsonStr = JsonUtility.ToJson(obj);
            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }

        // データ転送
        var uwr = new UnityWebRequest(senUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();
        
        // 成功・失敗を判断後、コールバック
        if (uwr.isNetworkError || uwr.isHttpError)
            Debug.Log(uwr.error);
        else
            callback?.Invoke(uwr);
    }
}
