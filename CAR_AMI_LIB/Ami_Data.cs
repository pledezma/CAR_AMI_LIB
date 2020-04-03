using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CAR_AMI_LIB
{
    public class Ami_Data
    {
        public bool ContingencyReadByEndpoints(string key)
        {
            DateTime date = DateTime.Now;
            bool r = false;
            string resultado = "";
            TokenController tokenController = new TokenController();
            Ami_Data_Service.DataServiceClient dataServiceClient = new Ami_Data_Service.DataServiceClient();
            Ami_Data_Service.ContingencyReadByEndpointsRequest contingencyReadByEndpointsRequest = new Ami_Data_Service.ContingencyReadByEndpointsRequest();
            Ami_Data_Service.ContingencyReadParameters contingencyReadParameters = new Ami_Data_Service.ContingencyReadParameters();
            Ami_Data_Service.RequestToken requestToken = new Ami_Data_Service.RequestToken(); 
            
            contingencyReadParameters.ReadingStartTime = Convert.ToDateTime("24/3/2020 10:25:35");
            contingencyReadParameters.ReadingEndTime = Convert.ToDateTime("24/3/2020 13:25:35");
            contingencyReadParameters.RetrieveInstantaneousData = false;
            contingencyReadParameters.RetrievePriorSelfRead = true;
           

            Ami_Data_Service.EndpointCollectionRequest endpointCollectionRequest = new Ami_Data_Service.EndpointCollectionRequest();
            string[] electronicSerialNumbersCollection = new string[1];
            electronicSerialNumbersCollection[0] = "2.16.840.1.114416.15.243." + key;     
            endpointCollectionRequest.ElectronicSerialNumbers = electronicSerialNumbersCollection;
             
            contingencyReadByEndpointsRequest.EndpointCollectionRequest = endpointCollectionRequest; 
            contingencyReadByEndpointsRequest.Parameters = contingencyReadParameters;

            

            try
            {
                requestToken = dataServiceClient.ContingencyReadByEndpoints(contingencyReadByEndpointsRequest);
                r = tokenController.insert(requestToken, "SEVERAL", "ContingencyReadByEndpoints");
            }
            catch (Exception e)
            {

            }
            return r;
        }

        public bool ContingencyReadByEndpointsMasive()
        {
            bool r = false;
            model.Ami_Token ami_Token = null;
            model.Ami_Medidores_Instalados ami_Medidores_Instalados = new model.Ami_Medidores_Instalados();
            model.Ami_Medidores_Instalados[] arr_ami_Medidores_Instalados;
            arr_ami_Medidores_Instalados = ami_Medidores_Instalados.getAllMeters();
            if (ami_Medidores_Instalados.lisMeters != null)
            {
                foreach (var item in ami_Medidores_Instalados.lisMeters)
                {
                    r = ContingencyReadByEndpoints(item);
                }
            }
            return r;
        }
        public bool InterrogateByEndpoints(string key)
        {
            DateTime date = DateTime.Now;
            bool r = false;
            string resultado = "";
            TokenController tokenController = new TokenController();
            Ami_Data_Service.RequestToken requestToken = new Ami_Data_Service.RequestToken();
            Ami_Data_Service.DataServiceClient dataServiceClient = new Ami_Data_Service.DataServiceClient();
            Ami_Data_Service.InterrogateByEndpointsRequest interrogateByEndpointsRequest = new Ami_Data_Service.InterrogateByEndpointsRequest();
            Ami_Data_Service.InterrogationParameters interrogationParameters = new Ami_Data_Service.InterrogationParameters();

            interrogationParameters.ReadingStartTime = Convert.ToDateTime("24/3/2020 10:25:35");
            interrogationParameters.ReadingEndTime = Convert.ToDateTime("24/3/2020 13:25:35");
            interrogationParameters.RetrieveInstantaneousData = false;
            interrogationParameters.RetrievePriorSelfRead = true;

           
            interrogationParameters.InterrogationWindowStartTime = Convert.ToDateTime("02/4/2020 10:25:35");
            interrogationParameters.InterrogationWindowEndTime = Convert.ToDateTime("24/3/2021 13:25:35");

            Ami_Data_Service.EndpointCollectionRequest endpointCollectionRequest = new Ami_Data_Service.EndpointCollectionRequest();
            string[] electronicSerialNumbersCollection = new string[1];
            electronicSerialNumbersCollection[0] = "2.16.840.1.114416.15.243." + key;
            endpointCollectionRequest.ElectronicSerialNumbers = electronicSerialNumbersCollection;

            interrogateByEndpointsRequest.Parameters = interrogationParameters;
            interrogateByEndpointsRequest.EndpointCollectionRequest = endpointCollectionRequest;
            

            try
            {
                requestToken = dataServiceClient.InterrogateByEndpoints(interrogateByEndpointsRequest);
                r = tokenController.insert(requestToken, "SEVERAL", "InterrogateByEndpoints");
            }
            catch (Exception e)
            {

            }
            return r;
        }
        public bool InterrogateByEndpointsMasive()
        {
            bool r = false;
            model.Ami_Token ami_Token = null;
            model.Ami_Medidores_Instalados ami_Medidores_Instalados = new model.Ami_Medidores_Instalados();
            model.Ami_Medidores_Instalados[] arr_ami_Medidores_Instalados;
            arr_ami_Medidores_Instalados = ami_Medidores_Instalados.getAllMeters();
            if (ami_Medidores_Instalados.lisMeters != null)
            {
                foreach (var item in ami_Medidores_Instalados.lisMeters)
                {
                    r = InterrogateByEndpoints(item);//item.COD_INTERNO
                }
            }
            return r;
        }
        public bool GetInterrogateByEndpointsResult()
        {
            bool r = false;
            Ami_Data_Service.DataServiceClient dataServiceClient = new Ami_Data_Service.DataServiceClient(); 
            //LO DEVULEVE POR EL DATA SUBSCRIBE
            return r;
        }
        public bool GetContingencyReadByEndpointsResult() {
            bool r = false;
           //LO DEVULEVE POR EL DATA SUBSCRIBE
            return r;
        }
        
        
        public bool InteractiveReadByEndpoint(string key)
        {
            DateTime date = DateTime.Now;
            bool r = false;
            string resultado = "";
            TokenController tokenController = new TokenController();
            Ami_Data_Service.InteractiveReadByEndpointRequest interactiveReadByEndpointRequest = new Ami_Data_Service.InteractiveReadByEndpointRequest();
            Ami_Data_Service.InteractiveReadParameters interactiveReadParameters = new Ami_Data_Service.InteractiveReadParameters();

            interactiveReadParameters.ReadingStartTime = Convert.ToDateTime("24/3/2020 10:25:35");
            interactiveReadParameters.ReadingEndTime = Convert.ToDateTime("24/3/2020 13:25:35");
            interactiveReadParameters.RetrieveInstantaneousData = false;
            interactiveReadParameters.RetrievePriorSelfRead = true;
            Ami_Data_Service.EndpointRequest endpointRequest = new Ami_Data_Service.EndpointRequest();
            endpointRequest.ElectronicSerialNumber = "2.16.840.1.114416.15.243." + key;

            interactiveReadByEndpointRequest.EndpointRequest = endpointRequest;
            interactiveReadByEndpointRequest.Parameters = interactiveReadParameters;
            Ami_Data_Service.DataServiceClient dataServiceClient = new Ami_Data_Service.DataServiceClient();

            Ami_Data_Service.RequestToken requestToken = new Ami_Data_Service.RequestToken();

            try
            {
                requestToken = dataServiceClient.InteractiveReadByEndpoint(interactiveReadByEndpointRequest);
                r = tokenController.insert(requestToken, key, "InteractiveReadByEndpoint"); 
            }
            catch (Exception e)
            {

            }
            return r;
        } 
        public bool GetInteractiveReadByEndpointResult(string token)
        {
            var json = "";
            bool r = false;
            string result = "";
            Ami_Control_Service.EndpointFailure service_endpointFailure = new Ami_Control_Service.EndpointFailure();
            Ami_Data_Service.InteractiveReadByEndpointResult interactiveReadByEndpointResult = null;
            Ami_Data_Service.RequestToken requestToken = new Ami_Data_Service.RequestToken();
            Guid guid = new Guid(token);
            requestToken.Id = guid;
            Ami_Data_Service.DataServiceClient dataServiceClient = new Ami_Data_Service.DataServiceClient();
            model.Ami_Reads ami_Reads = null;
            model.Ami_Period ami_Period = null;
            model.Ami_Token ami_Token = null;
            model.cnx cnx = null;
            TokenController tokenController = null;
            List<string> tranList = null;
            TokenFailureController tokenFailureController = null;
            try
            {
                tranList = new List<string>();
                interactiveReadByEndpointResult = new Ami_Data_Service.InteractiveReadByEndpointResult();
                ami_Reads = new model.Ami_Reads();
                ami_Period = new model.Ami_Period();
                interactiveReadByEndpointResult = dataServiceClient.GetInteractiveReadByEndpointResult(requestToken);
                result = interactiveReadByEndpointResult.Result.ToString();

                if (result == "Success")//3
                {
                    json = JsonConvert.SerializeObject(interactiveReadByEndpointResult);  
                    if (ami_Period.getLastPeriod() != null)
                    {
                        ami_Reads.period = ami_Period.getLastPeriod()[0].id;
                    }
                    else
                    {
                        ami_Reads.period = 0;
                    }
                    ami_Reads.readjson = help.Base64Encode(json);

                    ami_Reads.esn = interactiveReadByEndpointResult.Identifier;
                    ami_Reads.token_request = interactiveReadByEndpointResult.RequestToken.Id.ToString();
                    ami_Reads.token_result = interactiveReadByEndpointResult.Result.ToString();
                    if (interactiveReadByEndpointResult.ReadDataCollection.Length > 0)
                    {
                        ami_Reads.serialnumber = interactiveReadByEndpointResult.ReadDataCollection[0].DeviceSerialNumber.ToString();
                        ami_Reads.cod_medidor = interactiveReadByEndpointResult.ReadDataCollection[0].DeviceSerialNumber.ToString();
                        /*
                        ami_Reads.whd = int.Parse(interactiveReadByEndpointResult.ReadDataCollection[0].RegisterValues[0].Value.ToString());
                        ami_Reads.whr = int.Parse(interactiveReadByEndpointResult.ReadDataCollection[0].RegisterValues[1].Value.ToString());
                        ami_Reads.whnet = int.Parse(interactiveReadByEndpointResult.ReadDataCollection[0].RegisterValues[5].Value.ToString());
                            */
                        ami_Reads.fecha_lectura = interactiveReadByEndpointResult.ReadDataCollection[0].RegisterValues[0].Timestamp;
                        foreach (var item in interactiveReadByEndpointResult.ReadDataCollection[0].RegisterValues)
                        {
                            string itemTimLower = item.Quantity.Trim().TrimEnd().TrimStart().ToLower();
                            while (itemTimLower.Contains(" "))
                            {
                                itemTimLower = itemTimLower.Replace(" ", "");
                            }
                            if (itemTimLower == "whd")
                            {
                                ami_Reads.whd = int.Parse(item.Value.ToString());
                                string s = item.Timestamp.ToString(); 
                            }
                            else if (itemTimLower == "whr")
                            {
                                ami_Reads.whr = int.Parse(item.Value.ToString());
                            }
                            else if (itemTimLower == "whnet")
                            {
                                ami_Reads.whnet = int.Parse(item.Value.ToString());
                            } 
                        }
                    }
                    tranList.Add(ami_Reads.qInsert()); 
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qSuccsess(token));
                    r = ami_Reads.transaction(tranList); 
                }
                else if (result == "Failure")//5
                {
                    EndpointFailure endpointFailure = new EndpointFailure();
                    tranList = endpointFailure.qInsert(interactiveReadByEndpointResult.EndpointFailure, token);
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qFailure(token));
                    r = ami_Reads.transaction(tranList);

                }
                else if (result == "Cancell")//4
                {
                    tokenController = new TokenController();
                    tranList.Add(tokenController.qCancell(token));
                    r = ami_Reads.transaction(tranList);
                }
                else if (result == "Pending")//4
                {
                }

            }
            catch (Exception e)
            {

            }
            return r;
        }
        public bool InteractiveReadByEndpointMasive() 
        {
            bool r = false;
            model.Ami_Token ami_Token = null;
            model.Ami_Medidores_Instalados ami_Medidores_Instalados = new model.Ami_Medidores_Instalados();
            model.Ami_Medidores_Instalados[] arr_ami_Medidores_Instalados;
            arr_ami_Medidores_Instalados = ami_Medidores_Instalados.getAllMeters();
            if (arr_ami_Medidores_Instalados != null)
            {
                foreach (var item in arr_ami_Medidores_Instalados)
                {
                    r = InteractiveReadByEndpoint(item.COD_INTERNO); 
                }
            }
            return r;
        }
        public bool GetInteractiveReadByEndpointResultMasive()
        {
            bool r = false;
            model.Ami_Token ami_Token = new model.Ami_Token();
            model.Ami_Token[] arr_ami_token;
            ami_Token.tokenType = "InteractiveReadByEndpointRequest";
            ami_Token.status = 0;//requestprocesado
            arr_ami_token = ami_Token.getAllTokenByTokenType();
            if (arr_ami_token != null)
            {
                foreach (var item in arr_ami_token)
                {
                    r = GetInteractiveReadByEndpointResult(item.token);                   
                }
            }
            return r;
        }
    }    
}
