namespace ControleVeiculos.MVC.Infrastructure.KendoUI
{
    public class DataSourceRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public DataSourceRequest()
        {
            Page = 1;
            PageSize = 10;
        }
    }
}