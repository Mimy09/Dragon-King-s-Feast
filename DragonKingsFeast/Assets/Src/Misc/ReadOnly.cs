#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

/// <summary>
/// 
/// Read Only Attribute. To be used when you want a variable to be public but not editable in inspector
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute { }

/// <summary>
/// 
/// Overrides the inspector so items can't be edited
/// 
/// <para>
/// Author: Mitchell Jenkins
/// </para>
/// 
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer {

    /// <summary>
    /// Overrides the height of the item
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label) {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    /// <summary>
    /// Overrides the GUI for the item to show up as non-editable in inspector
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label) {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

#else

using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute {

}

#endif