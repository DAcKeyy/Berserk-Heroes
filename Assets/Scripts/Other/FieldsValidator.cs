using System;
using System.Net.Mail;

namespace Other
{
    public interface IValidatorFluent
    {
        IValidatorFluent SetPasswordLenght(int lenght);
        IValidatorFluent SetNicknameLenght(int lenght);
    }
    
    /// <summary>
    /// Класс, проверяющий валидность специфичного текста
    /// </summary>
    public class FieldsValidator : IValidatorFluent
    {
        private int NicknameLenght = 1;
        private int PasswordLenght = 1;

        /// <summary>
        /// Установка правильной длинны пароля
        /// </summary>
        /// <param name="lenght">Длинна пароля</param>
        /// <returns></returns>
        public IValidatorFluent SetPasswordLenght(int lenght)
        {
            this.PasswordLenght = lenght;
            return this;
        }
        
        /// <summary>
        /// Установка правильной длинны никнейма
        /// </summary>
        /// <param name="lenght">Длинна никнейма</param>
        /// <returns></returns>
        public IValidatorFluent SetNicknameLenght(int lenght)
        {
            this.NicknameLenght = lenght;
            return this;
        }

        /// <summary>
        /// Метод проверки парвильности пароля
        /// </summary>
        /// <param name="password">Строка пароля</param>
        /// <returns>True если пароль соответствет нужным парраметрам</returns>
        public bool ValidatePassword(string password)
        {
            if (password.Length < PasswordLenght) return false;
            else return true;
        }
        /// <summary>
        /// Метод проверки парвильности почты
        /// </summary>
        /// <param name="password">Строка почты</param>
        /// <returns>True если почта соответствет нужным парраметрам</returns>
        public bool ValidateEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Метод проверки парвильности никнейма
        /// </summary>
        /// <param name="password">Строка никнейма</param>
        /// <returns>True если никнейм соответствет нужным парраметрам</returns>
        public bool ValidateNickname(string nickname)
        {
            if (nickname.Length < NicknameLenght) return false;
            else return true;
        }
    }
}