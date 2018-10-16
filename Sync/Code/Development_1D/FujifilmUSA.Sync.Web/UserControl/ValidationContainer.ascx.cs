
namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ValidationContainer : BaseControl
    {

        #region Declaration
        string _validationProperty;
        public string ResourceValidationMsg { get; set; }
        //public string ValidationMsg { get; set; }
        public string ValidationMsg
        {
            get { return string.IsNullOrEmpty(ResourceValidationMsg)? _validationProperty : GetResourceString(ResourceValidationMsg); }
            set { _validationProperty = value; }
        }
        public int Size { get; set; }
        #endregion


    }
}