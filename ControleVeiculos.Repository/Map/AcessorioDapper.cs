﻿using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Acessorio")]
    public class AcessorioDapper
    {
        [ExplicitKey]
        public int acessorioID { get; set; }
        public string gps { get; set; }
        public string airBag { get; set; }
        public string arCondicionado { get; set; }
        public string direcao { get; set; }
        public string travasEletricas { get; set; }
        public string vidroEletrico { get; set; }
        public string alarme { get; set; }
         
    }
}
