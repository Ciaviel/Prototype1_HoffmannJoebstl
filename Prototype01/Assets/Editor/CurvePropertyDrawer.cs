using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(AnimationCurve))]
public class CurvePropertyDrawer : PropertyDrawer
{
    //Draw the property inside the given rect
    public override void OnGUI(Rect inRect, SerializedProperty inProperty, GUIContent inLabel)
    {
        var evt = Event.current;

        if (evt.type == EventType.MouseDown && evt.button == 0)
        {
            var mousePos = evt.mousePosition;

            if (inRect.Contains(mousePos))
            {
                inProperty.serializedObject.Update();

                CurveWindow curveEditor = CurveWindow.Init();
                curveEditor.CurveSerialProp = inProperty;
            }
        }
        else
        {
            inLabel = EditorGUI.BeginProperty(inRect, inLabel, inProperty);

            EditorGUI.BeginChangeCheck();
            AnimationCurve newCurve = EditorGUI.CurveField(inRect, inLabel, inProperty.animationCurveValue);

            if (EditorGUI.EndChangeCheck())
            {
                inProperty.animationCurveValue = newCurve;
            }

            EditorGUI.EndProperty();
        }
    }
}