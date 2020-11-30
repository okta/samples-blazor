using System;
using System.Collections.Generic;

namespace okta_blazor_web_assembly_example
{
    public class ServerMessage
    {
        public DateTime Date { get; set; }

        public string Text { get; set; }
    }

    public class ServerMessages
    {
        public IList<ServerMessage> Messages { get; set; }
    }
}
