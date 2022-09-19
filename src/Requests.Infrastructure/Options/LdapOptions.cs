namespace Requests.Infrastructure.Options
{
    internal class LdapOptions
    {
        /// <summary>
        /// Домейн
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
