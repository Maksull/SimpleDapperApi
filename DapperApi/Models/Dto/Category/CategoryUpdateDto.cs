﻿namespace DapperApi.Models.Dto.Category
{
    public sealed class CategoryUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
