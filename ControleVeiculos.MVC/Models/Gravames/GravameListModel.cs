using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Gravames
{
    public class GravameListModel
    {
        [DisplayName("Chassi")]
        public string PesquisarPorChassi { get; set; }

        [DisplayName("Número gravame")]
        public string PesquisarPorNumGravame { get; set; }

        [DisplayName("Placa")]
        public string PesquisarPorPlaca { get; set; }

        [DisplayName("Renavam")]
        public string PesquisarPorRenavam { get; set; }

        [DisplayName("UF Placa")]
        public string PesquisarPorUFPlaca { get; set; }

        public IList<SelectListItem> PesquisarUFsPlaca { get; set; }
    }
}