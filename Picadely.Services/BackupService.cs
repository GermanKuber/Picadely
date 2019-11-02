namespace Picadely.Services
{
    public class BackupService
    {
        public void Crear()
        {
            var sqlService = new SqlAccessService();
            sqlService.Backup();
        }
        public void Restore()
        {
            var sqlService = new SqlAccessService();
            sqlService.Restore();
        }
    }
}
