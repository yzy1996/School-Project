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
using findculture.Controllers;

namespace findculture.Dialogs
{
    [LuisModel("d6d84f99-f550-4e33-8899-e5e75fd4fa2f", "56be90b4b8f140f0ad4df800ffeaa437", domain: "westus.api.cognitive.microsoft.com")]
    [Serializable]
    public class SimpleNoteDialog : LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            await context.PostAsync("该地点我还没有收录！");
            context.Wait(MessageReceived);
        }

        [LuisIntent("历史")]
        public async Task history(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            string answer = await QnaMaker.Qna(message.Text);
            await context.PostAsync(answer);
            context.Wait(MessageReceived);
        }
    }
}