namespace DrWhistle.Application.Common.Models
{
    // Summary:
    //     Specifies how email messages are delivered.
    public enum EmailDeliveryMethod
    {
        /// <summary>
        /// Email is sent through the network to an SMTP server.
        /// </summary>
        Network = 0,

        /// <summary>
        /// Email is copied to the directory specified by the System.Net.Mail.SmtpClient.PickupDirectoryLocation
        /// property for delivery by an external application.
        /// </summary>
        SpecifiedPickupDirectory = 1,
    }
}