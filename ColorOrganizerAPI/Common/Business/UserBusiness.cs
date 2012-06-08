using System.Linq;
using Service.Common.Encryption;
using Service.Common.FaultContracts;
using Service.Common.MessageContracts;
using System;
using System.Configuration;

namespace Service.Common.Business
{
    public class UserBusiness
    {
        public static ServiceGroup AuthenticateRequest(BaseRequest request)
        {
            ServiceGroup group = ServiceGroup.None;
            if (String.IsNullOrEmpty(request.SecurityToken))
            {
                ExceptionHandling.RaiseException("Security Token can not be empty", ErrorCode.BadRequest);
            }

            if (ConfigurationManager.AppSettings.AllKeys.ToList().Contains("TokenKey"))
            {
                var decryptVal = string.Empty;
                try
                {
                    decryptVal = CryptHelper.DecryptString(request.SecurityToken, ConfigurationManager.AppSettings["TokenKey"]);
                }
                catch (Exception)
                {
                    ExceptionHandling.RaiseException("Security Token is not valid", ErrorCode.InValidCredential);
                }
                if (decryptVal.Length > 1)
                {
                    Guid decryptGuid;
                    if (!Guid.TryParse(decryptVal.Substring(1), out decryptGuid))
                    {
                        ExceptionHandling.RaiseException("Security Token is not valid", ErrorCode.InValidCredential);
                    }
                    Enum.TryParse(decryptVal.Substring(0, 1), true, out group);
                    if (group == ServiceGroup.None)
                    {
                        ExceptionHandling.RaiseException("Security Token is not valid", ErrorCode.InValidCredential);
                    }
                }
                else
                {
                    ExceptionHandling.RaiseException("Security Token is not valid", ErrorCode.InValidCredential);
                }
            }
            else
            {
                ExceptionHandling.RaiseException("Token Key not found in config file", ErrorCode.Internal);
            }
            return group;
        }

        public static string GenerateSecurityToken(ServiceGroup group)
        {
            if (group == ServiceGroup.None)
            {
                ExceptionHandling.RaiseException("Must pass valid service group to generate security token", ErrorCode.InValidCredential);
            }
            if (ConfigurationManager.AppSettings.AllKeys.ToList().Contains("TokenKey"))
            {
                string resultString = string.Empty;
                try
                {
                    resultString = CryptHelper.EncryptString(Convert.ToInt32(group) + Guid.NewGuid().ToString(), ConfigurationManager.AppSettings["TokenKey"]);
                }
                catch (Exception)
                {
                    ExceptionHandling.RaiseException("Token Key not found in config file", ErrorCode.Internal);
                }
                return resultString;
            }
            else
            {
                ExceptionHandling.RaiseException("Token Key not found in config file", ErrorCode.Internal);
            }
            return string.Empty;
        }
    }
}