using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    public string url = "";
    public RawImage thisRenderer;

    [System.Obsolete]
    void Start()
    {
        StartCoroutine(DownloadImage(url)); 
        thisRenderer.material.color = Color.white;
    }

    [System.Obsolete]
    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            thisRenderer.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}