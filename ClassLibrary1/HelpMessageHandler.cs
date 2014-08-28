using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using BuildABot.Core.MessageHandlers;

namespace ClassLibrary1 
{
    [Export(typeof(MessageHandler))]
    public class HelpMessageHandler : MessageHandler
    {
        string ticketNumber;
        string problemDescription;

        public HelpMessageHandler()
            : base("help")
        {
        }

        protected override StateHandler InitialStateHandler
        {
            get { return AskForTicketStatus; }
        }

        public Reply AskForTicketStatus(Message message)
        {
            Reply reply = new Reply("Is this a new or an already opened ticket?");
            this.nextStateHandler = HandleTicketStatus;
            return reply;
        }

        public Reply HandleTicketStatus(Message message)
        {
            Reply reply = new Reply();
            if (message.Content.Contains("new"))
            {
                this.ticketNumber = this.CreateNewTicket();
                reply.Add("This is your new ticket number: " + this.ticketNumber);
                reply.Add("What's your problem?");
                this.nextStateHandler = this.GetUserIssue;
            }
            else
            {
                reply.Add("What's the ticket number?");
                this.nextStateHandler = this.GetTicketNumber;
            }
            return reply;
        }

        public Reply GetTicketNumber(Message message)
        {
            this.ticketNumber = message.Content;
            this.nextStateHandler = this.GetUserIssue;
            return new Reply("What's the update on your problem?");
        }

        public Reply GetUserIssue(Message message)
        {
            this.problemDescription = message.Content;
            string solution = this.GetSolution(problemDescription);
            this.nextStateHandler = this.GetSolutionFeedback;
            return new Reply("Will this work: " + solution);
        }

        public Reply GetSolutionFeedback(Message message)
        {
            Reply reply = new Reply();
            if (message.Content.Contains("yes"))
            {
                reply.Add("Great, good to know");
                this.Done = true;
            }
            else
            {
                string solution = this.GetAlternativeSolution(problemDescription);
                reply.Add("Sorry to hear that. What about this, will this work: " + solution);
                this.nextStateHandler = this.GetAlternativeSolutionFeedback;
            }
            return reply;
        }

        public Reply GetAlternativeSolutionFeedback(Message message)
        {
            Reply reply = new Reply();
            if (message.Content.Contains("yes"))
            {
                reply.Add("Great, good to know");
            }
            else
            {
                reply.Add("Sorry, I'll escalate this for you.");
                // escalation code...
            }
            this.Done = true;
            return reply;
        }

        private string CreateNewTicket()
        {
            return Guid.NewGuid().ToString();
        }

        private string GetSolution(string problemDescription)
        {
            return "http://www.letmebingthatforyou.com/?q=" + problemDescription;
        }

        private string GetAlternativeSolution(string problemDescription)
        {
            return "Ask your manager about " + problemDescription;
        }
    }
}
