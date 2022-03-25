﻿using System;

namespace Services.DTO
{
	public class ItemDTO
	{
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}
