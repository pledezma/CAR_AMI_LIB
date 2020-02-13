using System;
using System.Collections.Generic;
using System.Text;

namespace CAR_AMI_LIB
{
    public class Ami_Control
    {
        public bool ReconnectMeter(string key)
        {
            bool r = true;
            Ami_Control_Service.ControlServiceClient client = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.ReconnectMeterRequest ReconnectRequest = new Ami_Control_Service.ReconnectMeterRequest();
            Ami_Control_Service.RequestToken token = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequest endPoint = new Ami_Control_Service.EndpointRequest();
            endPoint.ElectronicSerialNumber = "2.16.840.1.114416.15.243." + key;
            ReconnectRequest.MeterRequest = endPoint;
            try
            {
                token = client.ReconnectMeter(ReconnectRequest);
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
            Ami_Control_Service.ControlServiceClient client = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.DisconnectMeterRequest DisconenectRequest = new Ami_Control_Service.DisconnectMeterRequest();
            Ami_Control_Service.RequestToken token = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequest endPoint = new Ami_Control_Service.EndpointRequest();
            endPoint.ElectronicSerialNumber = "2.16.840.1.114416.15.243." + key;
            DisconenectRequest.MeterRequest = endPoint;
            try
            {
                token = client.DisconnectMeter(DisconenectRequest);
            }
            catch (Exception e)
            {
            }
            return r;
        }
        
        public bool GetDisconnectMeterResult(string key)
        {
            bool r = true;
            Ami_Control_Service.ControlServiceClient client = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.RequestToken token = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequestResult endPoint = new Ami_Control_Service.EndpointRequestResult();
            Guid d = new Guid(key);
            token.Id = d;
            try
            {
                endPoint = client.GetDisconnectMeterResult(token);
            }
            catch (Exception e)
            { 
            }
            
            return r;

        }
         
        public bool GetReconnectMeterResult(string key)
        {
            bool r = true;
            Ami_Control_Service.ControlServiceClient client = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.RequestToken token = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequestResult endPoint = new Ami_Control_Service.EndpointRequestResult();
            Guid d = new Guid(key);
            token.Id = d;
            try
            {
                endPoint = client.GetReconnectMeterResult(token);
            }
            catch (Exception e)
            {
                
            }                       
            return r;
        }
         
        public bool DisconnectMeterMasive(string key)
        {//1c681827-2161-4cef-874d-734137e1c0d3
            bool r = true;
            Ami_Control_Service.ControlServiceClient client = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.DisconnectMeterRequest DisconenectRequest = new Ami_Control_Service.DisconnectMeterRequest();
            Ami_Control_Service.RequestToken token = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequest endPoint = new Ami_Control_Service.EndpointRequest();
            List<int> lisMeters = new List<int>();
            List<string> lisToken = new List<string>();
            //lisMeters.Add(15422);
            lisMeters.Add(15424);
            lisMeters.Add(15425);
            foreach (var item in lisMeters)
            {
                endPoint.ElectronicSerialNumber = "2.16.840.1.114416.15.243." + item;
                DisconenectRequest.MeterRequest = endPoint;
                try
                {
                    token = client.DisconnectMeter(DisconenectRequest);
                    lisToken.Add(token.Id.ToString());
                }
                catch (Exception e)
                {
                     
                }
            }
            return r;
        }
        
        public bool ReconnectMeterMasive(string key)
        {//1c681827-2161-4cef-874d-734137e1c0d3
            bool r = true;
            Ami_Control_Service.ControlServiceClient client = new Ami_Control_Service.ControlServiceClient();
            Ami_Control_Service.ReconnectMeterRequest ReconnectRequest = new Ami_Control_Service.ReconnectMeterRequest();
            Ami_Control_Service.RequestToken token = new Ami_Control_Service.RequestToken();
            Ami_Control_Service.EndpointRequest endPoint = new Ami_Control_Service.EndpointRequest();
            List<int> lisMeters = new List<int>();
            List<string> lisToken = new List<string>();
            //lisMeters.Add(15422);
            lisMeters.Add(15424);
            lisMeters.Add(15425);
            foreach (var item in lisMeters)
            {
                endPoint.ElectronicSerialNumber = "2.16.840.1.114416.15.243." + item;
                ReconnectRequest.MeterRequest = endPoint;
                try
                {
                    token = client.ReconnectMeter(ReconnectRequest);
                    lisToken.Add(token.Id.ToString());
                }
                catch (Exception e)
                {

                } 
            }
            return r;
        }
    }
}
