using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Enums;

namespace SMeat.MODELS.Entities
{
  public class Friends
  {
    [ForeignKey("User")]
    [Required]
    public string UserId { get; set; }

    public virtual User User { get; set; }

    [ForeignKey("Friend")]
    [Required]
    public string FriendId { get; set; }

    public virtual User Friend { get; set; }

    public ContactStatus Status { get; set; }
  }
}
