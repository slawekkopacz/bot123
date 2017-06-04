using System;

namespace BotApp
{
    internal enum Education
    {
        Primary,
        Seconadary,
        Higher,
    }

    [Serializable]
    internal class InterviewCommand
    {
        public string Name { get; set; }
        public double Age { get; set; }
        public Education Education { get; set; }
        public double MonthlyIncome { get; set; }
        public bool HasLeasing { get; set; }
        public double LeasingValue { get; set; }
        public double Result { get; set; }

        public override string ToString()
        {
            return $@"{nameof(Name)}: {Name}
{nameof(Age)}: {Age}
{nameof(Education)}: {Education}
{nameof(MonthlyIncome)}: {MonthlyIncome}
{nameof(HasLeasing)}: {HasLeasing}
{nameof(LeasingValue)}: {LeasingValue}
{nameof(Result)}: {Result}";
        }
    }
}