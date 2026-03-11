using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Generic;

[InitializeOnLoad]
public class AIBridgeServer
{
    private static HttpListener listener;
    private static Thread serverThread;
    private static bool running;

    const string PREFIX = "http://localhost:8090/";

    static AIBridgeServer()
    {
        StartServer();
        AssemblyReloadEvents.beforeAssemblyReload += StopServer;
        EditorApplication.quitting += StopServer;
    }

    static void StartServer()
    {
        if (running) return;

        try
        {
            listener = new HttpListener();
            listener.Prefixes.Add(PREFIX);
            listener.Start();

            running = true;

            serverThread = new Thread(Listen);
            serverThread.IsBackground = true;
            serverThread.Start();

            Debug.Log("AI Bridge Server running at " + PREFIX);
        }
        catch (Exception e)
        {
            Debug.LogWarning("AI Bridge failed to start: " + e.Message);
        }
    }

    static void StopServer()
    {
        running = false;

        try
        {
            listener?.Stop();
            listener?.Close();
        }
        catch {}

        listener = null;
    }

    static void Listen()
    {
        while (running)
        {
            try
            {
                var context = listener.GetContext();

                string body = "";

                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    body = reader.ReadToEnd();
                }

                BridgeCommand command = null;

                try
                {
                    command = JsonUtility.FromJson<BridgeCommand>(body);
                }
                catch
                {
                    Debug.LogWarning("Invalid JSON command");
                }

                if (command != null)
                {
                    EditorApplication.delayCall += () => Execute(command);
                }

                byte[] buffer = Encoding.UTF8.GetBytes("ok");

                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.Close();
            }
            catch
            {
                // Happens during shutdown
            }
        }
    }

    static void Execute(BridgeCommand cmd)
    {
        switch (cmd.action)
        {
            case "createPrimitive":
                CreatePrimitive(cmd);
                break;

            case "addComponent":
                AddComponent(cmd);
                break;

            case "listScene":
                ListScene();
                break;

            case "delete":
                DeleteObject(cmd);
                break;

            default:
                Debug.LogWarning("Unknown AI command: " + cmd.action);
                break;
        }
    }

    static void CreatePrimitive(BridgeCommand cmd)
    {
        PrimitiveType type = PrimitiveType.Cube;

        if (!string.IsNullOrEmpty(cmd.type))
        {
            Enum.TryParse(cmd.type, true, out type);
        }

        GameObject obj = GameObject.CreatePrimitive(type);

        if (!string.IsNullOrEmpty(cmd.name))
            obj.name = cmd.name;

        Debug.Log("AI created object: " + obj.name);
    }

    static void AddComponent(BridgeCommand cmd)
    {
        if (string.IsNullOrEmpty(cmd.objectName) || string.IsNullOrEmpty(cmd.component))
            return;

        GameObject obj = GameObject.Find(cmd.objectName);

        if (obj == null)
        {
            Debug.LogWarning("Object not found: " + cmd.objectName);
            return;
        }

        Type type = Type.GetType(cmd.component);

        if (type == null)
        {
            Debug.LogWarning("Component type not found: " + cmd.component);
            return;
        }

        obj.AddComponent(type);

        Debug.Log("AI added component " + cmd.component + " to " + obj.name);
    }

    static void DeleteObject(BridgeCommand cmd)
    {
        GameObject obj = GameObject.Find(cmd.objectName);

        if (obj != null)
        {
            GameObject.DestroyImmediate(obj);
            Debug.Log("AI deleted object " + cmd.objectName);
        }
    }

    static void ListScene()
    {
        GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        List<string> names = new List<string>();

        foreach (var obj in objects)
        {
            names.Add(obj.name);
        }

        Debug.Log("Scene Objects: " + string.Join(", ", names));
    }
}

[Serializable]
public class BridgeCommand
{
    public string action;
    public string type;
    public string name;
    public string objectName;
    public string component;
}