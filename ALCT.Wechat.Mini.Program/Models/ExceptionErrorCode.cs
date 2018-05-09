namespace ALCT.Wechat.Mini.Program.Models
{
    public class ExceptionErrorCode
    {
		protected ExceptionErrorCode() 
		{
			
		}
		public const string InterServerError = "500";
		public const string BadRequest = "400";
		public const string NotFound = "404";
		public const string NoContent = "204";

		public const string SystemUnhandledErrorCode = "SS000000";
	}
}