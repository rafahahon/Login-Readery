using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Login_Readery.Models;

[Table("Usuario")]
[Index("Email", Name = "UQ__Usuario__A9D105344E42AF12", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [StringLength(150)]
    public string Email { get; set; } = null!;

    [MaxLength(32)]
    public byte[] SenhaHash { get; set; } = null!;

    [Column("FotoURL")]
    [StringLength(500)]
    public string? FotoUrl { get; set; }

    [Precision(0)]
    public DateTime DataCadastro { get; set; }
}
