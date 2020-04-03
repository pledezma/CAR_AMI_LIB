using System;
using System.Collections.Generic;
using System.Text;
using model;

namespace CAR_AMI_LIB
{
    public class TokenFailureController
    {
        public List<string> qInsert(List<Ami_Token_Failure> list_ami_Token_Failure) {
            bool r = false;             
            string q = "";
            List<string> tranList = null;
            model.Ami_Token_Failure ami_Token_Failure = new model.Ami_Token_Failure();
            if (list_ami_Token_Failure.Count > 0)
            {
                tranList = new List<string>();
                foreach (var item in list_ami_Token_Failure )
                {
                    ami_Token_Failure.description = item.description;
                    ami_Token_Failure.jsonData = item.jsonData;
                    ami_Token_Failure.reason = item.reason;
                    ami_Token_Failure.token = item.token;
                    q = ami_Token_Failure.qInsert();
                    tranList.Add(q); 
                }
                 
            }
            return tranList;
        }

       
    }
}
