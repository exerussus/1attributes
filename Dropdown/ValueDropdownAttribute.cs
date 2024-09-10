using System;
using UnityEngine;

namespace Exerussus._1Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ValueDropdownAttribute : PropertyAttribute
    {
        public string MethodName { get; private set; }

        public ValueDropdownAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}