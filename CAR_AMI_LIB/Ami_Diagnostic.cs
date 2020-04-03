using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAR_AMI_LIB
{
    public class Ami_Diagnostic
    {
        public bool ReadDisconnectStateByMeters(string key)//string[] arrKey
        {
            bool r = true;
            string resultado = "";
            TokenController tokenController = new TokenController();
            Ami_Diagnostic_Service.DiagnosticServiceClient diagnosticServiceClient = new Ami_Diagnostic_Service.DiagnosticServiceClient();
            Ami_Diagnostic_Service.RequestToken requestToken = new Ami_Diagnostic_Service.RequestToken();
            Ami_Diagnostic_Service.ReadDisconnectStateByMetersRequest readDisconnectStateByMetersRequest = new Ami_Diagnostic_Service.ReadDisconnectStateByMetersRequest();
            Ami_Diagnostic_Service.ReadDisconnectStateByMetersResult readDisconnectStateByMetersResult = new Ami_Diagnostic_Service.ReadDisconnectStateByMetersResult();
            Ami_Diagnostic_Service.EndpointCollectionRequest endpointCollectionRequest = new Ami_Diagnostic_Service.EndpointCollectionRequest();

            Ami_Diagnostic_Service.EndpointCollectionRequestResult endpointCollectionResult = new Ami_Diagnostic_Service.EndpointCollectionRequestResult();
            string[] ElectronicSerialNumbersCollection = null;
            ElectronicSerialNumbersCollection = new string[1];
            ElectronicSerialNumbersCollection[0] = "2.16.840.1.114416.15.243." + key;//arrKey[0];


            endpointCollectionRequest.ElectronicSerialNumbers = ElectronicSerialNumbersCollection;
            readDisconnectStateByMetersRequest.EndpointCollectionRequest = endpointCollectionRequest;
            
            try
            {
                requestToken = diagnosticServiceClient.ReadDisconnectStateByMeters(readDisconnectStateByMetersRequest);
                r = tokenController.insert(requestToken, key, "ReadDisconnectStateByMeters"); 
            }
            catch (Exception e)
            {
            }
            return r;

        }
        public bool ReadDisconnectStateByMetersMasive()
        {
            bool r = true;
            string[] arrKey = null;
            model.Ami_Medidores_Instalados ami_Medidores_Instalados = new model.Ami_Medidores_Instalados();
            if (ami_Medidores_Instalados.lisMeters != null)
            {
                foreach (var item in ami_Medidores_Instalados.lisMeters)// ami_Medidores_Instalados.getAllMetersDisconect()
                {
                    //arrKey = new string[1];
                    //arrKey[0] = item;//item.token;
                    r = ReadDisconnectStateByMeters(item);//arrKey
                }
            }
            return r;
            /*bool r = true;
            model.Ami_Token[] arr_ami_Token;
            model.Ami_Token ami_Token = new model.Ami_Token();
            ami_Token.tokenType = "ReadDisconnectStateByMeters";
            ami_Token.status = 0;
            arr_ami_Token = ami_Token.getAllTokenByTokenType();
            string[] arrKey = null;
            if (arr_ami_Token != null)
            {
                foreach (var item in arr_ami_Token)
                {
                    arrKey = new string[1];
                    arrKey[0] = item.token;
                    r = ReadDisconnectStateByMeters(arrKey);
                }
            }

            return r;*/

        }

        public bool GetReadDisconnectStateByMetersResult(string token)
        {
            bool r = false;
            var json = "";
            string result = "";
            model.Ami_Diagnostic ami_Diagnostic = null;
            TokenController tokenController = null;
            List<string> tranList = null;
            TokenFailureController tokenFailureController = null;
            Ami_Diagnostic_Service.EndpointFailureResult service_endpointFailureResult = new Ami_Diagnostic_Service.EndpointFailureResult();
            Ami_Diagnostic_Service.DiagnosticServiceClient diagnosticServiceClient = new Ami_Diagnostic_Service.DiagnosticServiceClient();
            Ami_Diagnostic_Service.RequestToken requestToken = new Ami_Diagnostic_Service.RequestToken();
            Ami_Diagnostic_Service.ReadDisconnectStateByMetersResult readDisconnectStateByMetersResult = new Ami_Diagnostic_Service.ReadDisconnectStateByMetersResult();

            Guid d = new Guid(token);
            requestToken.Id = d;
           

            try
            {
                readDisconnectStateByMetersResult = diagnosticServiceClient.GetReadDisconnectStateByMetersResult(requestToken);
                json = JsonConvert.SerializeObject(readDisconnectStateByMetersResult);
                ami_Diagnostic = new model.Ami_Diagnostic();
                tokenController = new TokenController();
                tokenFailureController = new TokenFailureController();
                tranList = new List<string>();
                result = readDisconnectStateByMetersResult.Result.ToString();
                if (result == "Success")//3
                {
                    if (readDisconnectStateByMetersResult.MeterDisconnectSwitchResults.Length > 0)
                    { 
                        ami_Diagnostic.status = readDisconnectStateByMetersResult.Result.ToString();
                        ami_Diagnostic.token = token;
                        ami_Diagnostic.meter = readDisconnectStateByMetersResult.MeterDisconnectSwitchResults[0].ElectronicSerialNumber;
                        ami_Diagnostic.switchState = readDisconnectStateByMetersResult.MeterDisconnectSwitchResults[0].SwitchState.ToString();
                        ami_Diagnostic.extendedSwitchState = readDisconnectStateByMetersResult.MeterDisconnectSwitchResults[0].ExtendedSwitchState;
                        ami_Diagnostic.jsonData = help.Base64Encode(json);   
                        tranList.Add(ami_Diagnostic.qInsert());
                        tranList.Add(tokenController.qSuccsess(token));
                        r = ami_Diagnostic.transaction(tranList);
                    }
                }
                else if (result == "Failure")//5
                {
                    EndpointFailure endpointFailure = new EndpointFailure(); 
                    tranList = endpointFailure.qInsert(readDisconnectStateByMetersResult.EndpointFailureResult, token);
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qFailure(token));
                    r = ami_Diagnostic.transaction(tranList);

                }
                else if (result == "Cancell")//4
                {
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qCancell(token));
                    r = ami_Diagnostic.transaction(tranList);
                }
            }
            catch (Exception e)
            {    
            } 
            return r; 
        }
        public bool GetReadDisconnectStateByMetersResultMasive()
        {
            bool r = true;
            model.Ami_Token[] arr_ami_Token;
            model.Ami_Token ami_Token = new model.Ami_Token();
            ami_Token.tokenType = "ReadDisconnectStateByMeters";
            ami_Token.status = 0;
            arr_ami_Token = ami_Token.getAllTokenByTokenType();
            if (arr_ami_Token != null)
            {
                foreach (var item in arr_ami_Token)
                {
                    r = GetReadDisconnectStateByMetersResult(item.token);
                }
            }

            return r; 
        }
        public bool PingByEndpoints(string key)//string[] arrKey
        {
            bool r = true;
            string resultado = "";
            string[] ElectronicSerialNumbersCollection = null;
            ElectronicSerialNumbersCollection = new string[1];
            TokenController tokenController = new TokenController();
            Ami_Diagnostic_Service.DiagnosticServiceClient diagnosticServiceClient = new Ami_Diagnostic_Service.DiagnosticServiceClient();
            Ami_Diagnostic_Service.RequestToken requestToken = new Ami_Diagnostic_Service.RequestToken();
            Ami_Diagnostic_Service.PingByEndpointsRequest pingByEndpointsRequest = new Ami_Diagnostic_Service.PingByEndpointsRequest();
            Ami_Diagnostic_Service.EndpointCollectionRequest endpointCollectionRequest = new Ami_Diagnostic_Service.EndpointCollectionRequest();
            ElectronicSerialNumbersCollection[0] = "2.16.840.1.114416.15.243." + key;//arrKey[0];
            endpointCollectionRequest.ElectronicSerialNumbers = ElectronicSerialNumbersCollection;
            pingByEndpointsRequest.EndpointCollectionRequest = endpointCollectionRequest;

            try
            {
                requestToken = diagnosticServiceClient.PingByEndpoints(pingByEndpointsRequest);
                r = tokenController.insert(requestToken, key, "PingByEndpoints");
            }
            catch (Exception e)
            {
            }
            return r;
        }
        public bool PingByEndpointsMasive()
        {
            bool r = true;
            string[] arrKey = null;
            model.Ami_Medidores_Instalados ami_Medidores_Instalados = new model.Ami_Medidores_Instalados();
            if (ami_Medidores_Instalados.lisMeters != null)
            {
                foreach (var item in ami_Medidores_Instalados.lisMeters)// ami_Medidores_Instalados.getAllMetersDisconect()
                {
                    //arrKey = new string[1];
                    //arrKey[0] = item;//item.token;
                    r = PingByEndpoints(item);//arrKey
                }
            }
            return r;

        }
        public bool GetPingByEndpointsResult(string token)
        {
            bool r = true;
            var json = "";
            string result = "";
            model.Ami_Diagnostic ami_Diagnostic = null;
            TokenController tokenController = null;
            List<string> tranList = null;
            TokenFailureController tokenFailureController = null;

            Ami_Diagnostic_Service.DiagnosticServiceClient diagnosticServiceClient = new Ami_Diagnostic_Service.DiagnosticServiceClient();
            Ami_Diagnostic_Service.RequestToken requestToken = new Ami_Diagnostic_Service.RequestToken();
            Ami_Diagnostic_Service.EndpointCollectionRequestResult endpointCollectionRequestResult = new Ami_Diagnostic_Service.EndpointCollectionRequestResult();

            Guid d = new Guid(token);
            requestToken.Id = d;
            try
            {
                endpointCollectionRequestResult = diagnosticServiceClient.GetPingByEndpointsResult(requestToken);
                json = JsonConvert.SerializeObject(endpointCollectionRequestResult);
                ami_Diagnostic = new model.Ami_Diagnostic();
                tokenController = new TokenController();
                tokenFailureController = new TokenFailureController();
                tranList = new List<string>();
                result = endpointCollectionRequestResult.Result.ToString();
                if (result == "Success")//3
                {
                    ami_Diagnostic.meter = "";
                    ami_Diagnostic.status = result;
                    ami_Diagnostic.token = token;
                    ami_Diagnostic.jsonData = help.Base64Encode(json);
                    tranList.Add(ami_Diagnostic.qInsert());
                    tranList.Add(tokenController.qSuccsess(token));
                    r = ami_Diagnostic.transaction(tranList);
                }
                else if (result == "Failure")//5
                {
                    EndpointFailure endpointFailure = new EndpointFailure();
                    tranList = endpointFailure.qInsert(endpointCollectionRequestResult.EndpointFailureResult, token);
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qFailure(token));
                    r = ami_Diagnostic.transaction(tranList);
                }
                else if (result == "Cancell")//4
                {
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qCancell(token));
                    r = ami_Diagnostic.transaction(tranList);
                }
            }
            catch (Exception e)
            {
            }

            return r;
        }
        public bool GetPingByEndpointsResultMasive()
        {
            bool r = true;
            model.Ami_Token[] arr_ami_Token;
            model.Ami_Token ami_Token = new model.Ami_Token();
            ami_Token.tokenType = "PingByEndpoints";
            ami_Token.status = 0;
            arr_ami_Token = ami_Token.getAllTokenByTokenType();
            if (arr_ami_Token != null)
            {
                foreach (var item in arr_ami_Token)
                {
                    r = GetPingByEndpointsResult(item.token);
                }
            }

            return r;

        }

    }
}
