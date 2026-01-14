using System.ClientModel;
using Microsoft.Agents.AI.DevUI;
using Microsoft.Agents.AI.Hosting;
using Microsoft.Extensions.AI;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

var githubToken = builder.Configuration["GitHub:Token"]!;
var githubEndpoint = builder.Configuration["GitHub:Endpoint"]!;
var githubModel = builder.Configuration["GitHub:Model"]!;

var openAIClientOptions = new OpenAIClientOptions
{
    Endpoint = new Uri(githubEndpoint)
};

var openAIClient = new OpenAIClient(new ApiKeyCredential(githubToken), openAIClientOptions);
var chatClient = openAIClient.GetChatClient(githubModel).AsIChatClient();

builder.AddAIAgent("AIAgent name", "AIAgent instructions", chatClient);

builder.AddOpenAIConversations();
builder.AddOpenAIResponses();

builder.AddDevUI();

var app = builder.Build();

app.MapOpenAIConversations();
app.MapOpenAIResponses();

app.MapDevUI();

app.Run();