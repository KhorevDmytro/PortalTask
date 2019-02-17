using System;
using System.Net.Http;

namespace PortalTask.Utils.Extensions
{
    public static class HttpCodesExtension
    {
        public static HttpResponseMessage VerifyCode(this HttpResponseMessage context)
        {
            if (!context.IsSuccessStatusCode)
            {
                throw new Exception($"Not Success http code for {context.RequestMessage.RequestUri}: {context.StatusCode}");
            }

            return context;
        }
    }
}