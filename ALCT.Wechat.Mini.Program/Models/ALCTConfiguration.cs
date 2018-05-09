namespace ALCT.Wechat.Mini.Program.Models
{
    public class ALCTConfiguration
    {
        public string EnterpriseCode {get; set;} = "E0000101";
        public string AppIdentity {get; set;} = "072dffe08b9d412b99eafab3e2f02c93";
        public string AppKey {get; set;} = "1b1b734be0b64b549968ae8058c3b4af";
        public string OpenApiHost {get; set;} = "http://local.alct.com:4009/";
        public string DriverLoginUrl {get; set;} = "api/v1/openapi/apps/login";
        public string ApiLoginUrl {get; set;} = "api/v1/openapi/enterprises/login";
        public string SendVerificationCodeUrl {get; set;} = "api/v1/openapi/apps/verification?phoneNumber={0}";
        public string VerifyVerificationCodeUrl {get; set;} = "api/v1/openapi/apps/verify?phoneNumber={0}&code={1}";
        public string GetExecutingShipmentUrl {get; set;} = "api/v1/openapi/shipments/driver-shipment";
        public string GetShipmentDetailUrl {get; set;} = "api/v1/openapi/shipments/detail/{0}";
        public string ShipmentEventUrl {get; set;} = "api/v1/openapi/shipments/operation/{0}";
        public string GetDriverInvoiceUrl {get; set;} = "api/v1/openapi/driver-invoices";
        public string ConfirmDriverInvoiceUrl {get; set;} = "api/v1/openapi/driver-invoices/confirm";
        public string BindUrl {get; set;}
    }
}