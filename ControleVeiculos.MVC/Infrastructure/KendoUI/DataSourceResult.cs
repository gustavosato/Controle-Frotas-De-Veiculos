using System.Collections;

namespace ControleVeiculos.MVC.Infrastructure.KendoUI
{
    public class DataSourceResult
    {
        public object ExtraData { get; set; }
        public IEnumerable Data { get; set; }
        public object Errors { get; set; }
        public int Total { get; set; }
    }
}