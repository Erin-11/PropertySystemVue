using System;
using System.Collections.Generic;

namespace KnightFrank.BAL.Extensions
{
    public static class ExceptionExtension
    {
        public static string GetExceptionErrorMessage(this Exception ex)
        {
            var errorMessage = ex.Message;
            if (ex.InnerException != null)
                errorMessage = ex.InnerException.GetExceptionErrorMessage();
            return errorMessage;
        }

        public static IEnumerable<string> GetExceptionErrorMessages(this Exception ex)
        {
            var errors = new List<string>
            {
                ex.Message
            };
            if (ex.InnerException != null)
                errors.AddRange(GetExceptionErrorMessages(ex.InnerException));
            return errors;
        }

        //public static string GetExceptionErrorMessage(this HandleErrorInfo errorInfo)
        //{
        //    if (errorInfo.Exception is DbEntityValidationException)
        //    {
        //        var errMsgs = (errorInfo.Exception as DbEntityValidationException).EntityValidationErrors.SelectMany(sm =>
        //            sm.ValidationErrors.Select(s => string.Format("{0} ({1},{2}", s.ErrorMessage, sm.Entry.Entity.ToString(), s.PropertyName)));
        //        return string.Join(Environment.NewLine, errMsgs);
        //    }
        //    else
        //    {
        //        return errorInfo.Exception.GetExceptionErrorMessage(); ;
        //    }
        //}

        //public static string GetExceptionErrorMessage(this ModelStateDictionary modelstate)
        //{
        //    var errMsgs = modelstate.SelectMany(sm => sm.Value.Errors.Select(s => s.ErrorMessage));
        //    return string.Join(Environment.NewLine, errMsgs);

        //    //StringBuilder sb = new StringBuilder();
        //    //foreach (KeyValuePair<string, ModelState> state in modelstate)
        //    //{
        //    //    if (state.Value.Errors.Count > 0)
        //    //    {
        //    //        sb.AppendLine(state.Value.Errors[0].ErrorMessage + Environment.NewLine);
        //    //    }
        //    //}
        //    //return sb.ToString();
        //}
    }
}
