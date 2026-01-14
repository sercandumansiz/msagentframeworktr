using Microsoft.Agents.AI.AGUI;
using Microsoft.Extensions.AI;

string AGUI_URL = "http://localhost:5219/agui";

HttpClient httpClient = new HttpClient();

AGUIChatClient aguiChatClient = new AGUIChatClient(httpClient, AGUI_URL);

while (true)
{
    var input = Console.ReadLine();

    await foreach (ChatResponseUpdate update in aguiChatClient.GetStreamingResponseAsync(input))
    {
        foreach (AIContent aiContent in update.Contents)
        {
            Console.WriteLine(aiContent);
        }
    }
}