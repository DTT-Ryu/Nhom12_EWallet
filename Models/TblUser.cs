using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom12_EWallet.Models;

[Table("tblUsers")]
[Index("SEmail", Name = "UQ__tblUsers__07DACB088C520435", IsUnique = true)]
[Index("SCccd", Name = "UQ__tblUsers__31AF424FAA530312", IsUnique = true)]
[Index("SPhoneNumber", Name = "UQ__tblUsers__E40346FF189A166A", IsUnique = true)]
public partial class TblUser
{
    [Key]
    [Column("iUserID_PK")]
    public int IUserIdPk { get; set; }

    [Column("sPhoneNumber")]
    [StringLength(10)]
    [Unicode(false)]
    public string SPhoneNumber { get; set; } = null!;

    [Column("sFullName")]
    [StringLength(100)]
    public string SFullName { get; set; } = null!;

    [Column("sCCCD")]
    [StringLength(12)]
    [Unicode(false)]
    public string SCccd { get; set; } = null!;

    [Column("dBirthDate")]
    public DateOnly DBirthDate { get; set; }

    [Column("sEmail")]
    [StringLength(100)]
    [Unicode(false)]
    public string SEmail { get; set; } = null!;

    [Column("fBalance", TypeName = "decimal(15, 2)")]
    public decimal? FBalance { get; set; }

    [Column("sPasswordHash")]
    [StringLength(255)]
    [Unicode(false)]
    public string SPasswordHash { get; set; } = null!;

    [Column("sPinCode")]
    [StringLength(255)]
    [Unicode(false)]
    public string SPinCode { get; set; } = null!;

    [Column("iRoleID_FK")]
    public byte IRoleIdFk { get; set; }

    [Column("dCreatedAt", TypeName = "datetime")]
    public DateTime? DCreatedAt { get; set; }

    [Column("dUpdatedAt", TypeName = "datetime")]
    public DateTime? DUpdatedAt { get; set; }

    [Column("sStatus")]
    [StringLength(10)]
    public string? SStatus { get; set; }

    [ForeignKey("IRoleIdFk")]
    [InverseProperty("TblUsers")]
    public virtual TblRole IRoleIdFkNavigation { get; set; } = null!;

    [InverseProperty("IUserIdFkNavigation")]
    public virtual ICollection<TblBankAccount> TblBankAccounts { get; set; } = new List<TblBankAccount>();

    [InverseProperty("IRecipientUserIdFkNavigation")]
    public virtual ICollection<TblTransaction> TblTransactionIRecipientUserIdFkNavigations { get; set; } = new List<TblTransaction>();

    [InverseProperty("ISenderUserIdFkNavigation")]
    public virtual ICollection<TblTransaction> TblTransactionISenderUserIdFkNavigations { get; set; } = new List<TblTransaction>();
}
