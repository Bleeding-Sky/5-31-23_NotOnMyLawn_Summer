using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private UITrapItem item;
    // Start is called before the first frame update
    public void Awake()
    {
        canvas.transform.root.GetComponent<Canvas>();
        mainCamera = Camera.main;
        item = GetComponentInChildren<UITrapItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }
    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
            );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
