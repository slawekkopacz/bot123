using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace BotApp
{
    [Serializable]
    public class InterviewDialog : IDialog<InterviewCommand>
    {
        private InterviewCommand command;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait<InterviewCommand>(this.CommandReceived);
        }

        private async Task CommandReceived(IDialogContext context, IAwaitable<InterviewCommand> result)
        {
            command = await result;

            //context.Done(175);
            await this.ProceedInterview(context);
        }

        private async Task ProceedInterview(IDialogContext context)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                PromptDialog.Text(context, Name, "What's your name?");
                return;
            }

            context.Done(command);
        }

        private async Task Name(IDialogContext context, IAwaitable<string> result)
        {
            var name = await result;
            command.Name = name;
            await ProceedInterview(context);
        }
    }
}