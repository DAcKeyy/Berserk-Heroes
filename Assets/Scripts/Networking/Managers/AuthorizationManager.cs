using System;
using System.Threading.Tasks;
using Berserk.Messaging.Messages;
using Berserk.Messaging;
using Berserk.Networking.Messages;
using Other;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Berserk.Networking.Managers
{
    public class AuthorizationManager : MonoBehaviour
    {
        [SerializeField] private PanelController _panelController;
        [SerializeField] private TMP_InputField EmailInputField;
        [SerializeField] private TMP_InputField RestorePassEmailInputField;
        [SerializeField] private TMP_InputField PasswordInputField;
        
        private FieldsValidator _filedsValidator = new FieldsValidator();

        [SerializeField] private int minNickLenght = 1;
        [SerializeField] private int minPasswordLenght = 6;

        private void Start()
        {
            _filedsValidator.SetPasswordLenght(minPasswordLenght);

            _panelController.DisableAllPanels();
            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.LoadingPanel);

            ServerManager.ServerManagerInstance.FailedToConnect += delegate ()
            {
                _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.InformationPanel);
                _panelController.ShowInformationTextOnPanel("Включи сервер, чудище",PanelComponent.UserAuthorizationPanels.InformationPanel);
            };



            MessageHandeler.MessageHandelerInstance.SendCommandMessage(new Message("Command ValidateUnityVersion",
                    false, 0, "", JsonMessage.FromValue(new UnityConnection(Application.version, ""))))
                .ContinueWith(continueTask =>
                {
                   // Debug.Log(continueTask.Status);
                    UnityMainThread.wkr.AddJob(() =>
                    { 
                        if (continueTask.Status == TaskStatus.RanToCompletion)
                        {
                            if (continueTask.Result.Vaild)
                            {
                                _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.ChoseTypeOpAuntificationPanel);

                                if (!string.IsNullOrEmpty(StaticPrefs.AccessToken))
                                {
                                    _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.LoadingPanel);
                                    Send_Data("", "", StaticPrefs.AccessToken);
                                }
                            }
                            else
                            {
                                _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.InformationPanel);
                                _panelController.ShowInformationTextOnPanel(continueTask.Result.Text,PanelComponent.UserAuthorizationPanels.InformationPanel);
                            }
                        }
                        else
                        {
                            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.InformationPanel);
                            _panelController.ShowInformationTextOnPanel("Внутренняя ошибка клиента",PanelComponent.UserAuthorizationPanels.InformationPanel);
                        }
                    });
                });
        }
        
        void Send_Data(string Email,string Password, string AccessToken)
        {
            MessageHandeler.MessageHandelerInstance.SendCommandMessage(
                    new Message("Command AuthorizeUser", false,(uint)StaticPrefs.UserID, AccessToken, 
                        JsonMessage.FromValue(
                            new Authorization(AccessToken, Email, Password, ""))))
                .ContinueWith(continueTask =>
                {
                    UnityMainThread.wkr.AddJob(() =>
                    { 
                        if (continueTask.Status == TaskStatus.RanToCompletion)
                        {
                            if (continueTask.Result.Vaild)///////////////////////////////////////////////////////////Вход
                            {
                                StaticPrefs.AccessToken = continueTask.Result.AccessToken;
                                StaticPrefs.NickName = continueTask.Result.AccessToken;

                                _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.LoadingPanel);
                                _panelController.ShowInformationTextOnPanel(continueTask.Result.Text,PanelComponent.UserAuthorizationPanels.LoadingPanel);

                                SceneManager.LoadSceneAsync("Main Menu");
                            }
                            else
                            {
                                _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.ChoseTypeOpAuntificationPanel);
                                _panelController.ShowInformationTextOnPanel(continueTask.Result.Text,PanelComponent.UserAuthorizationPanels.ChoseTypeOpAuntificationPanel);
                            }
                        }
                        else
                        {
                            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.InformationPanel);
                            _panelController.ShowInformationTextOnPanel("Внутренняя ошибка клиента",PanelComponent.UserAuthorizationPanels.InformationPanel);
                        }
                    });
                });
        }

        #region  UI

        public void ForgettenPasswordValidate()
        {
            if(!_filedsValidator.ValidateEmail(RestorePassEmailInputField.text))
            {
                _panelController.ShowInformationTextOnPanel($"Почта {RestorePassEmailInputField.text} введена не верно",
                    PanelComponent.UserAuthorizationPanels.UsualRestorePasswordPanel);
                return;
            }
            SendToServerRestorePassMessage(true, PasswordInputField.text, EmailInputField.text, "");
        }
        public void StartRestorePassword()
        {
            _panelController.DisableAllPanels();
            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.UsualRestorePasswordPanel);
        }
        
        public void StartUsualAuthorization()
        {
            _panelController.DisableAllPanels();
            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.UsualAuntificationPanel);
        }
        public void ValidateEmailAndPassword()
        {
            if(!_filedsValidator.ValidatePassword(PasswordInputField.text))
            {_panelController.ShowInformationTextOnPanel($"Пароль должен содержать не меньше {minPasswordLenght} символов", 
                    PanelComponent.UserAuthorizationPanels.UsualAuntificationPanel);
                return;
            }
            
            if(!_filedsValidator.ValidateEmail(EmailInputField.text))
            {
                _panelController.ShowInformationTextOnPanel(
                    $"Неправельно введена почта",
                    PanelComponent.UserAuthorizationPanels.UsualAuntificationPanel);
                return;
            }

            Send_Data(EmailInputField.text, PasswordInputField.text, "");
        }

        #endregion
        void SendToServerRestorePassMessage(bool isPasswordRestore,string Email,string password, string nickname)
        {
            /*
            Authorization authorizationMessage = new Authorization(isPasswordRestore,Email,password,nickname);
            
            MessageHandeler.MessageHandelerInstance.SendCommandMessage(
                    new Message("Command AuthorizeUser", false,(uint)StaticPrefs.UserID,"", 
                        JsonMessage.FromValue(
                            new Authorization(isPasswordRestore,Email,password,nickname))))
                .ContinueWith(continueTask =>
                {
                    UnityMainThread.wkr.AddJob(() =>
                    { 
                        if (continueTask.Status == TaskStatus.RanToCompletion)
                        {
                            if (continueTask.Result.Vaild) Debug.Log("Иди поспи, ты красава");
                            else
                            {
                                _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.ChoseTypeOpAuntificationPanel);
                                _panelController.ShowInformationTextOnPanel(continueTask.Result.Text,PanelComponent.UserAuthorizationPanels.ChoseTypeOpAuntificationPanel);
                            }
                        }
                        else
                        {
                            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.InformationPanel);
                            _panelController.ShowInformationTextOnPanel("Внутренняя ошибка клиента",PanelComponent.UserAuthorizationPanels.InformationPanel);
                        }
                    });
                });
                */
            
            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.ChoseTypeOpAuntificationPanel);
            _panelController.ShowInformationTextOnPanel("эта функция пока не доступна, пожалуйста создатейте нового пользователя", 
                PanelComponent.UserAuthorizationPanels.ChoseTypeOpAuntificationPanel);
        }
    }
}
