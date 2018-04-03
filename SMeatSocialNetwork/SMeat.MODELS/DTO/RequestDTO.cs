using SMeat.MODELS.Entities;
using SMeat.MODELS.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SMeat.MODELS.DTO
{
  public class RequestDTO
  {
    public User User { get; set; }

    public User Friend { get; set; }

    public ContactStatus Status { get; set; }
  }
}
