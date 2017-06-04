using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace BotApp
{
    [Serializable]
    public class InterviewDialog : IDialog<InterviewResult>
    {
        private InterviewCommand command;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait<InterviewCommand>(this.CommandReceived);
        }

        private async Task CommandReceived(IDialogContext context, IAwaitable<InterviewCommand> result)
        {
            command = await result;

            await this.ProceedInterview(context);
        }

        private async Task ProceedInterview(IDialogContext context)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                PromptDialog.Text(context, async (ctx, result) =>
                {
                    var name = await result;
                    command.Name = name;
                    await ProceedInterview(ctx);
                },
                "What's your name?");
                return;
            }

            if (! command.Age.HasValue)
            {
                PromptDialog.Number(context, async delegate (IDialogContext ctx, IAwaitable<double> result)
                {
                    var age = await result;
                    command.Age = age;
                    await ProceedInterview(ctx);
                },
                "What's your age?");
                return;
            }

            if (! command.Education.HasValue)
            {
                PromptDialog.Choice<Education>(context, async (ctx, result) =>
                {
                    var education = await result;
                    command.Education = education;
                    await ProceedInterview(ctx);
                },
                (Education[])Enum.GetValues(typeof(Education)),
                "What's your education level?");
                return;
            }

            if (! command.MonthlyIncome.HasValue)
            {
                PromptDialog.Number(context, async delegate (IDialogContext ctx, IAwaitable<double> result)
                {
                    var income = await result;
                    command.MonthlyIncome = income;
                    await ProceedInterview(ctx);
                },
                "What's your monthly gross income?");
                return;
            }

            if (! command.HasLeasing.HasValue)
            {
                PromptDialog.Confirm(context, async (ctx, result) =>
                {
                    var response = await result;
                    command.HasLeasing = response;
                    await ProceedInterview(ctx);
                },
                "Do you already have a leasing or pay installments?");
                return;
            }

            if (command.HasLeasing.Value && ! command.LeasingValue.HasValue)
            {
                PromptDialog.Number(context, async delegate (IDialogContext ctx, IAwaitable<double> result)
                {
                    var leasingValue = await result;
                    command.LeasingValue = leasingValue;
                    await ProceedInterview(ctx);
                },
                "What's the monthly installment?");
                return;
            }

            //todo[sk]: algorithm
            InterviewResult interviewResult = null;
            if (command.Age > 22 && command.MonthlyIncome > 2000)
            {
                interviewResult = new InterviewResult
                {
                    IsElligibleCredit = true,
                    MaxCreditValue = 25000,
                };
            }
            else
            {
                interviewResult = new InterviewResult
                {
                    IsElligibleCredit = false,
                };
            }

            context.Done(interviewResult);
        }
    }
}