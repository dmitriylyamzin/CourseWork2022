using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Clases
{
    public class VKBot
    {
        public static void Main()
        { 
        }
        public static string Authorization(string message)
        {
            string result = "";
            var api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                AccessToken = "ac31d6ff96366bb055e83e3514c3024c3b8df54794fa0f86d662d8fe887df2ea84acadffc24f13bbe5399"
            });
            api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
            {
                RandomId = new Random().Next(1, 1000000),
                PeerId = 304159800,
                Message = message
            });
            Thread.Sleep(1000);
            var res = api.Messages.GetHistory(new MessagesGetHistoryParams { UserId = 304159800 }).Messages.GetEnumerator();
            if (res.MoveNext())
            {
                result = res.Current.Text;
            }

            Console.WriteLine(result);


            return result;
        }
    }
}
