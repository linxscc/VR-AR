using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class _WWWProxy {

	public static List<_WWWProxy> wwwProxies = new List<_WWWProxy>();
	public static bool IsDone{get{ return wwwProxies.Count == 0;}}

	public string url{set; get;}
	public Action<WWW> feedback{set; get;}

	public _WWWProxy(string _url, Action<WWW> _action){
		wwwProxies.Add (this);

		url = _url;
		feedback = _action;
	}
	
	public IEnumerator WaitForFinished(){
		
		WWW www = new WWW(url);//WWW.LoadFromCacheOrDownload(url, 0);//
        yield return www;
		
		if (www.error == null)
		{

			if(wwwProxies.Remove(this)){
               // Debug.Log(url + " WWW is removed!\n The Count = " + wwwProxies.Count);
			}

			if(feedback != null){
				feedback(www);

			}
            
			if(www.assetBundle != null)
				www.assetBundle.Unload(false);

			// Frees the memory from the web stream
			www.Dispose();
			www = null;
		}
		else
		{
            Debug.Log ( www.error );
			if(wwwProxies.Remove(this)){
				Debug.Log (url + " WWW is ERROR!\n The Count = " + wwwProxies.Count);
			}
			yield return null;
		}
	}
}
