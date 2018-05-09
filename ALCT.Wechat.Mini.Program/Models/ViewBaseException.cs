using System;
using System.Collections.Generic;
using System.Linq;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class ViewBaseException : Exception
    {
        public string ErrorCode;
        public IList<string> Parameters;
        public ViewBaseException(string errorCode) 
        {
            this.ErrorCode = errorCode;
            Parameters = new List<string>();
        }

        public ViewBaseException AddParamter(params string[] parameters) 
        {
            if(parameters == null) 
            {
                parameters = new string[0];
            }
            Parameters = parameters.ToList();
            return this;
        }

        public ViewBaseException AddParamter(IList<string> parameters) 
        {
            if(parameters == null) 
            {
                parameters = new List<string>();
            }
            Parameters = parameters;
            return this;
        }
    }
}