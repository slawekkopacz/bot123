namespace BotApp
{
    public class InterviewResult
    {
        public bool IsElligibleCredit { get; set; }
        public double? MaxCreditValue { get; set; }

        public string DisplayMessage()
        {
            if (IsElligibleCredit)
            {
                return $"Your are elligible for credit in max value of {MaxCreditValue}.";
            }

            return $"You are not elligible for credit.";
        }
    }
}