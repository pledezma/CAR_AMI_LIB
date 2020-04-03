using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;

namespace CAR_AMI_LIB
{
    public class Ami_Control
    {
        public bool ReconnectMeter(string key)
        {
            bool r = true;
            string resultado = "";
            TokenController tokenController = new TokenController();
            Ami_Control_Service.ControlServiceClient controlServiceClient = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.ReconnectMeterRequest reconnectMeterRequest = new Ami_Control_Service.ReconnectMeterRequest();
            Ami_Control_Service.RequestToken requestToken = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequest endpointRequest = new Ami_Control_Service.EndpointRequest();
            endpointRequest.ElectronicSerialNumber = "2.16.840.1.114416.15.243." + key;
            reconnectMeterRequest.MeterRequest = endpointRequest;
            try
            {
                requestToken = controlServiceClient.ReconnectMeter(reconnectMeterRequest);
                r = tokenController.insert(requestToken, key, "ReconnectMeter"); 
            }
            catch (Exception e)
            {
                r = false;
            }
            return r;
        }
         
        public bool DisconnectMeter(string key)
        {
            bool r = true;
            string resultado = "";
            TokenController tokenController = new TokenController();
            Ami_Control_Service.ControlServiceClient controlServiceClient = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.DisconnectMeterRequest disconnectMeterRequest = new Ami_Control_Service.DisconnectMeterRequest();
            Ami_Control_Service.RequestToken requestToken = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequest endpointRequest = new Ami_Control_Service.EndpointRequest();
            endpointRequest.ElectronicSerialNumber = "2.16.840.1.114416.15.243." + key;
            disconnectMeterRequest.MeterRequest = endpointRequest;
            try
            {
                requestToken = controlServiceClient.DisconnectMeter(disconnectMeterRequest);
                r = tokenController.insert(requestToken,key, "DisconnectMeter"); 
            }
            catch (Exception e)
            {
            }
            return r;
        }
        
