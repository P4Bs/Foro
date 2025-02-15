namespace ForoWebApp.Helpers;

public static class PageHelper
{
    public static int ResolveRequestedPage(int? pageNumber, int totalPages)
    {
        int requestedPage = 1;
        if (pageNumber.HasValue)
        {
            if (pageNumber.Value > totalPages)
            {
                requestedPage = totalPages;
            }
            else if (pageNumber.Value > 1)
            {
                requestedPage = pageNumber.Value;
            }
        }
        return requestedPage;
    }

    public static int ResolveItemsPerPage(int? itemsPerPage, int defaultItemsPerPage)
    {
        int resolvedItemsPerPage = defaultItemsPerPage;
        if (itemsPerPage.HasValue && itemsPerPage.Value > 0)
        {
            resolvedItemsPerPage = itemsPerPage.Value;
        }
        return resolvedItemsPerPage;
    }
}
