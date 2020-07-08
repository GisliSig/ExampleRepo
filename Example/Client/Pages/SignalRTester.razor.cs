using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Client.Pages
{
    public class SignalRTesterBase : ComponentBase
    {
        private HubConnection hubConnection;

        public List<string> messages = new List<string>();

        [Inject]
        private NavigationManager NavManager { get; set; }

        public string messageinput;

        protected override async Task OnInitializedAsync()
        {
            await StartHubConnection();
        }

        async Task StartHubConnection()
        {
            hubConnection = new HubConnectionBuilder()
             .WithUrl(NavManager.ToAbsoluteUri("/message"))
             .Build();


            hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                messages.Add(message);
                StateHasChanged();
            });


            await hubConnection.StartAsync();
        }

        public async Task SendMessage()
        {
            await hubConnection.SendAsync("SendMessage", messageinput);
            messageinput = "";
        }
    }
}
