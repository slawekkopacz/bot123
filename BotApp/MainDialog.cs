﻿using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace BotApp
{
    [Serializable]
    public class MainDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceived);
        }

        private async Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            //todo[sk]: I'd expact to get LuisResult from some Luis Dialog. I'd extract entities form LuisResult and fill InterviewCommand.
            //InterviewDialog will prompt for missing values and return result.
            var interviewCommand = new InterviewCommand();
            await context.Forward(new InterviewDialog(), ResumeAfterInterviewDialog, interviewCommand, System.Threading.CancellationToken.None);
        }

        private async Task ResumeAfterInterviewDialog(IDialogContext context, IAwaitable<InterviewResult> result)
        {
            var response = await result;
            await context.PostAsync(response.DisplayMessage());
            context.Done(new object());
        }
    }
}