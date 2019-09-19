using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MenuItem
{
    [Key]
    public int MenuItemId { get; set; }
    public int? MenuPaiId { get; set; }
    [Required]
    public int Nome { get; set; }
    public bool Habilitado { get; set; }

    [Required]
    public String Action { get; set; }
    [Required]
    public String Controller { get; set; }

    [ForeignKey("MenuPaiId")]
    public virtual MenuItem MenuPai { get; set; }
    public virtual ICollection<MenuItem> MenusFilhos { get; set; }
}