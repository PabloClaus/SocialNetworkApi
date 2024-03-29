﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkApi.Common.DTO.POST.Registration;

public class ApplicationUser
{
    [Required] public string? FirstName { get; set; }

    [Required] public string? LastName { get; set; }

    [Required] [EmailAddress] public string? Email { get; set; }

    [Required] [PasswordPropertyText] public string? Password { get; set; }

    [DataType(DataType.DateTime)] public DateTime? Birthday { get; set; } = default;

    [RegularExpression("MASC|FEM", ErrorMessage = "Only MASC or FEM allowed.")]
    public string? Gender { get; set; } = default;

}