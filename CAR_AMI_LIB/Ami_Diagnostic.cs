using System;
using System.Collections.Generic;
using System.Text;

namespace CAR_AMI_LIB
{
    public class Ami_Diagnostic
    {
        public bool ReadDisconnectStateByMeters()
        {
            bool r = true;
            Ami_Diagnostic_Service.DiagnosticServiceClient client = new Ami_Diagnostic_Service.DiagnosticServiceClient();
            Ami_Diagnostic_Service.RequestToken token = new Ami_Diagnostic_Service.RequestToken();
            Ami_Diagnostic_Service.ReadDisconnectStateByMetersRequest request = new Ami_Diagnostic_Service.ReadDisconnectStateByMetersRequest();
            Ami_Diagnostic_Service.ReadDisconnectStateByMetersResult result = new Ami_Diagnostic_Service.ReadDisconnectStateByMetersResult();
            Ami_Diagnostic_Service.EndpointCollectionRequest endpointCollection = new Ami_Diagnostic_Service.EndpointCollectionRequest();

            Ami_Diagnostic_Service.EndpointCollectionRequestResult endpointCollectionResult = new Ami_Diagnostic_Service.EndpointCollectionRequestResult();
            string[] endPoints = null;
            endPoints = new string[1];
            endPoints[0] = "2.16.840.1.114416.15.243.15425";
            //endPoints[1] = "2.16.840.1.114416.15.243.15422";

            endpointCollection.ElectronicSerialNumbers = endPoints;
            request.EndpointCollectionRequest = endpointCollection;

            //token = await client.ReadDisconnectStateByMetersAsync(request);
            token = client.ReadDisconnectStateByMeters(request);
            //result = await client.GetReadDisconnectStateByMetersResultAsync(token); 
            client.CloseAsync();
            return r;

        }
        
        public bool GetDisconnectStateResult(string key)
        {
            bool r = true;
            Ami_Diagnostic_Service.DiagnosticServiceClient client = new Ami_Diagnostic_Service.DiagnosticServiceClient();
            Ami_Diagnostic_Service.RequestToken token = new Ami_Diagnostic_Service.RequestToken();
            Ami_Diagnostic_Service.ReadDisconnectStateByMetersResult result = new Ami_Diagnostic_Service.ReadDisconnectStateByMetersResult();

            Guid d = new Guid(key);
            token.Id = d;
            result = client.GetReadDisconnectStateByMetersResult(token);
            client.CloseAsync();
            return r;

        }
    }
}
