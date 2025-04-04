using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom12_EWallet.Models;

[Table("tblBankAccounts")]
[Index("SAccountNumber", Name = "UQ__tblBankA__0A9F968F7025F9EB", IsUnique = true)]
public partial class TblBankAccount
{
    [Key]
    [Column("iAccountID_PK")]
    public int IAccountIdPk { get; set; }

    [Column("iUserID_FK")]
    public int IUserIdFk { get; set; }

    [Column("sBankID_FK")]
    [StringLength(10)]
    [Unicode(false)]
    public string SBankIdFk { get; set; } = null!;

    [Column("sAccountNumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string SAccountNumber { get; set; } = null!;

    [Column("sStatus")]
    [StringLength(10)]
    public string? SStatus { get; set; }

    public bool Deleted { get; set; }

    [ForeignKey("IUserIdFk")]
    [InverseProperty("TblBankAccounts")]
    public virtual TblUser IUserIdFkNavigation { get; set; } = null!;

    [ForeignKey("SBankIdFk")]
    [InverseProperty("TblBankAccounts")]
    public virtual TblBank SBankIdFkNavigation { get; set; } = null!;

    [InverseProperty("IBankAccountIdFkNavigation")]
    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
