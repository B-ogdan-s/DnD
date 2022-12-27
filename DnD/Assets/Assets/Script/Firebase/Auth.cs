using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using Sirenix.OdinInspector;

public class Auth : MonoBehaviour
{
    public static FirebaseAuth _auth;
    public static FirebaseUser _user;

    public static Action _OpenFirebase;
    public static Action _CloseFirenase;

    public static Action _SetName;
    public static Action<int> _SetRecord;



    private void Start()
    {
        FirebaseUI.LoginUI._Login += Login;
        FirebaseUI.RegisterUI._Register += Register;

        DontDestroyOnLoad(this);
        InitializeFirebase();
    }

    public void Login(string email, string password)
    {
        StartCoroutine(CR_Login(email, password));
    }
    public void Register(string email, string password, string name)
    {
        StartCoroutine(CR_Register(email, password, name));
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        _auth = FirebaseAuth.DefaultInstance;
        _auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (_auth.CurrentUser != _user)
        {
            bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
            if (!signedIn && _user != null)
            {
                _OpenFirebase?.Invoke();
            }
            _user = _auth.CurrentUser;
        }
        else if (_user == null)
        {
            _OpenFirebase?.Invoke();
        }
    }
    [Button]
    public void OnSignOut()
    {
        _auth.SignOut();
    }

    private IEnumerator CR_Login(string email, string password)
    {
        var loginTask = _auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError error = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (error)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            FirebaseUI.ErrorUI._instance.SetError(message);
            Debug.Log("Error");
        }
        else
        {
            _user = loginTask.Result;
            _CloseFirenase?.Invoke();
        }
    }
    private IEnumerator CR_Register(string email, string password, string name)
    {
        var registerTask = _auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            FirebaseException firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Register Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WeakPassword:
                    message = "Weak Password";
                    break;
                case AuthError.EmailAlreadyInUse:
                    message = "Email Already In Use";
                    break;
            }
            FirebaseUI.ErrorUI._instance.SetError(message);
        }
        else
        {
            _user = registerTask.Result;
            if (_user != null)
            {
                UserProfile profile = new UserProfile { DisplayName = name };
                var profileTask = _user.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => profileTask.IsCompleted);
                if (profileTask.Exception != null)
                {
                    Debug.Log("Error");
                }
            }
            _SetName?.Invoke();
            _SetRecord?.Invoke(0);
            _CloseFirenase?.Invoke();
        }
    }

    private void OnDestroy()
    {
        FirebaseUI.RegisterUI._Register -= Register;
        FirebaseUI.LoginUI._Login -= Login;
        _auth.StateChanged -= AuthStateChanged;
        _auth = null;
    }
}
