namespace ForoWebApp.Helpers.Pagination;

public static class PaginationUtilities
{
    public static int CalculateLowerBoundPage(int currentPage)
    {
        return currentPage - 5 > 0 ? currentPage - 5 : 1;
    }

    public static int CalculateUpperBoundPage(int currentPage, int totalPages)
    {
        return currentPage + 5 < currentPage ? currentPage + 5 : totalPages;
    }

    public static string GetPageNumberForBound(int lowerValue, int upperValue)
    {
        return upperValue > lowerValue ? $"{upperValue}" : string.Empty;
    }
}
