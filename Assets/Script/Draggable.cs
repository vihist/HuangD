using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Draggable : MonoBehaviour, IDragHandler
{
    private Vector2 Min;
    private Vector2 Max;

    void Start()
    {
        Min = new Vector2(transform.GetComponent<RectTransform>().rect.width/2, transform.GetComponent<RectTransform>().rect.height/2);
        Max = new Vector2(transform.parent.GetComponent<RectTransform>().rect.width - Min.x, transform.parent.GetComponent<RectTransform>().rect.height - Min.y);
    }
        

    public void OnDrag (PointerEventData eventData)
    {
        Vector3 vector =  (Vector3)eventData.position;

        if (vector.x < Min.x)
        {
            vector.x = Min.x;
        }
        if (vector.y < Min.y)
        {
            vector.y = Min.y;
        }
        if (vector.x > Max.x)
        {
            vector.x = Max.x;
        }
        if (vector.y > Max.y)
        {
            vector.y = Max.y;
        }

        transform.position = vector;

    }
}
