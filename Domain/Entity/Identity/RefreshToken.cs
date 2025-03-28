﻿using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entity.Identity;

[Table("RefreshTokens")]
public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public DateTime Created { get; set; }
    public string? CreatedByIp { get; set; }
    public DateTime? Revoked { get; set; }
    public string? RevokedByIp { get; set; }
    public string? ReplacedByToken { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
        
    public string? UserId { get; set; }
        
    [ForeignKey(nameof(UserId))]
    public AppUser? User { get; set; }
}