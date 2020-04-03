using model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAR_AMI_LIB
{
    public class TokenController
    {
        public string qSuccsess(string token) {
            string q = "";
            model.Ami_Token ami_Token = null;
            ami_Token = new model.Ami_Token();
            ami_Token.status = 3;//Succsess 
            ami_Token.token = token;
            q = convertToUpdateQuery(ami_Token);
            return q;
        }
        public string qCancell(string token)
        {
            string q = "";
            model.Ami_Token ami_Token = null;
            ami_Token = new model.Ami_Token();
            ami_Token.status = 4;//Cancell 
            ami_Token.token = token;
            q = convertToUpdateQuery(ami_Token);
            return q;
        }
        public string qFailure(string token)
        {
            string q = "";
            model.Ami_Token ami_Token = null;
            ami_Token = new model.Ami_Token();
            ami_Token.status = 5;//Failure 
            ami_Token.token = token;
            q = convertToUpdateQuery(ami_Token);
            return q;
        }
        public bool update(model.Ami_Token ami_Token)
        {
            bool r = false;           
            r = ami_Token.updateStatusByToken();
            return r;
        }

        public string convertToUpdateQuery(Ami_Token ami_Token) { 
            string q =
                "UPDATE AMI.dbo.Ami_Token " +
                "SET status = " + ami_Token.status + " where token = '" + ami_Token.token + "' ; ";
            return q;
        }
        public bool insert(dynamic resulToken,string meter,string tokenType)
        { 
            dynamic dyObj = null;
            var json = "";
            bool g = true;
            bool r = false;
            var param = resulToken;

            if (param is Ami_Diagnostic_Service.RequestToken)
            {

                Ami_Diagnostic_Service.RequestToken obj = new Ami_Diagnostic_Service.RequestToken();
                obj = param;
                dyObj = obj;
            }
            else if (param is Ami_Control_Service.RequestToken)
            {

                Ami_Control_Service.RequestToken obj = new Ami_Control_Service.RequestToken();
                obj = param;
                dyObj = obj;
            }
            else if (param is Ami_Data_Service.RequestToken)
            {
                Ami_Data_Service.RequestToken obj = new Ami_Data_Service.RequestToken();
                obj = param;
                dyObj = obj;
            }
            else {
                g = false;
            }
            if (g)
            {
                 
                model.Ami_Token ami_Token = new model.Ami_Token();
                ami_Token.token = dyObj.Id.ToString();
                ami_Token.status = 0;
                ami_Token.meter = meter;
                ami_Token.tokenType = tokenType;
                ami_Token.period = 1;
                r = ami_Token.insert();
            }
            return r;
        }
    }
}
