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

    void Start()
    {
        // Chờ cho đến khi đối tượng mạng được khởi tạo trước khi thực hiện các thay đổi
        if (IsServer)
        {
            score.Value = 0;
        }
        score.OnValueChanged += OnScoreChanged;
        UpdateScoreText();
    }
    public void IncreaseScore()
    {
        if (IsClient)
        {
            SubmitIncreaseScoreServerRpc();
        }
        else
        {
            score.Value++;
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
            score.Value--;
        }
    }

    // RPC gửi yêu cầu tăng điểm từ Client lên Server
    [ServerRpc(RequireOwnership = false)]
    private void SubmitIncreaseScoreServerRpc()
    {
        score.Value++;
    }

    // RPC gửi yêu cầu giảm điểm từ Client lên Server
    [ServerRpc(RequireOwnership = false)]
    private void SubmitDecreaseScoreServerRpc()
    {
        score.Value--;
    }

    private void OnScoreChanged(int oldValue, int newValue)
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.Value.ToString();
    }
}

