﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreMastersTodoList.Api.BindingModels
{
    public class ItemCreateBindingModel
    {
        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Text { get; set; } = string.Empty;
    }
}

