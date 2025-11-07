using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.DTOs.Requests;

public class EmailNotifictionModel
{
    public string? To { get; set; }
    public string? Cc { get; set; }
    public string? Cco { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public List<string> Attachment { get; set; } = new();

}

