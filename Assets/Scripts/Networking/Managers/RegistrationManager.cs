using System.Threading.Tasks;
using Berserk.Messaging;
using Berserk.Messaging.Messages;
using Other;
using TMPro;
using UnityEngine;

namespace Berserk.Networking.Managers
{
    public class RegistrationManager : MonoBehaviour
    {
        [SerializeField] private PanelController _panelController;
        [SerializeField] private TMP_InputField EmailInputField;
        [SerializeField] private TMP_InputField PasswordInputField;
        [SerializeField] private TMP_InputField NicknameInputField;
        
        private FieldsValidator _filedsValidator = new FieldsValidator();

        [SerializeField] private int minNickLenght = 1;
        [SerializeField] private int minPasswordLenght = 6;
        private void Start()
        {
            _filedsValidator.SetNicknameLenght(minNickLenght);
            _filedsValidator.SetPasswordLenght(minPasswordLenght);
        }

        #region UI
        public void StartUsualRegistration()
        {
            _panelController.DisableAllPanels();
            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.UsualRegistartionPanel);
        }
        
        public void ValidateEmailAndPassword()
        {
            if(!_filedsValidator.ValidateEmail(EmailInputField.text))
            {_panelController.ShowInformationTextOnPanel($"Почта {EmailInputField.text} введена не верно", 
                    PanelComponent.UserAuthorizationPanels.UsualRegistartionPanel);
                return;
            }
            
            if(!_filedsValidator.ValidatePassword(PasswordInputField.text))
            {_panelController.ShowInformationTextOnPanel($"Пароль должен содержать не меньше {minPasswordLenght} символов", 
                    PanelComponent.UserAuthorizationPanels.UsualRegistartionPanel);
                return;
            }
            
            _panelController.DisableAllPanels();
            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.UsualRegistartionNicknamePanel);
        }

        public void ValidateNickname()
        {
            if(!_filedsValidator.ValidateNickname(NicknameInputField.text))
            {_panelController.ShowInformationTextOnPanel($"Имя пользователя должно содержать не меньше {minNickLenght} символов", 
                    PanelComponent.UserAuthorizationPanels.UsualRegistartionNicknamePanel);
                return;
            }
            
            _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.LoadingPanel);
            
            SendToServerRegistartionMessage(EmailInputField.text,PasswordInputField.text,NicknameInputField.text);
        }

        

        #endregion

        #region Network
        public void SendToServerRegistartionMessage(string Email, string Password, string Nickname)
        {
            MessageHandeler.MessageHandelerInstance.SendCommandMessage(new Message("Command CreateAccount",
                    false, 0, "", JsonMessage.FromValue( new Registration(Email, Password, Nickname))))
                .ContinueWith(continueTask =>
                {
                    // Debug.Log(continueTask.Status);
                    UnityMainThread.wkr.AddJob(() =>
                    { 
                        if (continueTask.Status == TaskStatus.RanToCompletion)
                        {
                            if (continueTask.Result.Vaild)
                            {
                                StaticPrefs.AccessToken =
                                    continueTask.Result.MessageBody.Value.ToObject<Registration>().AccessToken;
                                _panelController.EnablePanel(PanelComponent.UserAuthorizationPanels.ChoseTypeOpAuntificationPanel);
                                _panelController.ShowInformationTextOnPanel(continueTask.Result.Text,PanelComponent.UserAuthorizationPanels.ChoseTypeOpAuntificationPanel);
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
        #endregion
       
    }
}
