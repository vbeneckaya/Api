using System.ComponentModel;
using Domain.Error;

namespace Services
{
    public class ErrorHandlerService : IErrorService
    {

        public ErrorViewModel GetError(ErrorCodes errorCode)
        {
            return new ErrorViewModel
            {
                Code = errorCode.ToString(),
                Message = GetDescription(errorCode)
            };
        }

        private string GetDescription(ErrorCodes enumerationValue)
        {
            var _enumerationValue = enumerationValue.ToString();

            var memberInfo = typeof(ErrorCodes).GetMember(_enumerationValue);
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return _enumerationValue;
        }
    }
}
