// <copyright file="MessageReceiverService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApplicationBussinessLayer.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Experimental.System.Messaging;

    public class MessageReceiverService
    {
        /// <summary>
        /// Method to fetch message from MSMQ.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void ReceiveFromQueue()
        {
            MessageQueue MyQueue = null;
            try
            {
                MyQueue = new MessageQueue(@".\Private$\parkingbillsQueue");
                Message[] messages = MyQueue.GetAllMessages();
                if (messages.Length > 0)
                {
                    foreach (Message m in messages)
                    {
                        m.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                        string message = m.Body.ToString();
                        MyQueue.Receive();
                        using StreamWriter file = new StreamWriter(@"C:\Users\User\source\ParkingRecords.txt", true);
                        file.WriteLine(message);
                    }
                }
                else
                {
                    Console.WriteLine("No New Messages in Message Queue");
                }

                MyQueue.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                MyQueue.Close();
            }
        }
    }
}