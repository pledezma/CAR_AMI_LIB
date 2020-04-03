using model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAR_AMI_LIB
{
    public class EndpointFailure
    {
        public List<Ami_Token_Failure> convertFailure(dynamic endpointFailureResult, string token)
        {
            Ami_Token_Failure ami_Token_Failure = null;
            List<Ami_Token_Failure> list_Ami_Token_Failure = null;
            dynamic dyObj = null;
            var json = "";
            bool n = false;
            var param = endpointFailureResult;

            if (param is Ami_Diagnostic_Service.EndpointFailureResult)
            {
                n = true;
                Ami_Diagnostic_Service.EndpointFailureResult obj = new Ami_Diagnostic_Service.EndpointFailureResult();
                obj = param;
                dyObj = obj;
            } 
            else if (param is Ami_Control_Service.EndpointFailureResult)
            {
                n = true;
                Ami_Control_Service.EndpointFailureResult obj = new Ami_Control_Service.EndpointFailureResult();
                obj = param;
                dyObj = obj;
            }
            else if (param is Ami_Control_Service.EndpointFailure)
            {
                Ami_Control_Service.EndpointFailure obj = new Ami_Control_Service.EndpointFailure();
                obj = param;
                dyObj = obj;
            }
            else if (param is Ami_Data_Service.EndpointFailureResult)
            {
                n = true;
                Ami_Data_Service.EndpointFailureResult obj = new Ami_Data_Service.EndpointFailureResult();
                obj = param;
                dyObj = obj;
            }  
            else if (param is Ami_Data_Service.EndpointFailure)
            {                 
                Ami_Data_Service.EndpointFailure obj = new Ami_Data_Service.EndpointFailure();
                obj = param;
                dyObj = obj;
            }
            
             
            if (dyObj != null)
            {
                list_Ami_Token_Failure = new List<Ami_Token_Failure>();

                json = JsonConvert.SerializeObject(dyObj);
                json = help.Base64Encode(json);
                if (n)
                { 
                    for (int i = 0; i < dyObj.Failures.Length; i++)
                    {
                        ami_Token_Failure = new Ami_Token_Failure();
                        ami_Token_Failure.token = token;
                        ami_Token_Failure.reason = dyObj.Failures[i].FailureReason.ToString();
                        ami_Token_Failure.description = dyObj.Failures[i].Description.ToString();
                        ami_Token_Failure.jsonData = json;

                        list_Ami_Token_Failure.Add(ami_Token_Failure);
                    }
                }
                else {
                    ami_Token_Failure = new Ami_Token_Failure();
                    ami_Token_Failure.token = token;
                    ami_Token_Failure.reason = dyObj.FailureReason.ToString();
                    ami_Token_Failure.description = dyObj.Description.ToString();
                    ami_Token_Failure.jsonData = json;

                    list_Ami_Token_Failure.Add(ami_Token_Failure);
                }
                
            }
            return list_Ami_Token_Failure;
        }

        public List<string> qInsert(dynamic endpointFailureResult,string token) {
            var param = endpointFailureResult;
            List<string> tranList = null;
            TokenFailureController tokenFailureController = new TokenFailureController();
            tranList = tokenFailureController.qInsert(convertFailure(param, token));
            return tranList;
        }
    }
}