        public bool GetDisconnectMeterResult(string token)
        {
            bool r = true;
            var json = "";
            string result = "";
            TokenController tokenController = null;
            List<string> tranList = null;
            TokenFailureController tokenFailureController = null;
            model.Ami_Control ami_Control = null;
            model.Ami_Period ami_Period = null;
            model.Ami_Token ami_Token = null;
            Ami_Control_Service.EndpointFailure service_endpointFailure = new Ami_Control_Service.EndpointFailure();
            Ami_Control_Service.ControlServiceClient controlServiceClient = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.RequestToken requestToken = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequestResult endpointRequestResult = new Ami_Control_Service.EndpointRequestResult();
            Guid d = new Guid(token);
            requestToken.Id = d;
            try
            {
                ami_Control = new model.Ami_Control();
                endpointRequestResult = controlServiceClient.GetDisconnectMeterResult(requestToken);
                result = endpointRequestResult.Result.ToString();
                tranList = new List<string>();
                if (result == "Success")//3
                {
                    json = JsonConvert.SerializeObject(endpointRequestResult);
                    ami_Control.jsonData = help.Base64Encode(json);
                    ami_Control.meter = endpointRequestResult.ElectronicSerialNumber;
                    ami_Control.status = endpointRequestResult.Result.ToString();
                    ami_Control.token = token;
                    tranList.Add(ami_Control.qInsert()); 
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qSuccsess(token));
                    r = ami_Control.transaction(tranList);
                }
                else if (result == "Failure")//5
                {
                    EndpointFailure endpointFailure = new EndpointFailure(); 
                    service_endpointFailure = endpointRequestResult.MeterFailure;
                    tranList = endpointFailure.qInsert(service_endpointFailure, token);
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qFailure(token));
                    r = ami_Control.transaction(tranList);
                }
                else if (result == "Cancell")//4
                {
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qCancell(token));
                    r = ami_Control.transaction(tranList);
                }

            }
            catch (Exception e)
            {
                //GetDisconnectMeterResult(token);
            }
            
            return r;

        } 
        
        public bool GetReconnectMeterResult(string token)
        {
            bool r = true;
            var json = "";
            string result = "";
            TokenController tokenController = null;
            TokenFailureController tokenFailureController = null;
            model.Ami_Control ami_Control = null;
            model.Ami_Period ami_Period = null;
            List<string> tranList = null;
            Ami_Control_Service.EndpointFailure service_endpointFailure = new Ami_Control_Service.EndpointFailure();
            Ami_Control_Service.ControlServiceClient controlServiceClient = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.RequestToken requestToken = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequestResult endpointRequestResult = null;  new Ami_Control_Service.EndpointRequestResult();
            Guid d = new Guid(token);
            requestToken.Id = d;
            try
            {
                ami_Control = new model.Ami_Control();
                tranList = new List<string>();
                endpointRequestResult = controlServiceClient.GetReconnectMeterResult(requestToken);
                result = endpointRequestResult.Result.ToString();
                if (result == "Success")//3
                {
                    json = JsonConvert.SerializeObject(endpointRequestResult);
                    ami_Control.jsonData = help.Base64Encode(json);
                    ami_Control.meter = endpointRequestResult.ElectronicSerialNumber;
                    ami_Control.status = endpointRequestResult.Result.ToString();
                    ami_Control.token = token;
                    tranList.Add(ami_Control.qInsert());
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qSuccsess(token));
                    r = ami_Control.transaction(tranList);
                }
                else if (result == "Failure")//5
                {
                    EndpointFailure endpointFailure = new EndpointFailure();
                    service_endpointFailure = endpointRequestResult.MeterFailure;
                    tranList = endpointFailure.qInsert(service_endpointFailure, token);
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qFailure(token));
                    r = ami_Control.transaction(tranList);
                     
                }
                else if (result == "Cancell")//4
                {
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qCancell(token));
                    r = ami_Control.transaction(tranList);
                }

            }
            catch (Exception e)
            {
                //GetReconnectMeterResult(token);
            }                       
            return r;
        }
        public bool GetDisconnectMeterResultMasive()
        {
            bool r = true;
            model.Ami_Token[] arr_ami_Token;
            model.Ami_Token ami_Token = new model.Ami_Token();
            ami_Token.tokenType = "DisconnectMeter";
            ami_Token.status = 0;
            arr_ami_Token = ami_Token.getAllTokenByTokenType();
            if (arr_ami_Token != null)
            {
                foreach (var item in arr_ami_Token)
                {
                    r = GetDisconnectMeterResult(item.token);
                }
            }
            return r;
        }
        public bool GetReconnectMeterResultMasive()
        {
            bool r = true;
            model.Ami_Token[] arr_ami_Token;
            model.Ami_Token ami_Token = new model.Ami_Token();
            ami_Token.tokenType = "ReconnectMeter";
            ami_Token.status = 0;
            arr_ami_Token = ami_Token.getAllTokenByTokenType();
            if (arr_ami_Token != null)
            {
                foreach (var item in arr_ami_Token)
                {
                    r = GetReconnectMeterResult(item.token);
                }
            }
            
            return r;
        }


        public bool DisconnectMeterMasive()
        {
            bool r = true; 
            model.Ami_Medidores_Instalados ami_Medidores_Instalados = new model.Ami_Medidores_Instalados();
            //ami_Medidores_Instalados.getAllMetersDisconect()
            //TODO SE HACE CON LISTA DE MEDIDORES TEST
            //model.Ami_Medidores_Instalados ami_Medidores_Instalados = new model.Ami_Medidores_Instalados();
            //model.Ami_Medidores_Instalados[] arr_ami_Medidores_Instalados;
            //arr_ami_Medidores_Instalados = ami_Medidores_Instalados.getAllMeters();
            // ami_Medidores_Instalados.lisMeters
            foreach (var item in ami_Medidores_Instalados.lisMeters)// ami_Medidores_Instalados.getAllMetersDisconect()
            {
               r = DisconnectMeter(item);//item.COD_INTERNO
            }
            return r;
        }
        
        public bool ReconnectMeterMasive()
        { 
            bool r = true;
            model.Ami_Token ami_Token = null;
            model.Ami_Medidores_Instalados ami_Medidores_Instalados = new model.Ami_Medidores_Instalados();
            //TODO SE HACE CON LISTA DE MEDIDORES TEST 
            /*
             * ami_Token = new model.Ami_Token();
                ami_Token.getAllTokenByStatus();
             */
            foreach (var item in ami_Medidores_Instalados.lisMeters)//ami_Medidores_Instalados.getAllMetersReconect()
            { 
                r = ReconnectMeter(item);//item.COD_INTERNO
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
            Ami_Control_Service.ControlServiceClient controlServiceClient = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.RequestToken requestToken = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.PingByEndpointsRequest pingByEndpointsRequest = new Ami_Control_Service.PingByEndpointsRequest();
            Ami_Control_Service.EndpointCollectionRequest endpointCollectionRequest = new Ami_Control_Service.EndpointCollectionRequest();
            ElectronicSerialNumbersCollection[0] = "2.16.840.1.114416.15.243." + key;//arrKey[0];
            endpointCollectionRequest.ElectronicSerialNumbers = ElectronicSerialNumbersCollection;
            pingByEndpointsRequest.EndpointCollectionRequest = endpointCollectionRequest;

            try
            {
                requestToken = controlServiceClient.PingByEndpoints(pingByEndpointsRequest);
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
            model.Ami_Control ami_Control = null;
            TokenController tokenController = null;
            List<string> tranList = null;
            TokenFailureController tokenFailureController = null;

            Ami_Control_Service.ControlServiceClient controlServiceClient = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.RequestToken requestToken = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointCollectionRequestResult endpointCollectionRequestResult = new Ami_Control_Service.EndpointCollectionRequestResult();

            Guid d = new Guid(token);
            requestToken.Id = d;
            try
            {
                endpointCollectionRequestResult = controlServiceClient.GetPingByEndpointsResult(requestToken);
                json = JsonConvert.SerializeObject(endpointCollectionRequestResult);
                ami_Control = new model.Ami_Control();
                tokenController = new TokenController();
                tokenFailureController = new TokenFailureController();
                tranList = new List<string>();
                result = endpointCollectionRequestResult.Result.ToString();
                if (result == "Success")//3
                { 
                    ami_Control.meter = "";
                    ami_Control.status = result;
                    ami_Control.token = token;
                    ami_Control.jsonData = help.Base64Encode(json);
                    tranList.Add(ami_Control.qInsert());
                    tranList.Add(tokenController.qSuccsess(token));
                    r = ami_Control.transaction(tranList); 
                }
                else if (result == "Failure")//5
                {
                    EndpointFailure endpointFailure = new EndpointFailure();
                    tranList = endpointFailure.qInsert(endpointCollectionRequestResult.EndpointFailureResult, token);
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qFailure(token));
                    r = ami_Control.transaction(tranList); 
                }
                else if (result == "Cancell")//4
                {
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qCancell(token));
                    r = ami_Control.transaction(tranList);
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
        public bool RestrictMeter()
        {
            bool r = false;

            return r;
        }
        public bool GetRestrictMeterResult()
        {
            bool r = false;

            return r;
        }
        public bool DownloadConfigurationByGroup()
        {
            bool r = false;

            return r;
        }
        public bool GetDownloadConfigurationByGroup()
        {
            bool r = false;

            return r;
        }
        public bool DownloadConfigurationByEndpoints()
        {
            bool r = false;

            return r;
        }
        public bool DownloadConfigurationByEndpointsResult()
        {
            bool r = false;

            return r;
        }
        public bool DetectLoadSideVoltageByMeter()
        {
            bool r = false;

            return r;
        }
        public bool GetDetectLoadSideVoltageByMeterResult()
        {
            bool r = false;

            return r;
        }
        public bool DisplayMessageByDevices()
        {
            bool r = false;

            return r;
        }
        public bool GetDisplayMessageByDevicesResult()
        {
            bool r = false;

            return r;
        }
    }
}
