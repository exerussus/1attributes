
#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Exerussus._1Attributes
{
    [CustomPropertyDrawer(typeof(ValueDropdownAttribute))]
    public class ValueDropdownDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Получаем атрибут
            ValueDropdownAttribute dropdownAttribute = (ValueDropdownAttribute)attribute;
            // Получаем целевой объект (объект, где объявлено поле)
            object targetObject = property.serializedObject.targetObject;

            // Находим метод, указанный в атрибуте
            MethodInfo method = targetObject.GetType().GetMethod(dropdownAttribute.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (method != null)
            {
                // Вызываем метод и получаем значения
                object result = method.Invoke(targetObject, null);

                if (result is IEnumerable)
                {
                    List<GUIContent> displayList = new List<GUIContent>();
                    List<object> valueList = new List<object>();

                    foreach (var item in (IEnumerable)result)
                    {
                        displayList.Add(new GUIContent(item.ToString()));
                        valueList.Add(item);
                    }

                    // Проверяем, что текущее значение есть в списке, иначе используем индекс по умолчанию
                    int selectedIndex = valueList.IndexOf(property.propertyType == SerializedPropertyType.String ? property.stringValue : (object)property.intValue);
            
                    if (selectedIndex == -1) 
                    {
                        selectedIndex = 0;  // Если не найдено, устанавливаем значение по умолчанию
                    }

                    // Отрисовка выпадающего списка
                    selectedIndex = EditorGUI.Popup(position, label, selectedIndex, displayList.ToArray());

                    // Обновляем значение в зависимости от типа свойства
                    if (property.propertyType == SerializedPropertyType.String)
                    {
                        property.stringValue = valueList[selectedIndex]?.ToString();
                    }
                    else if (property.propertyType == SerializedPropertyType.Integer)
                    {
                        property.intValue = Convert.ToInt32(valueList[selectedIndex]);
                    }
                }
                else
                {
                    EditorGUI.LabelField(position, label.text, "Method must return IEnumerable");
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Method not found");
            }
        }

    }
}

#endif