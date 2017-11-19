﻿using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServer.Core
{
    class POP3
    {
        public static void Fetch(string hostname, int port, bool useSsl, string username, string password, List<string> seenUids)
        {
            // Create a list we can return with all new messages
            List<Message> newMessages = new List<Message>();

            // The client disconnects from the server when being disposed
            using (Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(hostname, port, useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(username, password);

                // Fetch all the current uids seen
                List<string> uids = client.GetMessageUids();

                // All the new messages not seen by the POP3 client
                for (int i = 0; i < uids.Count; i++)
                {
                    string currentUidOnServer = uids[i];
                    if (!seenUids.Contains(currentUidOnServer))
                    {
                        // We have not seen this message before.
                        // Download it and add this new uid to seen uids

                        // the uids list is in messageNumber order - meaning that the first
                        // uid in the list has messageNumber of 1, and the second has 
                        // messageNumber 2. Therefore we can fetch the message using
                        // i + 1 since messageNumber should be in range [1, messageCount]
                        Message unseenMessage = client.GetMessage(i + 1);

                        // Add the message to the new messages
                        newMessages.Add(unseenMessage);

                        // Add the uid to the seen uids, as it has now been seen
                        seenUids.Add(currentUidOnServer);
                    }
                }

                // Return our new found messages
                //return newMessages;

                //SaveAllNewMessages
            }

            foreach (Message mes in newMessages)
            {
                string body = GetBody(mes);
                if (IsValidPhoneNumber(mes.Headers.Subject, body))
                {
                    Database.SaveMessage("12345", mes.Headers.Subject, body, mes.Headers.From.MailAddress.Address, mes.Headers.DateSent);
                }
            }
        }


        public static bool IsValidPhoneNumber(string subject, string body)
        {
            return true;
        }

        private static string GetBody(Message message)
        {
            StringBuilder builder = new StringBuilder();
            OpenPop.Mime.MessagePart plainText = message.FindFirstPlainTextVersion();
            if (plainText != null)
            {
                // We found some plaintext!
                builder.Append(plainText.GetBodyAsText());
            }
            else
            {
                // Might include a part holding html instead
                OpenPop.Mime.MessagePart html = message.FindFirstHtmlVersion();
                if (html != null)
                {
                    // We found some html!
                    builder.Append(html.GetBodyAsText());
                }
            }

            return builder.ToString();
        }
    }
}