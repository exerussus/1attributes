
#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

namespace Exerussus._1Attributes
{
    public class ExampleAttributesWork : MonoBehaviour
    {
        [SerializeField, ValueDropdown("Options")] private string dropDownValue;
        [SerializeField, ReadOnly] private string readOnlyValue = "this is readOnlyValue";
        
        public static IEnumerable<string> Options() => new[] {"Option 1", "Option 2", "Option 3"};
        
        [Button]
        public void Show()
        {
            Debug.Log(dropDownValue);
            Debug.Log(readOnlyValue);
        }
    }
}

#endif