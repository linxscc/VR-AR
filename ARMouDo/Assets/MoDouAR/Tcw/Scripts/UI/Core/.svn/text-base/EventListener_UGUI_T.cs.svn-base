using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public delegate void UGUIEvent_Hander_T(GameObject go);
/// <summary>
/// UI监听器
/// </summary>
public class EventListener_UGUI_T : EventTrigger
{
    /// <summary>
    /// 当点击时
    /// </summary>
    public UGUIEvent_Hander_T onClick;
    /// <summary>
    /// 当按下时
    /// </summary>
    public UGUIEvent_Hander_T onDown;
    /// <summary>
    /// 当悬浮时
    /// </summary>
    public UGUIEvent_Hander_T onEnter;
    /// <summary>
    /// 当离开时
    /// </summary>
    public UGUIEvent_Hander_T onExit;
    /// <summary>
    /// 当松开时
    /// </summary>
    public UGUIEvent_Hander_T onUp;
    /// <summary>
    /// 当选中时,触发一次 [仅非选中状态有效]
    /// </summary>
    public UGUIEvent_Hander_T onSelect;
    /// <summary>
    /// 当选中时,每帧触发 [仅非选中状态有效]
    /// </summary>
    public UGUIEvent_Hander_T onUpdateSelect;
    /// <summary>
    /// 当开始拖拽
    /// </summary>
    public UGUIEvent_Hander_T onBeginDrag;
    /// <summary>
    /// 当结束拖拽
    /// </summary>
    public UGUIEvent_Hander_T onEndDrag;
    /// <summary>
    /// 当拖拽中
    /// </summary>
    public UGUIEvent_Hander_T onDrag;

    /// <summary>
    /// 注册UI事件
    /// </summary>
    /// <param name="go">注册对象</param>
    /// <returns></returns>
    static public EventListener_UGUI_T LoadEvent(GameObject go)
    {
        EventListener_UGUI_T listener = go.GetComponent<EventListener_UGUI_T>();
        if (listener == null) listener = go.AddComponent<EventListener_UGUI_T>();
        return listener;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null) onBeginDrag(gameObject);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null) onEndDrag(gameObject);
    }
    public override void OnDrag(PointerEventData data)
    {
        if (onDrag != null) onDrag(gameObject);
    }
}



