using System;
using System.Net;

namespace P3D.Legacy.Launcher.Controls
{
    internal class TimeoutWebClient : WebClient
    {
        public int Timeout { get; set; } = 86400;

        protected override WebRequest GetWebRequest(Uri address)
        {
            var objWebrequest = base.GetWebRequest(address);
            objWebrequest.Timeout = Timeout;
            return objWebrequest;
        }
    }
}
