using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;

namespace YourApp.AI
{
    public class ChatService
    {
        private readonly IChatClient _chatClient;

        public ChatService(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<string?> GetResponseAsync(string prompt)
        {
            var result = await _chatClient.CompleteAsync(prompt);
            return result.Choices?.FirstOrDefault()?.Text;
        }
    }
}
