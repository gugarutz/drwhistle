namespace DrWhistle.Application.Common.Models
{
    /// <summary>
    /// Email settings.
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether [enable SSL].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable SSL]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Gets or sets the delivery method.
        /// </summary>
        /// <value>
        /// The delivery method.
        /// </value>
        public EmailDeliveryMethod DeliveryMethod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use default credentials].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use default credentials]; otherwise, <c>false</c>.
        /// </value>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets or sets the pickup directory location.
        /// </summary>
        /// <value>
        /// The pickup directory location.
        /// </value>
        public string PickupDirectoryLocation { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Noreply address.
        /// </summary>
        /// <value>
        /// The noreply email address.
        /// </value>
        public string FromAddress { get; set; }

        /// <summary>
        /// Gets or sets the send grid API key.
        /// </summary>
        /// <value>
        /// The send grid API key.
        /// </value>
        public string SendGridApiKey { get; set; }
    }
}