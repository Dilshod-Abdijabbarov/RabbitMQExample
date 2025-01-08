namespace RabbitMQExample.Services
{
    public interface IRabbitMQService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}
