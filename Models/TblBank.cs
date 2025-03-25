using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom12_EWallet.Models;

[Table("tblBanks")]
[Index("SBankName", Name = "UQ__tblBanks__16DFBC10429B524F", IsUnique = true)]
public partial class TblBank
{
    [Key]
    [Column("sBankID_PK")]
    [StringLength(10)]
    [Unicode(false)]
    public string SBankIdPk { get; set; } = null!;

    [Column("sBankName")]
    [StringLength(100)]
    public string SBankName { get; set; } = null!;

    [Column("sImage")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SImage { get; set; }

    [InverseProperty("SBankIdFkNavigation")]
    public virtual ICollection<TblBankAccount> TblBankAccounts { get; set; } = new List<TblBankAccount>();
}
