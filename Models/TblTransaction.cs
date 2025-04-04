using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom12_EWallet.Models;

[Table("tblTransactions")]
public partial class TblTransaction
{
    [Key]
    [Column("iTransactionID_PK")]
    public int ITransactionIdPk { get; set; }

    [Column("iSenderUserID_FK")]
    public int ISenderUserIdFk { get; set; }

    [Column("sTransactionType")]
    [StringLength(10)]
    public string STransactionType { get; set; } = null!;

    [Column("fAmount", TypeName = "decimal(15, 2)")]
    public decimal FAmount { get; set; }

    [Column("dCreatedAt", TypeName = "datetime")]
    public DateTime? DCreatedAt { get; set; }

    [Column("sDescription")]
    [StringLength(255)]
    public string? SDescription { get; set; }

    [Column("iRecipientUserID_FK")]
    public int? IRecipientUserIdFk { get; set; }

    [Column("iBankAccountID_FK")]
    public int? IBankAccountIdFk { get; set; }

    [Column("sStatus")]
    [StringLength(10)]
    public string? SStatus { get; set; }

    public bool Deleted { get; set; }

    [ForeignKey("IBankAccountIdFk")]
    [InverseProperty("TblTransactions")]
    public virtual TblBankAccount? IBankAccountIdFkNavigation { get; set; }

    [ForeignKey("IRecipientUserIdFk")]
    [InverseProperty("TblTransactionIRecipientUserIdFkNavigations")]
    public virtual TblUser? IRecipientUserIdFkNavigation { get; set; }

    [ForeignKey("ISenderUserIdFk")]
    [InverseProperty("TblTransactionISenderUserIdFkNavigations")]
    public virtual TblUser ISenderUserIdFkNavigation { get; set; } = null!;
}
