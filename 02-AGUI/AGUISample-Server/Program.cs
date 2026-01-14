using System.ClientModel;
using Microsoft.Agents.AI.Hosting.AGUI.AspNetCore;
using Microsoft.Extensions.AI;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Configuration'dan GitHub ayarlarını oku
var githubToken = builder.Configuration["GitHub:Token"]!;
var githubEndpoint = builder.Configuration["GitHub:Endpoint"]!;
var githubModel = builder.Configuration["GitHub:Model"]!;

var openAIClientOptions = new OpenAIClientOptions
{
    Endpoint = new Uri(githubEndpoint)
};

var openAIClient = new OpenAIClient(new ApiKeyCredential(githubToken), openAIClientOptions);
var chatClient = openAIClient.GetChatClient(githubModel).AsIChatClient();

var agent = chatClient.CreateAIAgent(
    name: "AGUISampleAgent", 
    instructions: "AGUISampleAgent Instructions");

builder.Services.AddAGUI();

var app = builder.Build();

app.MapAGUI("/agui", agent);

app.Run();