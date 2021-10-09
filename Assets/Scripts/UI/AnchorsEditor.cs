#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

    public class AnchorsEditor
    {
        [MenuItem("Nyx/UI/Anchor Around Objects")]
        public static void uGUIAnchorAroundObject()
        {
            var o = Selection.objects;
            if (o != null && o.Length > 0)
            {
                foreach(GameObject obj in o)
                {
                    var r = obj.GetComponent<RectTransform>();
                    if (r != null)
                    {
                        var p = obj.transform.parent.GetComponent<RectTransform>();

                        var offsetMin = r.offsetMin;
                        var offsetMax = r.offsetMax;
                        var _anchorMin = r.anchorMin;
                        var _anchorMax = r.anchorMax;

                        var parent_width = p.rect.width;
                        var parent_height = p.rect.height;

                        var anchorMin = new Vector2(_anchorMin.x + (offsetMin.x / parent_width),
                                                    _anchorMin.y + (offsetMin.y / parent_height));
                        var anchorMax = new Vector2(_anchorMax.x + (offsetMax.x / parent_width),
                                                    _anchorMax.y + (offsetMax.y / parent_height));

                        r.anchorMin = anchorMin;
                        r.anchorMax = anchorMax;

                        r.offsetMin = new Vector2(0, 0);
                        r.offsetMax = new Vector2(0, 0);
                        r.pivot = new Vector2(0.5f, 0.5f);
                    }
                }
            }
        }

        public static void uGUIAnchorObject(GameObject a_go)
        {
            var r = a_go.GetComponent<RectTransform>();
            if (r != null)
            {
                var p = a_go.transform.parent.GetComponent<RectTransform>();

                var offsetMin = r.offsetMin;
                var offsetMax = r.offsetMax;
                var _anchorMin = r.anchorMin;
                var _anchorMax = r.anchorMax;

                var parent_width = p.rect.width;
                var parent_height = p.rect.height;

                var anchorMin = new Vector2(_anchorMin.x + (offsetMin.x / parent_width),
                                            _anchorMin.y + (offsetMin.y / parent_height));
                var anchorMax = new Vector2(_anchorMax.x + (offsetMax.x / parent_width),
                                            _anchorMax.y + (offsetMax.y / parent_height));

                r.anchorMin = anchorMin;
                r.anchorMax = anchorMax;

                r.offsetMin = new Vector2(0, 0);
                r.offsetMax = new Vector2(0, 0);
                r.pivot = new Vector2(0.5f, 0.5f);
            }
        }
    } // Class
#endif