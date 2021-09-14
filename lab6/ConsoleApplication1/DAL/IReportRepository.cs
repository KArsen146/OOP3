namespace DAL
{
    public interface IReportRepository : IRepository<Report>
    {
        int Create(CommandReport entity);
        void Update(CommandReport entity);
        void Reset();
        
        CommandReport GetCommandReport();
    }
}