namespace SimulatedExchange.Reporting
{
    public class CurrentPagingInfo
    {
        public CurrentPagingInfo(int currentPageIndex, int totalPage)
        {
            CurrentPageIndex = currentPageIndex;
            TotalPage = totalPage;
        }

        public int CurrentPageIndex { get; }
        public int TotalPage { get; }
    }
}
