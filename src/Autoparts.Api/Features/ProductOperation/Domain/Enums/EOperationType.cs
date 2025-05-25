using System.ComponentModel.DataAnnotations;

namespace Autoparts.Api.Features.ProductOperation.Domain.Enums;

public enum EOperationType
{
    [Display(Name = "Purchase")]
    Purchase = 1,

    [Display(Name = "Sale")]
    Sale = 2,

    [Display(Name = "Return")]
    Return = 3
}
