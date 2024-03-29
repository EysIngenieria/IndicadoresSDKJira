﻿using MQTT.Infrastructure.DAL;
using MQTT.Infrastructure.Models.DTO;
using System;

namespace MQTT.Subscriber.BL
{
    public class SubscriberBL
    {

        private readonly string _connectionString = AppSettings.Instance.Configuration["connectionString"].ToString();
        private readonly string _identifierField = AppSettings.Instance.Configuration["appSettings:identifierField"].ToString();

        private General _objGeneral;
        public General DBAccess { get => _objGeneral; set => _objGeneral = value; }

        public SubscriberBL()
        {
            DBAccess = new General(_connectionString);
        }
        public void ProccessMessageIn(string message)
        {
            try
            {
                LogMessageDTO log = AddLogMessageIn(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private LogMessageDTO AddLogMessageIn(string message)
        {
            try
            {
                return LogMessagesDAL.Add(DBAccess, message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUser(string user, string pwd)
        {
            try
            {
                var userValue = ParametersDAL.GetValue(DBAccess, "user");
                var pwdValue = ParametersDAL.GetValue(DBAccess, "password");

                if (user.ToUpper().Equals(userValue.ToUpper()) && pwd.Equals(pwdValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long AddLogExecution()
        {
            try
            {
                return LogExecutionDAL.Add(DBAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLogExecution(long id, string observations)
        {
            try
            {
                LogExecutionDAL.Update(DBAccess, id, observations);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
