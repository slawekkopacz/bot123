using Microsoft.Bot.Builder.Dialogs;
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
            var interviewCommand = new InterviewCommand();
            await context.Forward(new InterviewDialog(), ResumeAfterInterviewDialog, interviewCommand, System.Threading.CancellationToken.None);
        }

        private async Task ResumeAfterInterviewDialog(IDialogContext context, IAwaitable<object> result)
        {
            var response = await result;
            await context.PostAsync($"Interview dialog result: {response}");
            context.Done(new object());
        }
    }
}