using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Card))]
public class CardPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float spriteWidth = 80f;
        float labelWidth = 30f;
        float idFieldWidth = 200f;
        float idFieldHeight = 18f;

        Rect spriteRect = new Rect(position.x, position.y, spriteWidth, spriteWidth);
        Rect idLabelRect = new Rect(position.x + spriteWidth + 10f, position.y + (spriteWidth - idFieldHeight) / 2, labelWidth, idFieldHeight);
        Rect idFieldRect = new Rect(position.x + spriteWidth + labelWidth + 15f, position.y + (spriteWidth - idFieldHeight) / 2, idFieldWidth, idFieldHeight);

        
        SerializedProperty spriteProperty = property.FindPropertyRelative("cardSprite");
        spriteProperty.objectReferenceValue = EditorGUI.ObjectField(spriteRect, GUIContent.none, spriteProperty.objectReferenceValue, typeof(Sprite), false);

        EditorGUI.LabelField(idLabelRect, "ID:");

        SerializedProperty idProperty = property.FindPropertyRelative("id");
        idProperty.intValue = EditorGUI.IntField(idFieldRect, idProperty.intValue);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 90f;
    }
}