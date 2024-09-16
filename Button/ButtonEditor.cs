
#if UNITY_EDITOR

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Exerussus._1Attributes
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var targetType = target.GetType();
            var methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        
            foreach (var method in methods)
            {
                var buttonAttribute = (ButtonAttribute)System.Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
                if (buttonAttribute != null)
                {
                    var buttonLabel = buttonAttribute.ButtonLabel ?? method.Name;
                    if (GUILayout.Button(buttonLabel)) method.Invoke(target, null);
                }
            }
        }
    }
}

#endif