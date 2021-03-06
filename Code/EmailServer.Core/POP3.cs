﻿using OpenPop.Mime;
using OpenPop.Pop3;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Data;

namespace EmailServer.Core
{
    public class POP3
    {
        public static void Fetch(string hostname, int port, bool useSsl, string username, string password/*, List<string> seenUids*/)
        {
            List<string> seenUids = new List<string>();
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
                string phone_number = string.Empty;
                if (IsValidPhoneNumber(mes.Headers.Subject, body, out phone_number) || IsInSafeList(mes.Headers.Subject, body, out phone_number, mes.Headers.From.MailAddress.Address))
                {
                    long id = Database.SaveMessage(phone_number, mes.Headers.Subject, body, mes.Headers.From.MailAddress.Address, mes.Headers.DateSent);

                    foreach (MessagePart att in mes.FindAllAttachments())
                    {
                        Database.SaveAttachment(id, att.Body, att.FileName);
                    }
                }
                else
                {
                    SMTP.SendBadResponse(mes.Headers.From.MailAddress.Address);
                }
            }
        }

        private static bool IsInSafeList(string subject, string body, out string phone_number, string sender_mail)
        {
            bool exists = false;
            phone_number = string.Empty;

            DataTable dt = Database.IsAlreadyInSafeList(sender_mail);
            exists = Convert.ToBoolean(dt.Rows[0]["exists"]);

            //It's already in a safe list
            if (exists)
            {
                phone_number = dt.Rows[0]["p_phone_number"].ToString();
                return true;
            }

            //Check all tokens
            dt = Database.GetSafeList(sender_mail);

            foreach (DataRow row in dt.Rows)
            {
                string token = string.Format("#{0}#", row["token"].ToString());
                if (subject.Contains(token) || body.Contains(token))
                {
                    phone_number = row["phone_number"].ToString();
                    Database.ActivateSafeList(phone_number, sender_mail);
                    return true;
                }
            }

            return false;
        }

        public static bool IsValidPhoneNumber(string subject, string body, out string phone_number)
        {
            string text = subject + "\n" + body;

            const string MatchPhonePattern = //@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
            @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})";

            Regex rx = new Regex(MatchPhonePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Find matches.
            MatchCollection matches = rx.Matches(text);

            foreach (Match match in matches)
            {
                string tempPhoneNumber = match.Value.ToString().Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(".", string.Empty).Replace("-", string.Empty);

                if (Database.IsValidPhoneNumber(tempPhoneNumber))
                {
                    phone_number = tempPhoneNumber;
                    return true;
                }
            }

            phone_number = string.Empty;
            return false;
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
