using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using BuildABot.Core.MessageHandlers;




namespace ClassLibrary1 
{
    [Export(typeof(MessageHandler))]
    public class CardBalanceHandler : MessageHandler
    {
        string cardNumber;
        string cardBalance;
        string pin;
        string problemDescription;

        public CardBalanceHandler()
            : base("help")
        {
        }

        protected override StateHandler InitialStateHandler
        {
            get { return AskForCardNumber; }
        }

        public Reply AskForCardNumber(Message message)
        {
            Reply reply = new Reply("Please speak or enter your card number.");
            this.nextStateHandler = AskForPIN;
            return reply;
        }

        public Reply AskForPIN(Message cardMessage)
        {
            Reply reply = new Reply("Please speak or enter your pin");
            // Hard coded valid Card Number for now
            //if (cardMessage.Content.Contains("1234567890"))
            //  {
            //  this.cardNumber = cardMessage.Content.ToString();
            this.nextStateHandler = GetCardInfo;
            this.cardNumber = cardMessage.Content.ToString(); ;
            // this.cardNumber = this.CreateNewTicket();
            //   reply.Add("This is your new card balance, " + this.cardBalance);
            // reply.Add("This is your new card number: " + this.cardNumber);
            //   reply.Add("Current time is: " + DateTime.Now.ToShortTimeString());
            //   this.nextStateHandler = this.GetUserIssue;
            // }
            // else
            //{
            //   reply.Add(cardMessage + " is an invalid card number");
            //   this.nextStateHandler = this.AskForCardNumber;
            // }
            return reply;
        }

        public Reply GetCardInfo(Message pinMessage)
        {
            Reply reply = new Reply();
            // Call webservice with card number and PIN
            // if (pinMessage.Content.Contains("1234")) {
            this.pin = pinMessage.Content.ToString();

            GitHubService service = new GitHubService(this.cardNumber, this.pin);


            string balance = service.getBalance();

            if (balance == null)
            {
                reply.Add("");
                this.nextStateHandler = AskForCardNumber;
            }
            else
            {
                reply.Add("You're card balance is: " + balance);
                this.Done = true;
            }

            return reply;
        }



    }
}
