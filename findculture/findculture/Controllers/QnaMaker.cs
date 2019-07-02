using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Net;

namespace findculture.Controllers
{
    class QnaMaker
    {
        public static async Task<string> Qna(string query)
        {
            var knowledgebaseId = "63225f3b-b129-49af-8e9d-d29d0e2b5262";
            var qnamakerSubscriptionKey = "8702a2773664453793e66a5ec8219b7c";
            Uri qnamakerUriBase = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v1.0");
            var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{knowledgebaseId}/generateAnswer");
            var postBody = $"{{\"question\": \"{query}\"}}";
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add("Ocp-Apim-Subscription-Key", qnamakerSubscriptionKey);
                client.Headers.Add("Content-Type", "application/json");
                var responseString = client.UploadString(builder.Uri, postBody);
                QnAMakerResult response1;
                try
                {
                    response1 = JsonConvert.DeserializeObject<QnAMakerResult>(responseString);
                    return response1.Answer;
                }
                catch
                {
                    throw new Exception("Unable to deserialize QnA Maker response string.");
                }              
            }            
        }

        public class QnAMakerResult
        {
            /// <summary>
            /// The top answer found in the QnA Service.
            /// </summary>
            [JsonProperty(PropertyName = "answer")]
            public string Answer { get; set; }

            /// <summary>
            /// The score in range [0, 100] corresponding to the top answer found in the QnA    Service.
            /// </summary>
            [JsonProperty(PropertyName = "score")]
            public double Score { get; set; }
        }
    }
}