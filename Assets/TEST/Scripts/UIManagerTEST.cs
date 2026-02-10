using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using Newtonsoft.Json;


public class UIManagerTEST : MonoBehaviour
{
    private const string LOGINURL = "https://stone-api.meltyn.fr/auth/login";
    private const string GETPLAYERMEURL = "https://stone-api.meltyn.fr/players/me";
    private string _tokenReceived;
    private PlayerMe _playerConnected;
    [SerializeField] private TMP_InputField _pseudoInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private GameObject _badIdImage;
    [SerializeField] private GameObject _itinerariesPanel;
    [SerializeField] private GameObject _connectionPanel;
    [SerializeField] private TMP_Text _playerNameText;

    private void Start()
    {
        _connectionPanel.SetActive(true);
        _itinerariesPanel.SetActive(false);
        _badIdImage.SetActive(false);
    }

    #region ---- UI INTERACTIONS ----

    public void ValidateConnection()
    {
        string pseudo = _pseudoInput.text;
        string password = _passwordInput.text;
        Debug.Log($"{pseudo}  et {password}");
        Login(pseudo, password);
    }

    public void BackToConnectionMenu()
    {
        _connectionPanel.SetActive(true);
        _itinerariesPanel.SetActive(false);

        _pseudoInput.text = "";
        _passwordInput.text = "";
    }

    public void UpdatePlayerName()
    {
        if (_playerConnected != null && _playerNameText != null)
        {
            _playerNameText.text = _playerConnected.title.url;
        }
    }

    #endregion


    #region ---- CONNECTING ----

    public void Login(string pseudo, string password)
    {
        _badIdImage.SetActive(false);
        StartCoroutine(LoginCoroutine(pseudo, password));
    }

    private IEnumerator LoginCoroutine(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(LOGINURL, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"HTTP ERROR : {request.responseCode}");
                Debug.LogError(request.error);
                Debug.LogError(request.downloadHandler.text);
                _badIdImage.SetActive(true);
            }
            else
            {
                _badIdImage.SetActive(false);
                Debug.Log("LOGIN OK");
                Debug.Log(request.downloadHandler.text);

                LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);

                PlayerPrefs.SetString("auth_token", response.access_token);
                PlayerPrefs.Save();

                Debug.Log("Token sauvegardé : " + response.access_token);

                StartCoroutine(GetPlayerCoroutine());

                _itinerariesPanel.SetActive(true);
                _connectionPanel.SetActive(false);
            }
        }
    }

    private IEnumerator GetPlayerCoroutine()
    {
        _tokenReceived = PlayerPrefs.GetString("auth_token", "");

        if (string.IsNullOrEmpty(_tokenReceived))
        {
            Debug.LogError("Missing Token.");
            yield break;
        }

        UnityWebRequest request = UnityWebRequest.Get(GETPLAYERMEURL);
        request.SetRequestHeader("Authorization", "Bearer " + _tokenReceived);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"HTTP ERROR : {request.responseCode}");
            Debug.LogError(request.error);
            Debug.LogError(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("GET /player/me OK");
            Debug.Log(request.downloadHandler.text);
            string json = request.downloadHandler.text;
            _playerConnected = JsonConvert.DeserializeObject<PlayerMe>(json);

            Debug.Log("Coins : " + _playerConnected.coins);
            Debug.Log("Gems : " + _playerConnected.gems);
            Debug.Log("Level : " + _playerConnected.level);
            Debug.Log("XP : " + _playerConnected.xp);
            Debug.Log("Total Distance : " + _playerConnected.total_distance);
            Debug.Log("Icon URL : " + _playerConnected.icon.url);
            Debug.Log("Banner URL : " + _playerConnected.banner.url);
            Debug.Log("Title URL : " + _playerConnected.title.url);

            UpdatePlayerName();
        }
    }

    #endregion
}
