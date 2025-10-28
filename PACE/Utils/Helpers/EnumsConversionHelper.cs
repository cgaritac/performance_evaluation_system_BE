namespace PACE.Utils.Helpers;
using PACE.Utils.Constants;
using PACE.Utils.Enums;

public static class EnumsConversionHelper
{
    public static int? GetFeedbackValue(string searchTerm)
    {
        if (EnumsConstants.NotEvaluated.Contains(searchTerm))
        {
            return (int)FeedbackEnum.NotEvaluated;
        }
        else if (EnumsConstants.Outstanding.Contains(searchTerm))
        {
            return (int)FeedbackEnum.Outstanding;
        }
        else if (EnumsConstants.Successful.Contains(searchTerm))
        {
            return (int)FeedbackEnum.Successful;
        }
        else if (EnumsConstants.NeedsImprovement.Contains(searchTerm))
        {
            return (int)FeedbackEnum.NeedsImprovement;
        }
        else if (EnumsConstants.Unsatisfactory.Contains(searchTerm))
        {
            return (int)FeedbackEnum.Unsatisfactory;
        }
        return null;
    }
}
