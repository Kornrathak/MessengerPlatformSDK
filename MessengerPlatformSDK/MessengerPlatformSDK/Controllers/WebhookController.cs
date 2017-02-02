using FacebookMessengerSDK.Received;
using FacebookMessengerSDK.Sended;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MessengerChatAPI.Controllers
{
    public class WebhookController : ApiController
    {
        //GET api/<controller>
        public HttpResponseMessage Get ()
        {
            HttpResponseMessage response;
            string validation_token = ConfigurationManager.AppSettings["ValidationToken"].ToString();
            if (String.IsNullOrEmpty(validation_token))
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            string header = Request.Headers.GetValues("X-Original-URL").FirstOrDefault();
            if (!(header.IndexOf("hub.mode") > -1 && header.IndexOf("subscribe") > -1))
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            if (!(header.IndexOf(validation_token) > -1))
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            string challenge = header.Substring(header.IndexOf("hub.challenge"));
            challenge = challenge.Substring(0, challenge.IndexOf('&'));
            challenge = challenge.Substring(challenge.IndexOf('=') + 1);

            response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(challenge);

            return response;
        }

        public async Task<HttpResponseMessage> Post (HttpRequestMessage request)
        {
            CommonFormatCallBack data = JsonConvert.DeserializeObject<CommonFormatCallBack>(await request.Content.ReadAsStringAsync());

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            if (data.Object.Equals("page"))
            {
                foreach (Entry pageEntry in data.Entries)
                {
                    var pageID = pageEntry.Id;
                    var timeOfEvent = pageEntry.Time;

                    foreach (Messaging messagingEvent in pageEntry.Messaginges)
                    {
                        try
                        {
                            response = await receivedMessage(messagingEvent);
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.StackTrace);
                            throw;
                        }
                    }
                }

                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadGateway);
            }


            return response;
        }

        /**
         * Message Event
         * This event is called when a message is sent to your page. The 'message' 
         * object format can vary depending on the kind of message that was received.
         * Read more at https://developers.facebook.com/docs/messenger-platform/webhook-reference/message-received
         * 
         * For this example, we're going to echo any text that we get. If we get some 
         * special keywords ('button', 'generic', 'receipt'), then we'll send back
         * examples of those bubbles to illustrate the special message bubbles we've 
         * created. If we receive a message with an attachment (image, video, audio), 
         * then we'll simply confirm that we've received the attachment.
         **/
        private async Task<HttpResponseMessage> receivedMessage(Messaging e)
        {
            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK); ;
            var senderID = e.Sender.Id;
            var recipientID = e.Recipient.Id;
            var timeOfMessage = e.Timestamp;
            var message = e.Message;

            var isEcho = message.Is_Echo;
            var messageId = message.Mid;
            var appId = message.App_Id;
            var metadata = message.Metadata;

            // You may get a text or attachment but not both
            var messageText = message.Text;
            var messageAttachments = message.Attachments;
            var quickReply = message.Quick_Reply;

            //if (isEcho)
            //{
            //    // Just logging message echoes to console
            //    //return result ;
            //}
            //else if (quickReply != null)
            //{
            //    var quickReplyPayload = quickReply.Payload;
            //    //sendTextMessage(senderID, "Quick reply tapped");
            //    //return result;
            //}

            if (!String.IsNullOrEmpty(messageText))
            {
                result = await sendTextMessage(senderID, messageText);
            }
            else if (messageAttachments != null)
            {
                //sendTextMessage(senderID, "Message with attachment received");
            }

            return result;
        }

        private async Task<HttpResponseMessage> sendTypingOff(string recipientId)
        {
            MessageAction messageData = new MessageAction();
            var recip = new Recipient();
            recip.Id = recipientId;
            messageData.Recipient = recip;
            messageData.Sender_action = "typing_off";

            var result = await callSendAPI(messageData);
            return result;
        }

        private async Task<HttpResponseMessage> sendTypingOn(string recipientId)
        {
            MessageAction messageData = new MessageAction();
            var recip = new Recipient();
            recip.Id = recipientId;
            messageData.Recipient = recip;
            messageData.Sender_action = "typing_on";

            var result = await callSendAPI(messageData);
            return result;
        }

        private async Task<HttpResponseMessage> sendReadReceipt(string recipientId)
        {
            MessageAction messageData = new MessageAction();
            var recip = new Recipient();
            recip.Id = recipientId;
            messageData.Recipient = recip;
            messageData.Sender_action = "mark_seen";

            var result = await callSendAPI(messageData);
            return result;
        }

        private async Task<HttpResponseMessage> sendTextMessage(string recipientId, string messageText)
        {
            MessageData messageData = new MessageData();
            var recip = new Recipient();
            recip.Id = recipientId;
            messageData.Recipient = recip;

            var message = new MessageText();
            message.Text = messageText;
            messageData.Message = message;

            var result = await callSendAPI(messageData);
            return result;
        }

        /**
        //private async Task<HttpResponseMessage> sendFileMessage(string recipientId)
        //{
        //    MessageData messageData = new MessageData();
        //    messageData.Recipient.Id = recipientId;
        //    messageData.Message.Attachments.Type = "file";
        //    messageData.Message.Attachments.Payload.Url = ConfigurationManager.AppSettings["ServerUrl"].ToString();

        //    var result = await callSendAPI(messageData);
        //    return result;
        //}

        //private async Task<HttpResponseMessage> sendVideoMessage(string recipientId)
        //{
        //    MessageData messageData = new MessageData();
        //    messageData.Recipient.Id = recipientId;
        //    messageData.Message.Attachments.Type = "video";
        //    messageData.Message.Attachments.Payload.Url = ConfigurationManager.AppSettings["ServerUrl"].ToString();

        //    var result = await callSendAPI(messageData);
        //    return result;
        //}

        //private async Task<HttpResponseMessage> sendAudioMessage(string recipientId)
        //{
        //    MessageData messageData = new MessageData();
        //    messageData.Recipient.Id = recipientId;
        //    messageData.Message.Attachments.Type = "audio";
        //    messageData.Message.Attachments.Payload.Url = ConfigurationManager.AppSettings["ServerUrl"].ToString();

        //    var result = await callSendAPI(messageData);
        //    return result;
        //}

        //private async Task<HttpResponseMessage> sendImageMessage(string recipientId)
        //{
        //    MessageData messageData = new MessageData();
        //    messageData.Recipient.Id = recipientId;
        //    messageData.Message.Attachments.Type = "image";
        //    messageData.Message.Attachments.Payload.Url = ConfigurationManager.AppSettings["ServerUrl"].ToString();

        //    var result = await callSendAPI(messageData);
        //    return result;
        //}
        **/
        const string endpoint = "https://graph.facebook.com/v2.6/me/messages";
        /**
         * Call the Send API. The message data goes in the body. If successful, we'll 
         * get the message id in a response 
         **/
        private async Task<HttpResponseMessage> callSendAPI(Object messageData)
        {
            string path = endpoint + "?access_token=" + ConfigurationManager.AppSettings["PageAccessToken"].ToString();

            using (HttpClient client = new HttpClient())
            {
                var stringMessage = JsonConvert.SerializeObject(messageData);

                StringContent content = new StringContent(stringMessage, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(new Uri(path), content);

                if (!result.IsSuccessStatusCode)
                    throw new Exception(await result.Content.ReadAsStringAsync());
                return result;
            }
        }
    }
}
