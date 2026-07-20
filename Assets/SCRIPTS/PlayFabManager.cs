using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance;

    [Header("Register Data")]
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerEmail;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private TMP_InputField confirmedPassword;
    [SerializeField] private Button registerButton;

    [Header("Login Data")]
    [SerializeField] private TMP_InputField loginPassword;
    [SerializeField] private TMP_InputField loginEmail;
    [SerializeField] private Button loginButton;

    [Header("Player Data")]
     public int victories;
     public int kills;
     public int deaths;


    [SerializeField] private GameObject loginRegisterPanel;
    
    public event Action<Dictionary<string, string>> OnReceivedData;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != null)
        {
            Destroy(this.gameObject);
        }

        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "E820E";
        }


    }

    //public void RegisterUser()
    //{
    //    RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
    //    {
    //        Username = "",
    //        Email = "",
    //        Password = "",
    //        RequireBothUsernameAndEmail = true,
    //    };

    //    PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterUserSuccess, OnPlayFabError);


    //}

    //public void OnRegisterUserSuccess(RegisterPlayFabUserResult result)
    //{

    //}

    //public void OnPlayFabError(PlayFabError error)
    //{
    //    Debug.Log(error);
    //}


    
    public async void RegisterUserInPlayFab()
    {
        if (registerPassword.text != confirmedPassword.text) return;
        try
        {
            var registerTask = RegisterUserInPlayFabTask();
            await registerTask;
            loginRegisterPanel.SetActive(false);
            registerUsername.text = string.Empty;
            registerEmail.text = string.Empty;
            registerPassword.text = string.Empty;
            confirmedPassword.text = string.Empty;
        }
        catch (Exception error)
        {
            Debug.Log(error);
        }
    }

    public async Task<RegisterPlayFabUserResult> RegisterUserInPlayFabTask()
    {
        var taskSource = new TaskCompletionSource<RegisterPlayFabUserResult>();

        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            Username = registerUsername.text,
            Email = registerEmail.text,
            Password = registerPassword.text,
            RequireBothUsernameAndEmail = true,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, resultCallback => taskSource.SetResult(resultCallback),
            errorCallback => taskSource.SetException(new Exception(errorCallback.GenerateErrorReport())));

        return await taskSource.Task;
    }



    
    public async void LoginInPlayFab()
    {
        try
        {
            var loginTask = LoginWithEmailTask();
            await loginTask;
            Debug.Log("Inicio de Sesion Exitoso");
            loginRegisterPanel.SetActive(false);
            loginEmail.text = string.Empty;
            loginPassword.text = string.Empty;

        }
        catch (Exception error)
        {
            Debug.Log(error);
        }
    }


    public async Task<LoginResult> LoginWithEmailTask()
    {
        var taskSource = new TaskCompletionSource<LoginResult>();

        LoginWithEmailAddressRequest request = new LoginWithEmailAddressRequest()
        {
            Email = loginEmail.text,
            Password = loginPassword.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, resultCallback => taskSource.SetResult(resultCallback), errorCallback => taskSource.SetException(new Exception(errorCallback.GenerateErrorReport())));

        return await taskSource.Task;
    }



    [ContextMenu("UpdateData")]
    public async void UpdatePlayerData()
    {
        try
        {
            var updateDataTask = UpdatePlayerDataTask();
            await updateDataTask;
        }
        catch (Exception error)
        {
            Debug.Log(error);
        }
    }

    public async Task<UpdateUserDataResult> UpdatePlayerDataTask()
    {
        var taskSource = new TaskCompletionSource<UpdateUserDataResult>();

        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                {"Victories", victories.ToString()},
                {"Kills", kills.ToString()},
                {"Deaths", deaths.ToString()}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, resultCallback => taskSource.SetResult(resultCallback),
            errorCallback => taskSource.SetException(new Exception(errorCallback.GenerateErrorReport())));

        return await taskSource.Task;
    }


    [ContextMenu("GetData")]
    public async void GetPlayerData()
    {
        try
        {
            var playerDataTask = GetPlayerDataTask();
            await playerDataTask;
            if (playerDataTask.Result.Data != null)
            {
                Dictionary<string, string> dataDic = playerDataTask.Result.Data.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Value
                    );

                OnReceivedData?.Invoke(dataDic);
            }


        }
        catch (Exception error)
        {
            Debug.Log(error);
        }
    }

    public async Task<GetUserDataResult> GetPlayerDataTask()
    {
        var taskSource = new TaskCompletionSource<GetUserDataResult>();

        GetUserDataRequest request = new GetUserDataRequest()
        {
            Keys = new List<string>
            {
                "Victories",
                "Kills",
                "Deaths"
            }
        };

        PlayFabClientAPI.GetUserData(request, resultCallback => taskSource.SetResult(resultCallback), errorCallback => taskSource.SetException(new Exception(errorCallback.GenerateErrorReport())));

        return await taskSource.Task;
    }




    public void CheckIfRegisterEmpty()
    {
        if (registerUsername.text.IsNullOrEmpty() || registerPassword.text.IsNullOrEmpty() || confirmedPassword.text.IsNullOrEmpty() || registerEmail.text.IsNullOrEmpty())
        {
            registerButton.interactable = false;
            return;
        }
        registerButton.interactable = true;
    }
    public void CheckIfLoginEmpty()
    {
        if (loginPassword.text.IsNullOrEmpty() || loginEmail.text.IsNullOrEmpty())
        {
            loginButton.interactable = false;
            return;
        } 
            
        loginButton.interactable = true;
    }
}
