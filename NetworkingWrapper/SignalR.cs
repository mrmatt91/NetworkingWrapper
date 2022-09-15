using Microsoft.AspNetCore.SignalR.Client;

namespace NetworkingWrapper
{
    public class SignalR
    {
        public static async void Init()
        {
            await Connection.Instance.hubConnection.StartAsync();
        }

        public static async void Disconnect()
        {
            await Connection.Instance.hubConnection.StopAsync();
        }

        public static async void Init(Action<Task> postConnectionTask)
        {
            await Connection.Instance.hubConnection.StartAsync().ContinueWith(postConnectionTask.Invoke);
        }

        public static void AddEventSubscription<T>(string methodName, Action<T> callbackAction)
        {
            Connection.Instance.hubConnection.On(methodName, callbackAction);
        }
    }
}
