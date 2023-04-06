namespace DapperApi.Models.Dto.Product
{
    public sealed class ProductInsertDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
    }
}
