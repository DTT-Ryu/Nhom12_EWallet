using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom12_EWallet.Models;

[Table("tblRoles")]
[Index("SRoleName", Name = "UQ__tblRoles__1295E5A9A1DD0059", IsUnique = true)]
public partial class TblRole
{
    [Key]
    [Column("iRoleID_PK")]
    public byte IRoleIdPk { get; set; }

    [Column("sRoleName")]
    [StringLength(50)]
    public string SRoleName { get; set; } = null!;

    [InverseProperty("IRoleIdFkNavigation")]
    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
