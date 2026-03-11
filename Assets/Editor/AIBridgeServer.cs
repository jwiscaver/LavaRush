using UnityEngine;
using UnityEditor;
using System.Net;
using System.Text;
using System.Threading;

[InitializeOnLoad]
public class AIBridgeServer
{
    private static HttpListener listener;
    private static Thread serverThread;

    static AIBridgeServer()
    {
        StartServer();
    }

    static void StartServer()
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8090/");
        listener.Start();

        serverThread = new Thread(Listen);
        serverThread.Start();

        Debug.Log("AI Bridge running on port 8090");
    }

    static void Listen()
    {
        while (true)
        {
            var context = listener.GetContext();
            var request = context.Request;
            var response = context.Response;

            string command = request.QueryString["cmd"];

            if (command == "createCube")
            {
                EditorApplication.delayCall += () =>
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.name = "AI Cube";
                };
            }

           /* if (command == "createPlayer")
            {
                EditorApplication.delayCall += () =>
                {
                    GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    player.name = "Player";

                    player.AddComponent<PlayerController>();
                };
            }*/

            byte[] buffer = Encoding.UTF8.GetBytes("ok");
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();
        }
    }
}