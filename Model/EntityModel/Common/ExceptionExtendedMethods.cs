using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.Common
{
    public class ExceptionExtendedMethods
    {
        //sample methods
        private string _getInnerExceptions(Exception ex)
        {
            var exceptionMessages = new StringBuilder();
            do
            {
                ex = ex.InnerException;
                exceptionMessages.Append(ex);
            }
            while (ex != null);
            return exceptionMessages.ToString();
        }

        //sample methods
        private void _raiseException(Exception ex)
        {
            var exceptionMessages = new StringBuilder();
            do
            {
                exceptionMessages.Append(ex.Message);
                ex = ex.InnerException;
            }
            while (ex != null);
            throw new Exception(exceptionMessages.ToString());
        }

        //sample calling methods
        public void SomeMethod1()
        {
            try
            {
                //some code to try
            }
            catch (Exception e)
            {
                throw new Exception(this._getInnerExceptions(e));
            }
        }

        //sample calling methods
        public void SomeMethod2()
        {
            try
            {
                //some code to try
            }
            catch (Exception e)
            {
                this._raiseException(e);
            }
        }

        public static string GetInnerExceptions(Exception ex)
        {
            string exceptionMessages = string.Empty;
            do
            {
                exceptionMessages += ex.Message + ";;;;";
                ex = ex.InnerException;
            }
            while (ex != null);
            return exceptionMessages;
        }
    }
}
