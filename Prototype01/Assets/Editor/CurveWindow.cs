using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CurveWindow : EditorWindow
{
    [MenuItem("Window/Curve Editor")]
    public static CurveWindow Init()
    {
        return EditorWindow.GetWindow(typeof(CurveWindow)) as CurveWindow;
    }

    public SerializedProperty CurveSerialProp
    {
        get
        {
            return _curveSerialProp;
        }
        set
        {
            _curveSerialProp = value;
            _curve = value.animationCurveValue;
        }
    }
    private SerializedProperty _curveSerialProp;

    private AnimationCurve _curve = new AnimationCurve();

    void OnGUI()
    {
        bool isCurveChanged = false;

        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true), GUILayout.Height(50));
        EditorGUILayout.LabelField("Click _curve to edit ->", GUILayout.ExpandWidth(false));

        EditorGUI.BeginChangeCheck();
        _curve = EditorGUILayout.CurveField(_curve, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

        if (EditorGUI.EndChangeCheck())
        {
            isCurveChanged = true;
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

        EditorGUILayout.LabelField("Nr.", GUILayout.MinWidth(20.0f));
        EditorGUILayout.LabelField("X", GUILayout.MinWidth(50.0f));
        EditorGUILayout.LabelField("Y", GUILayout.MinWidth(50.0f));
        EditorGUILayout.LabelField("inTangent", GUILayout.MinWidth(50.0f));
        EditorGUILayout.LabelField("outTangent", GUILayout.MinWidth(50.0f));

        GUILayout.EndHorizontal();

        for (int i = 0; i < _curve.keys.Length; i++)
        {
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField(i + ".", GUILayout.MinWidth(20.0f));

            EditorGUI.BeginChangeCheck();
            float x = EditorGUILayout.FloatField(_curve.keys[i].time);
            float y = EditorGUILayout.FloatField(_curve.keys[i].value);
            //int mode = EditorGUILayout.IntField( curve.keys[i].tangentMode );

            float inTangent = EditorGUILayout.FloatField(_curve.keys[i].inTangent);
            float outTangent = EditorGUILayout.FloatField(_curve.keys[i].outTangent);

            if (EditorGUI.EndChangeCheck())
            {
                if ((i == 0 || (_curve.keys.Length > 0 && x != _curve.keys[0].time)) && (i == _curve.length - 1 || (x != _curve.keys[_curve.length - 1].time)))
                {

                    Keyframe keyframe = new Keyframe(x, y, inTangent, outTangent);
                    //keyframe.tangentMode = (int)mode;
                    _curve.MoveKey(i, keyframe);
                    isCurveChanged = true;
                }

            }

            GUILayout.EndHorizontal();
            EditorGUILayout.Separator();
        }

        if (isCurveChanged)
        {
            CurveSerialProp.animationCurveValue = _curve;
            CurveSerialProp.serializedObject.ApplyModifiedProperties();
            _curve = CurveSerialProp.animationCurveValue;
        }
    }
}