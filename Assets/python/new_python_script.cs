using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;

public class new_Python_script : MonoBehaviour
{
   [MenuItem("Python Scripts/New Python Script")]
   public static void NewPythonScript()
   {
       PythonRunner.RunFile("Assets/python/new_python_script.py");
   }

    public void Start()
    {
        PythonRunner.RunFile("Assets/python/new_python_script.py");
    }
};
