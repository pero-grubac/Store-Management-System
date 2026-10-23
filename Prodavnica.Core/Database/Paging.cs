namespace Prodavnica.Database
{
    public sealed record PageRequest(int Page = 1, int PageSize = 20, string? Search = null)
    {
        public int SafePage => Math.Max(Page, 1);

        public int SafePageSize => Math.Clamp(PageSize, 1, 500);

        public int Offset => (SafePage - 1) * SafePageSize;

        // A LIKE pattern for "contains", with the wildcard characters in the search text escaped.
        public string? Like
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Search)) return null;
                string escaped = Search.Trim().Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
                return "%" + escaped + "%";
            }
        }
    }

    public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int Page, int PageSize)
    {
        public int PageCount => Math.Max(1, (TotalCount + PageSize - 1) / PageSize);

        public bool HasPrevious => Page > 1;

        public bool HasNext => Page < PageCount;

        public int FirstIndex => TotalCount == 0 ? 0 : (Page - 1) * PageSize + 1;

        public int LastIndex => Math.Min(Page * PageSize, TotalCount);
    }
}
