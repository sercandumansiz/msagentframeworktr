using System.ClientModel;
using Microsoft.Agents.AI.DevUI;
using Microsoft.Agents.AI.Hosting;
using Microsoft.Extensions.AI;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

var openAIClientOptions = new OpenAIClientOptions
{
    Endpoint = new Uri("https://models.github.ai/inference")
};

var openAIClient = new OpenAIClient(new ApiKeyCredential("GitHub TOKEN"), openAIClientOptions);

var chatClient = openAIClient.GetChatClient("gpt-4o-mini").AsIChatClient();

builder.AddAIAgent("AIAgent name", "AIAgent instructions", chatClient);

builder.AddOpenAIConversations();
builder.AddOpenAIResponses();

builder.AddDevUI();

var app = builder.Build();

app.MapOpenAIConversations();
app.MapOpenAIResponses();

app.MapDevUI();

app.Run();