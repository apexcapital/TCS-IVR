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
    class TimeMessageHandler : MessageHandler
    {
        public TimeMessageHandler()
            :base("time","Please wait while I get the current time...", true)
{
}
        public override MessageHandlingResponse CanHandle(Message message)
        {
            MessageHandlingResponse response = new MessageHandlingResponse();
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday
                || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                response.Confidence = 0;
            }
            else
            {
                // We could have implemented more complex/specific
                // logic for handling the message here.
                response = base.CanHandle(message);
            }

            return response;
        }

    }
}
