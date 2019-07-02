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
using Bot_Application2.Controllers;

namespace Bot_Application2.Dialogs
{
    [LuisModel("90bdab43-75d8-4224-b3af-d9d007bc866a", "175942d79ed945e4b504124f2f44ed12", domain: "southeastasia.api.cognitive.microsoft.com")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {

        public const string Position = "职位";
        public const string Name = "人物名称";
        public const string College = "学院名称";
        public const string Generaljudgment = "一般疑问句判断词";
        public const string Logicalorder = "次序逻辑";
        public const string Phone = "电话";
        public const string Web = "网站";
        public const string Objectname = "概况对象名";
        public const string City = "城市";
        public const string Province = "省份";
        public const string Song = "校歌";
        public const string Motto = "校训";
        public const string Inforobjectlocation = "信息对象地点";
        public const string Email = "邮箱";
        public const string Schoolrelated = "学校相关";
        public const string Title = "头衔";
        public const string Verb = "动词";
        public static string[] Principal= {"王树国","郑南宁","徐通模","将德名","史维祥","庄礼庭","陈吾愚","彭康","李培南","吴有训","王之卓","程孝刚","吴宝丰","徐名材","黎照寰" };
        public static string[] Partysecretary = {"张迈曾","王建华","王文生","潘季","陈明焰","苏庄","刘若曾","林茵如","彭康"};
        public static string record1;   //记录职位
        public static string record2;   //记录学院
        public static string record3;   //记录头衔
        public static int count1 = 0;   //记录校长任数
        public static int count2 = 0;    //记录党委书记任数
        public static int count3 = 3;   //记录逻辑是从什么地方来的


        [LuisIntent("None")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            count1 = 0;
            count2 = 0;
            count3 = 3;
            var message = await activity;
            string answer = await QnaMaker.Qna(message.Text);
            if (answer != "No good match found in the KB")
            {
                await context.PostAsync(answer);
            }
            else
            {
                await context.PostAsync("你在故意刁难本Orange机器人，请换一个问题！");
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("人物信息查询")]
        public async Task Search(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            if (message.Text.IndexOf("上一任") != -1 || message.Text.IndexOf("前任") != -1 || message.Text.IndexOf("前一任") != -1
                    || message.Text.IndexOf("上届") != -1 || message.Text.IndexOf("上一届") != -1 || message.Text.IndexOf("前届") != -1)
            {
                count1 += 1;
            }
            else if (message.Text.IndexOf("上两任") != -1 || message.Text.IndexOf("前两任") != -1)
            {
                count1 += 2;
            }
            else if (message.Text.IndexOf("下一任") != -1 || message.Text.IndexOf("后一任") != -1 || message.Text.IndexOf("后一任") != -1
            || message.Text.IndexOf("下届") != -1 || message.Text.IndexOf("下一届") != -1 || message.Text.IndexOf("后届") != -1)
            {
                count1 -= 1;
            }
            else if (message.Text.IndexOf("下两任") != -1 || message.Text.IndexOf("后两任") != -1)
            {
                count1 -= 2;
            }
            else
            {
                count1 = 0;
            }

            if (message.Text.IndexOf("上一任") != -1 || message.Text.IndexOf("前任") != -1 || message.Text.IndexOf("前一任") != -1
                    || message.Text.IndexOf("上届") != -1 || message.Text.IndexOf("上一届") != -1 || message.Text.IndexOf("前届") != -1)
            {
                count2 += 1;
            }
            else if (message.Text.IndexOf("上两任") != -1 || message.Text.IndexOf("前两任") != -1)
            {
                count2 += 2;
            }
            else if (message.Text.IndexOf("下一任") != -1 || message.Text.IndexOf("后一任") != -1 || message.Text.IndexOf("后一任") != -1
            || message.Text.IndexOf("下届") != -1 || message.Text.IndexOf("下一届") != -1 || message.Text.IndexOf("后届") != -1)
            {
                count2 -= 1;
            }
            else if (message.Text.IndexOf("下两任") != -1 || message.Text.IndexOf("后两任") != -1)
            {
                count2 -= 2;
            }
            else
            {
                count2 = 0;
            }
            count3 = 0;
            EntityRecommendation position;      //职位
            EntityRecommendation college;      //学院
            EntityRecommendation logicalorder; //次序逻辑
            EntityRecommendation schoolrelated; //学校相关           

            var query = "西安交通大学";
            if (result.TryFindEntity(Logicalorder, out logicalorder))
            {
                query += $"{logicalorder.Entity}";
            }
            if (result.TryFindEntity(College, out college))
            {
                query += $"{college.Entity}";
                record2 = $"{college.Entity}";
            }
            if (result.TryFindEntity(Position, out position))
            {
                query += $"{position.Entity}";
                record1 = $"{position.Entity}";
            }           
            if (message.Text.IndexOf(Phone) != -1)
            {
                query += "电话";
            }
            if (message.Text.IndexOf(Email) != -1)
            {
                query += "电子邮箱";
            }
            query += "是？";

            if (result.TryFindEntity(Schoolrelated, out schoolrelated))
            {
                message.Text = message.Text.Replace(schoolrelated.Entity, "西安交通大学");
            }
            else
            {
                message.Text = "西安交通大学" + message.Text;
            }
            if (message.Text.IndexOf("电子邮箱") != -1)
            {
                message.Text = message.Text.Replace("电子邮箱", "电子邮箱");
            }
            else if (message.Text.IndexOf(Email) != -1)
            {
                message.Text = message.Text.Replace(Email, "电子邮箱");
            }
            else if (message.Text.IndexOf("email") != -1)
            {
                message.Text = message.Text.Replace("email", "电子邮箱");
            }
            else if (message.Text.IndexOf("Email") != -1)
            {
                message.Text = message.Text.Replace("Email", "电子邮箱");
            }
            else if (message.Text.IndexOf("联系方式") != -1)
            {
                message.Text = message.Text.Replace("联系方式", "电子邮箱");
            }
            string answer = await QnaMaker.Qna(message.Text);
            if (answer != "No good match found in the KB")
            {
                await context.PostAsync(answer);
            }
            else
            {
                answer = await QnaMaker.Qna(query);
                if (answer != "No good match found in the KB")
                {
                    await context.PostAsync(answer);
                }
                else
                {
                    await context.PostAsync("通过学校官网我查询不到相关信息。");
                }
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("一般疑问句")]
        public async Task Generalquestion(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            count1 = 0;
            count2 = 0;
            count3 = 3;
            EntityRecommendation generaljudgment;
            EntityRecommendation position;
            EntityRecommendation name;
            EntityRecommendation schoolrelated; //学校相关
            EntityRecommendation college;
            EntityRecommendation logicalorder;
            var message = await activity;
            
            if (result.TryFindEntity(Position, out position) && result.TryFindEntity(Name, out name))
            {
                var query = "西安交通大学";
                if (result.TryFindEntity(Logicalorder, out logicalorder))
                {
                    query += $"{logicalorder.Entity}";
                }
                if (result.TryFindEntity(College, out college))
                {
                    query += $"{college.Entity}";
                }
                query += $"{position.Entity}是谁？";
                string answer = await QnaMaker.Qna(query);
                if (answer == name.Entity)
                    await context.PostAsync("是");
                else
                    await context.PostAsync("不是");
            }
            else if(result.TryFindEntity(Position, out position))
            {
                var query = "西安交通大学";
                if (result.TryFindEntity(Logicalorder, out logicalorder))
                {
                    query += $"{logicalorder.Entity}";
                }
                if (result.TryFindEntity(College, out college))
                {
                    query += $"{college.Entity}";
                }
                query += $"{position.Entity}是谁？";
                string answer = await QnaMaker.Qna(query);
                if (message.Text.IndexOf(answer) != -1)
                    await context.PostAsync("是");
                else
                    await context.PostAsync("不是");
            }
            else
            {
                if (result.TryFindEntity(Schoolrelated, out schoolrelated))
                {
                    message.Text = message.Text.Replace(schoolrelated.Entity, "西安交通大学");
                }
                else
                {
                    message.Text = "西安交通大学" + message.Text;
                }
                string answer = await QnaMaker.Qna(message.Text);
                if (answer != "No good match found in the KB")
                {
                    await context.PostAsync(answer);
                }
                else
                {
                    await context.PostAsync("通过学校官网我查询不到相关信息。");
                }
            }
            context.Wait(MessageReceived);
        }

        [LuisIntent("学校概况")]
        public async Task SchoolInformation(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            count1 = 0;
            count2 = 0;
            count3 = 3;
            var message = await activity;
            EntityRecommendation schoolrelated; //学校相关 
            EntityRecommendation objectname;  //概括对象名

            var query = "西安交通大学";
            if (result.TryFindEntity(Objectname, out objectname))
            {
                query += $"{objectname.Entity}";
            }
            if (message.Text.IndexOf("网站") != -1)
            {
                query += "网站是什么？";
            }
            if (message.Text.IndexOf("校训") != -1)
            {
                query += "校训是什么？";
            }

            if (result.TryFindEntity(Schoolrelated, out schoolrelated))
            {
                message.Text = message.Text.Replace(schoolrelated.Entity, "西安交通大学");
            }
            else
            {
                message.Text = "西安交通大学" + message.Text;
            }
            string answer = await QnaMaker.Qna(message.Text);     //Qna问题查询

            if (answer != "No good match found in the KB")
            {
                await context.PostAsync(answer);
            }           
            else
            {
                answer = await QnaMaker.Qna(query);
                if (answer != "No good match found in the KB")
                {
                    await context.PostAsync(answer);
                }
                else
                {
                    await context.PostAsync("通过学校官网我查询不到相关信息。");
                }
            }
        }

        [LuisIntent("时间查询")]
        public async Task TimeQuery(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            count1 = 0;
            count2 = 0;
            count3 = 3;
            EntityRecommendation verb; //学校相关
            EntityRecommendation schoolrelated; //学校相关
            var message = await activity;

            if (result.TryFindEntity(Schoolrelated, out schoolrelated))
            {
                message.Text = message.Text.Replace(schoolrelated.Entity, "西安交通大学");
            }
            else
            {
                message.Text = "西安交通大学" + message.Text;
            }
            string answer = await QnaMaker.Qna(message.Text);     //Qna问题查询

            var query = "西安交通大学";

            if (result.TryFindEntity(Verb, out verb))
            {
                query += $"{verb.Entity}时间？";
            }

            if (answer != "No good match found in the KB")
            {
                await context.PostAsync(answer);
            }
            else
            {
                answer = await QnaMaker.Qna(query);
                if (answer != "No good match found in the KB")
                {
                    await context.PostAsync(answer);
                }
                else
                {
                    await context.PostAsync("通过学校官网我查询不到相关信息。");
                }
            }
        }

        [LuisIntent("逻辑辨识")]
        public async Task LogicalIdentification(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            EntityRecommendation logicalorder;
            EntityRecommendation schoolrelated;
            EntityRecommendation college;
            EntityRecommendation title;
            var message = await activity;
            if (record1 == "校长")
            {
                if (message.Text.IndexOf("上一任") != -1 || message.Text.IndexOf("前任") != -1 || message.Text.IndexOf("前一任") != -1
                    || message.Text.IndexOf("上届") != -1 || message.Text.IndexOf("上一届") != -1 || message.Text.IndexOf("前届") != -1)
                {
                    count1 += 1;
                }
                if (message.Text.IndexOf("上两任") != -1 || message.Text.IndexOf("前两任") != -1)
                {
                    count1 += 2;
                }
                if (message.Text.IndexOf("下一任") != -1 || message.Text.IndexOf("后一任") != -1 || message.Text.IndexOf("后一任") != -1
                || message.Text.IndexOf("下届") != -1 || message.Text.IndexOf("下一届") != -1 || message.Text.IndexOf("后届") != -1)
                {
                    count1 -= 1;
                }
                if (message.Text.IndexOf("下两任") != -1 || message.Text.IndexOf("后两任") != -1)
                {
                    count1 -= 2;
                }
                if (count1 < 0|| message.Text.IndexOf("电话") != -1|| message.Text.IndexOf("邮箱") != -1 || message.Text.IndexOf("办公室") != -1)
                {
                    await context.PostAsync("查不到");
                }
                else
                {
                    await context.PostAsync($"第{15 - count1}任校长是{Principal[count1]}（我校一共有15任校长）");
                }
            }
            else if (record1 == "党委书记")
            {
                if (message.Text.IndexOf("上一任") != -1 || message.Text.IndexOf("前任") != -1 || message.Text.IndexOf("前一任") != -1
                    || message.Text.IndexOf("上届") != -1 || message.Text.IndexOf("上一届") != -1 || message.Text.IndexOf("前届") != -1)
                {
                    count2 += 1;
                }
                if (message.Text.IndexOf("上两任") != -1 || message.Text.IndexOf("前两任") != -1)
                {
                    count2 += 2;
                }
                if (message.Text.IndexOf("下一任") != -1 || message.Text.IndexOf("后一任") != -1 || message.Text.IndexOf("后一任") != -1
                    || message.Text.IndexOf("下届") != -1 || message.Text.IndexOf("下一届") != -1 || message.Text.IndexOf("后届") != -1)
                {
                    count2 -= 1;
                }
                if (message.Text.IndexOf("下两任") != -1 || message.Text.IndexOf("后两任") != -1)
                {
                    count2 -= 2;
                }
                if (count2 < 0 || message.Text.IndexOf("电话") != -1 || message.Text.IndexOf("邮箱") != -1 || message.Text.IndexOf("办公室") != -1)
                {
                    await context.PostAsync("查不到");
                }
                else
                {
                    await context.PostAsync($"第{9 - count2}任党委书记是{Partysecretary[count2]}（我校一共有9任党委书记）");
                }
            }
            else
            {
                if (count3 == 0)
                {
                    var query = "西安交通大学";
                    if (result.TryFindEntity(Logicalorder, out logicalorder))
                    {
                        query += $"{logicalorder.Entity}";
                    }
                    query += record2;   //加上了学院
                    query += record1;   //加上了职位
                    if (message.Text.IndexOf(Phone) != -1)
                    {
                        query += "电话";
                    }
                    if (message.Text.IndexOf("电子邮箱") != -1)
                    {
                        query += "电子邮箱";
                    }
                    else if (message.Text.IndexOf(Email) != -1)
                    {
                        query += "电子邮箱";
                    }
                    else if (message.Text.IndexOf("email") != -1)
                    {
                        query += "电子邮箱";
                    }
                    else if (message.Text.IndexOf("Email") != -1)
                    {
                        query += "电子邮箱";
                    }
                    else if (message.Text.IndexOf("联系方式") != -1)
                    {
                        query += "电子邮箱";
                    }
                    query += "是？";
                    var answer = await QnaMaker.Qna(query);
                    if (answer != "No good match found in the KB")
                    {
                        await context.PostAsync(answer);
                    }
                    else
                    {
                        await context.PostAsync("通过学校官网我查询不到相关信息。");
                    }
                }
                else if (count3 == 1)
                {
                    if (result.TryFindEntity(College, out college))
                    {
                        message.Text.Replace(record2, college.Entity);
                        var answer = await QnaMaker.Qna(message.Text);
                        if (answer != "No good match found in the KB")
                        {
                            await context.PostAsync(answer);
                        }
                        else
                        {
                            await context.PostAsync("通过学校官网我查询不到相关信息。");
                        }
                    }
                    else if (result.TryFindEntity(Title, out title))
                    {
                        message.Text.Replace(record3, title.Entity);
                        var answer = await QnaMaker.Qna(message.Text);
                        if (answer != "No good match found in the KB")
                        {
                            await context.PostAsync(answer);
                        }
                        else
                        {
                            await context.PostAsync("通过学校官网我查询不到相关信息。");
                        }
                    }
                    else
                    {
                        if (result.TryFindEntity(Schoolrelated, out schoolrelated))
                        {
                            message.Text = message.Text.Replace(schoolrelated.Entity, "西安交通大学");
                        }
                        else
                        {
                            message.Text = "西安交通大学" + message.Text;
                        }
                        var answer = await QnaMaker.Qna(message.Text);
                        if (answer != "No good match found in the KB")
                        {
                            await context.PostAsync(answer);
                        }
                        else
                        {
                            await context.PostAsync("通过学校官网我查询不到相关信息。");
                        }
                    }
                }
                else
                {
                    if (result.TryFindEntity(Schoolrelated, out schoolrelated))
                    {
                        message.Text = message.Text.Replace(schoolrelated.Entity, "西安交通大学");
                    }
                    else
                    {
                        message.Text = "西安交通大学" + message.Text;
                    }
                    var answer = await QnaMaker.Qna(message.Text);
                    if (answer != "No good match found in the KB")
                    {
                        await context.PostAsync(answer);
                    }
                    else
                    {
                        await context.PostAsync("通过学校官网我查询不到相关信息。");
                    }
                }
            }
        }

        [LuisIntent("数量信息查询")]
        public async Task QuantityQuery(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {          
            count1 = 0;
            count2 = 0;
            count3 = 1;
            EntityRecommendation title;      //职位
            EntityRecommendation college;      
            EntityRecommendation schoolrelated; //学校相关
            var message = await activity;
            if (message.Text.IndexOf("占") != -1)
            {
                if (message.Text.IndexOf("本科生") != -1)
                {
                    await context.PostAsync("17099/33604=50.9%");
                }
                if (message.Text.IndexOf("研究生") != -1)
                {
                    await context.PostAsync("16505/33604=49.1%");
                }
            }
            else
            {
                if (result.TryFindEntity(Schoolrelated, out schoolrelated))
                {
                    message.Text = message.Text.Replace(schoolrelated.Entity, "西安交通大学");
                }
                else
                {
                    message.Text = "西安交通大学" + message.Text;
                }
                string answer = await QnaMaker.Qna(message.Text);     //Qna问题查询
                if (result.TryFindEntity(College, out college))
                {
                    record2 = $"{college.Entity}";
                }
                var query = "西安交通大学有多少";

                if (result.TryFindEntity(Title, out title))
                {
                    query += $"{title.Entity}？";
                    record3 = $"{title.Entity}";
                }

                if (answer != "No good match found in the KB")
                {
                    await context.PostAsync(answer);
                }
                else
                {
                    answer = await QnaMaker.Qna(query);
                    if (answer != "No good match found in the KB")
                    {
                        await context.PostAsync(answer);
                    }
                    else
                    {
                        await context.PostAsync("通过学校官网我查询不到相关信息。");
                    }
                }
            }
        }
    }
}
            
