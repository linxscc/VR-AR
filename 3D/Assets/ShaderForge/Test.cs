using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    public Button uiButton;
    public Image uiImage;

    public void Click(GameObject go)
    {
        Debug.Log(go.name);

    }

    void OnGUI()
    {

        if (GUILayout.Button("Auto Button"))
        {
            ExecuteEvents.Execute<IPointerClickHandler>(uiButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            ExecuteEvents.Execute<ISubmitHandler>(uiButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }

        if (GUILayout.Button("Auto Image"))
        {
            ExecuteEvents.Execute<IPointerClickHandler>(uiImage.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }

    }
}
