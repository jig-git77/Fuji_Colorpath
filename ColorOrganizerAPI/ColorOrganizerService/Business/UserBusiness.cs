using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using Service.Common.Business;
using Service.Common.FaultContracts;

namespace FujifilmUSA.ColorOrganizerService.Business
{
    public class UserBusiness
    {
        internal string ErrorMessage { get; set; }

        internal bool IsErrorOccured { get; set; }

        internal void ErrorOccured(object sender, ErrorArgs e)
        {
            this.IsErrorOccured = true;
            this.ErrorMessage = e.ErrorMessage;
        }

        internal void UserLogin(string username, string hashPassword, bool dontRaiseException = false)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(hashPassword))
            {
                ExceptionHandling.RaiseException("UserName/Password field can not be empty", ErrorCode.BadRequest);
            }

            var userBusiness = Sync.Business.BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += this.ErrorOccured;
            var user = userBusiness.UserLogin(username, hashPassword, UserType.XmfColorOrganizerUser);

            if (this.IsErrorOccured)
            {
                ExceptionHandling.RaiseException(this.ErrorMessage, ErrorCode.Internal);
            }

            if (!dontRaiseException)
            {
                if (user == null || user.UserId == 0)
                {
                    ExceptionHandling.RaiseException("Invalid credentials", ErrorCode.InValidCredential);
                }
            }

            CurrentUser = user;
        }

        internal User CurrentUser { get; set; }
    }
}
