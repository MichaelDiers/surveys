namespace MailerService
{
	public interface IMailerProvider
	{
		void Send(Message message);
	}
}