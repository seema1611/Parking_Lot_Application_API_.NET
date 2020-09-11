// <copyright file="MessageSenderService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApplicationBussinessLayer.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Experimental.System.Messaging;

    public class MessageSenderService
    {/// <summary>
     /// Method to add message to MSMQ.
     /// </summary>
     /// <param name="message"></param>
     /// <returns></returns>
        public static void AddMessageToQueue(string message)
        {
            MessageQueue MyQueue = null;
            try
            {
                if (MessageQueue.Exists(@".\Private$\parkingbillsQueue"))
                {
                    MyQueue = new MessageQueue(@".\Private$\parkingbillsQueue");
                }
                else
                {
                    MyQueue = MessageQueue.Create(@".\Private$\parkingbillsQueue");
                }

                MyQueue.Label = "This is the ParkinglotMesaageQue";
                MyQueue.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                MyQueue.Dispose();
            }
        }
    }
}