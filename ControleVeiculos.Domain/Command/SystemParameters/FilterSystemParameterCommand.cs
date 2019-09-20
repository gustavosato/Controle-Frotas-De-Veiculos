namespace ControleVeiculos.Domain.Command.SystemParameters
{
    public class FilterSystemParameterCommand
    {
        public string ParamterName { get; set; }
        public string ParamterValue      { get; set; }
        public string ParamterDefaultValue { get; set; }
    }
}
