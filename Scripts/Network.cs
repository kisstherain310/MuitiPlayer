using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Network : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }

        GUILayout.EndArea();
    }
    static void StartButtons()
    {
        if (GUILayout.Button("Tạo phòng"))
        {
            NetworkManager.Singleton.StartHost();
        }

        if (GUILayout.Button("Vào phòng"))
        {
            NetworkManager.Singleton.StartClient();
        }

    }
}