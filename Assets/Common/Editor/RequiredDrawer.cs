using UnityEditor;
 using UnityEngine;
 
 [CustomPropertyDrawer(typeof(RequiredAttribute))]
 public class RequiredDrawer : PropertyDrawer {
     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
         Color c = GUI.backgroundColor;
         if (property.propertyType == SerializedPropertyType.ObjectReference) {
             GUI.backgroundColor = property.objectReferenceValue == null ? Color.red : Color.green;
         }
         if (property.propertyType == SerializedPropertyType.ExposedReference) {
             GUI.backgroundColor = property.exposedReferenceValue == null ? Color.red : Color.green;
         }
         EditorGUI.PropertyField(position, property);
         GUI.backgroundColor = c;
     }
 }