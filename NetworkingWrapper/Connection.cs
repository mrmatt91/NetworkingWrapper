using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetworkingWrapper
{
    public sealed class Connection
    {
        public HubConnection hubConnection { get; set; }
        private Connection()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8019/InteractiveHub", options =>
                {
                    options.Headers.Add("X-Api-Key", "somevalue");
                })
                .WithAutomaticReconnect(new[] {
                    TimeSpan.Zero,
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(7),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(30),
                })
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .Build();
        }
        private static readonly Lazy<Connection> instance = new Lazy<Connection>(() => new Connection());
        public static Connection Instance => instance.Value;
    }
}
