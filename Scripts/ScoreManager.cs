/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class ScoreManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    // private int score = 0; // Điểm số ban đầu
    private NetworkVariable<int> score = new NetworkVariable<int>(0);

    void Start()
    {
        UpdateScoreText(); // Cập nhật điểm số ban đầu
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    public void DecreaseScore()
    {
        score--;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
*/

using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class ScoreManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private NetworkVariable<int> score = new NetworkVariable<int>(0);

    public GameObject direct;

    public void IncreaseScore()
    {
        if (IsClient)
        {
            SubmitIncreaseScoreServerRpc();
        }
        else
        {
            UpdateDirectStateClientRpc(true);
        }
    }

    public void DecreaseScore()
    {
        if (IsClient)
        {
            SubmitDecreaseScoreServerRpc();
        }
        else
        {
            UpdateDirectStateClientRpc(false);
        }
    }

    // RPC gửi yêu cầu tăng điểm từ Client lên Server
    [ServerRpc(RequireOwnership = false)]
    private void SubmitIncreaseScoreServerRpc()
    {
        UpdateDirectStateClientRpc(true);
    }

    // RPC gửi yêu cầu giảm điểm từ Client lên Server
    [ServerRpc(RequireOwnership = false)]
    private void SubmitDecreaseScoreServerRpc()
    {
        UpdateDirectStateClientRpc(false);
    }

    [ClientRpc]
    private void UpdateDirectStateClientRpc(bool isActive)
    {
        direct.SetActive(isActive);
    }
}

