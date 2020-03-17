using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Auto_Move_UI_Anchors : MonoBehaviour
{
    private RectTransform t;
    private RectTransform pt;

    void Start()
    {
        t = gameObject.GetComponent<RectTransform>();
        pt = gameObject.GetComponent<RectTransform>().parent as RectTransform;

        if (t == null || pt == null) return;

        Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                                         t.anchorMin.y + t.offsetMin.y / pt.rect.height);
        Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                                         t.anchorMax.y + t.offsetMax.y / pt.rect.height);

        t.anchorMin = newAnchorsMin;
        t.anchorMax = newAnchorsMax;
        t.offsetMin = t.offsetMax = new Vector2(0, 0);

        Auto_Move_UI_Anchors _script_AUTOanchor = gameObject.GetComponent<Auto_Move_UI_Anchors>();
        DestroyImmediate(_script_AUTOanchor);//sterge componentul automat
    }
}
